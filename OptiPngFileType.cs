/*
 * OptiPNG file type
 * Copyright (C) 2008 ilikepi3142@gmail.com
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using PaintDotNet;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;

namespace ILikePi.FileTypes.OptiPng
{
    public sealed class OptiPngFileTypeFactory : IFileTypeFactory
    {
        public FileType[] GetFileTypeInstances()
        {
            return new[] { new OptiPngFileType() };
        }
    }

    internal class OptiPngFileType : FileType<OptiPngSaveConfigToken, OptiPngSaveConfigWidget>
    {
        private readonly string tempFile;
        private OptiPngSaveConfigToken tempFileToken;
        private Surface tempFileSurface;
        private static readonly string OptiPNGPath = InitOptiPNGPath();

        private static string InitOptiPNGPath()
        {
            string effectsDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return Path.Combine(effectsDir, "optipng.exe");
        }

        internal OptiPngFileType()
            : base("Optimized PNG", FileTypeFlags.SupportsLoading | FileTypeFlags.SupportsSaving, new[] { ".png" })
        {
            string path = Path.Combine(Path.GetTempPath(), "PDN_OptiPNG_");
            Random rnd = new Random();
            do
            {
                tempFile = path + rnd.Next() + ".png";
            } while (File.Exists(tempFile));
        }

        protected override Document OnLoad(Stream input)
        {
            using (Image image = Image.FromStream(input))
            {
                return Document.FromImage(image);
            }
        }

        protected override bool IsReflexive(OptiPngSaveConfigToken token)
        {
            return token.Color == ColorMode.RGBAlpha;
        }

        protected override void OnSaveT(Document input, Stream output, OptiPngSaveConfigToken token, Surface scratchSurface, ProgressEventHandler progressCallback)
        {
            using (RenderArgs ra = new RenderArgs(scratchSurface))
            {
                input.Render(ra, true);
            }

            // Only do reductions, saving, and optimizing if the document changed
            // This is to prevent reoptimizing when the user finalizes the settings by clicking "OK"
            if (!token.Equals(tempFileToken) || !areSurfacesEqual(scratchSurface, tempFileSurface))
            {
                tempFileToken = (OptiPngSaveConfigToken)token.Clone();
                if (tempFileSurface != null)
                {
                    tempFileSurface.Dispose();
                }
                tempFileSurface = scratchSurface.Clone();

                // Color reductions
                if (token.Color == ColorMode.Grayscale || token.Color == ColorMode.RGB)
                {
                    eliminateAlphaChannel(scratchSurface, token.MultiplyByAlphaChannel);
                }

                if (token.Color == ColorMode.Grayscale || token.Color == ColorMode.GrayscaleAlpha)
                {
                    new UnaryPixelOps.Desaturate().Apply(scratchSurface, scratchSurface.Bounds);
                }
                Bitmap final;
                if (token.Color == ColorMode.Palette)
                {
                    final = reduceToPalette(scratchSurface, token.DitheringLevel, token.TransparencyThreshold, progressCallback);
                }
                else
                {
                    final = scratchSurface.CreateAliasedBitmap();
                }

                float dpiX;
                float dpiY;

                switch (input.DpuUnit)
                {
                    case MeasurementUnit.Centimeter:
                        dpiX = (float)Document.DotsPerCmToDotsPerInch(input.DpuX);
                        dpiY = (float)Document.DotsPerCmToDotsPerInch(input.DpuY);
                        break;

                    case MeasurementUnit.Inch:
                        dpiX = (float)input.DpuX;
                        dpiY = (float)input.DpuY;
                        break;

                    default:
                    case MeasurementUnit.Pixel:
                        dpiX = 1.0f;
                        dpiY = 1.0f;
                        break;
                }

                final.SetResolution(dpiX, dpiY);
                final.Save(tempFile, ImageFormat.Png);
                final.Dispose();

                // Optimize if user wants it
                if (token.Optimize)
                {
                    string args = $"\"{tempFile}\" -o{token.Compression} -i{(token.Interlace ? "1" : "0")}{(token.Quiet ? " -quiet" : string.Empty)}";
                    ProcessStartInfo startInfo = new ProcessStartInfo(OptiPNGPath, args);
                    if (token.Quiet)
                    {
                        startInfo.CreateNoWindow = true;
                        startInfo.UseShellExecute = false;
                    }
                    else
                    {
                        startInfo.WindowStyle = ProcessWindowStyle.Minimized;
                    }

                    using (Process process = Process.Start(startInfo))
                    {
                        process.WaitForExit();
                    }
                }

            }
            // Rewrite to target
            using (FileStream transfer = new FileStream(tempFile, FileMode.Open, FileAccess.Read))
            {

                int bufferLength = (int)Math.Min(transfer.Length, 4096L);
                byte[] buffer = new byte[bufferLength];

                long remaining = transfer.Length;
                int readLength = bufferLength;

                do
                {
                    if (readLength > remaining)
                    {
                        readLength = (int)remaining;
                    }

                    int bytesRead = transfer.Read(buffer, 0, readLength);
                    output.Write(buffer, 0, bytesRead);

                    remaining -= bytesRead;
                } while (remaining > 0);

            }
        }

        private static unsafe void eliminateAlphaChannel(Surface surface, bool multByAlpha)
        {
            BinaryPixelOp blendOp = new UserBlendOps.NormalBlendOp();
            for (int y = 0; y < surface.Height; y++)
            {
                ColorBgra* ptr = surface.GetRowAddressUnchecked(y);

                for (int x = 0; x < surface.Width; x++)
                {
                    if (multByAlpha)
                    {
                        ptr->Bgra = blendOp.Apply(ColorBgra.White, *ptr).Bgra;
                    }
                    else if (ptr->A == 0)
                    {
                        ptr->Bgra = 0xFFFFFFFF;
                    }
                    else
                    {
                        ptr->A = 255;
                    }
                    ptr++;
                }
            }
        }

        private unsafe Bitmap reduceToPalette(Surface surface, byte ditheringLevel, byte threshold, ProgressEventHandler progressCallback)
        {
            BinaryPixelOp blendOp = new UserBlendOps.NormalBlendOp();

            for (int y = 0; y < surface.Height; y++)
            {
                ColorBgra* ptr = surface.GetRowAddressUnchecked(y);

                for (int x = 0; x < surface.Width; x++)
                {
                    if (ptr->A < threshold)
                    {
                        ptr->Bgra = 0x00000000;
                    }
                    else
                    {
                        ptr->Bgra = blendOp.Apply(ColorBgra.White, *ptr).Bgra;
                    }
                    ptr++;
                }
            }

            return Quantize(surface, ditheringLevel, 256, true, progressCallback);
        }

        private static unsafe bool areSurfacesEqual(Surface a, Surface b)
        {
            if (a == null || b == null)
            {
                return false;
            }
            if (!a.Size.Equals(b.Size))
            {
                return false;
            }
            for (int y = 0; y < a.Height; y++)
            {
                ColorBgra* ptrA = a.GetRowAddressUnchecked(y);
                ColorBgra* ptrB = b.GetRowAddressUnchecked(y);
                for (int x = 0; x < a.Width; x++)
                {
                    if (!ptrA->Equals(*ptrB))
                    {
                        return false;
                    }
                    ptrA++;
                    ptrB++;
                }
            }
            return true;
        }

        ~OptiPngFileType()
        {
            File.Delete(tempFile);
        }

        protected override OptiPngSaveConfigToken OnCreateDefaultSaveConfigTokenT()
        {
            return new OptiPngSaveConfigToken();
        }

        protected override OptiPngSaveConfigWidget OnCreateSaveConfigWidgetT()
        {
            return new OptiPngSaveConfigWidget();
        }
    }
}
