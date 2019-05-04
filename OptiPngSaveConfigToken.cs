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

namespace ILikePi.FileTypes.OptiPng
{
    internal enum ColorMode
    {
        Grayscale,
        RGB,
        Palette,
        GrayscaleAlpha,
        RGBAlpha
    }

    [Serializable]
    internal class OptiPngSaveConfigToken : SaveConfigToken
    {
        private byte compression;
        private byte ditheringLevel;

        public OptiPngSaveConfigToken()
        {
            Color = ColorMode.RGBAlpha;
            Compression = 2;
            DitheringLevel = 7;
            TransparencyThreshold = 128;
            MultiplyByAlphaChannel = true;
            Interlace = false;
            Optimize = true;
            Quiet = true;
        }

        public override object Clone()
        {
            return MemberwiseClone();
        }

        public override void Validate()
        {
            validateCompression(compression);
            validateDitheringLevel(ditheringLevel);
        }

        public override bool Equals(object obj)
        {
            OptiPngSaveConfigToken token = obj as OptiPngSaveConfigToken;
            if (token == null)
            {
                return false;
            }
            return token.Color == Color
                && token.compression == compression
                && token.ditheringLevel == ditheringLevel
                && token.TransparencyThreshold == TransparencyThreshold
                && token.Interlace == Interlace
                && token.MultiplyByAlphaChannel == MultiplyByAlphaChannel
                && token.Optimize == Optimize
                && token.Quiet == Quiet;
        }

        public override int GetHashCode()
        {
            return (int)Color + compression * 3 + ditheringLevel * 7;
        }

        public ColorMode Color { get; set; }

        public byte Compression
        {
            get
            {
                return compression;
            }
            set
            {
                validateCompression(value);
                compression = value;
            }
        }

        public byte DitheringLevel
        {
            get
            {
                return ditheringLevel;
            }
            set
            {
                validateDitheringLevel(value);
                ditheringLevel = value;
            }
        }

        public byte TransparencyThreshold { get; set; }

        public bool MultiplyByAlphaChannel { get; set; }

        public bool Interlace { get; set; }

        public bool Optimize { get; set; }

        public bool Quiet { get; set; }

        private void validateCompression(byte value)
        {
            if (value > 7 || value < 1)
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        private void validateDitheringLevel(byte value)
        {
            if (value > 8 || value < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
        }
    }
}