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
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

namespace ILikePi.FileTypes.OptiPng
{
    internal partial class OptiPngSaveConfigWidget : SaveConfigWidget<OptiPngFileType, OptiPngSaveConfigToken>
    {
        protected override OptiPngSaveConfigToken CreateTokenFromWidget()
        {
            OptiPngSaveConfigToken token = new OptiPngSaveConfigToken();

            if (grayscale.Checked)
            {
                token.Color = ColorMode.Grayscale;
            }
            else if (rgb.Checked)
            {
                token.Color = ColorMode.RGB;
            }
            else if (palette.Checked)
            {
                token.Color = ColorMode.Palette;
            }
            else if (grayscaleA.Checked)
            {
                token.Color = ColorMode.GrayscaleAlpha;
            }
            else if (rgbA.Checked)
            {
                token.Color = ColorMode.RGBAlpha;
            }

            token.Compression = (byte)compression.Value;
            token.DitheringLevel = (byte)ditheringLevel.Value;
            token.TransparencyThreshold = (byte)transThresh.Value;
            token.MultiplyByAlphaChannel = multiplyByAlpha.Checked;
            token.Interlace = interlace.Checked;
            token.Optimize = optimize.Checked;
            token.Quiet = quiet.Checked;

            return token;
        }

        protected override void InitWidgetFromToken(OptiPngSaveConfigToken sourceToken)
        {
            switch (sourceToken.Color)
            {
                case ColorMode.Grayscale:
                    grayscale.Checked = true;
                    break;
                case ColorMode.RGB:
                    rgb.Checked = true;
                    break;
                case ColorMode.Palette:
                    palette.Checked = true;
                    break;
                case ColorMode.GrayscaleAlpha:
                    grayscaleA.Checked = true;
                    break;
                case ColorMode.RGBAlpha:
                    rgbA.Checked = true;
                    break;
            }

            compression.Value = sourceToken.Compression;
            ditheringLevel.Value = sourceToken.DitheringLevel;
            transThresh.Value = sourceToken.TransparencyThreshold;
            multiplyByAlpha.Checked = sourceToken.MultiplyByAlphaChannel;
            interlace.Checked = sourceToken.Interlace;
            optimize.Checked = sourceToken.Optimize;
            quiet.Checked = sourceToken.Quiet;

            enforceDependencies();
        }

        private void tokenChanged(object sender, EventArgs e)
        {
            enforceDependencies();
            UpdateToken();
        }

        private void compression_ValueChanged(object sender, EventArgs e)
        {
            switch ((byte)compression.Value)
            {
                case 1:
                    compressionComment.Text = L10nStrings.Trials1;
                    break;
                case 2:
                    compressionComment.Text = L10nStrings.Trials8;
                    break;
                case 3:
                    compressionComment.Text = L10nStrings.Trials16;
                    break;
                case 4:
                    compressionComment.Text = L10nStrings.Trials24;
                    break;
                case 5:
                    compressionComment.Text = L10nStrings.Trials48;
                    break;
                case 6:
                    compressionComment.Text = L10nStrings.Trials120;
                    break;
                case 7:
                    compressionComment.Text = L10nStrings.Trials240;
                    break;
            }
        }

        private void optiPng_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start("http://optipng.sourceforge.net/");
            }
            catch (Win32Exception)
            {
                // Sometimes windows says file not found when Firefox takes too long to open
            }
        }

        private void enforceDependencies()
        {
            if (rgb.Checked || grayscale.Checked)
            {
                multiplyByAlpha.Enabled = true;
            }
            else
            {
                multiplyByAlpha.Checked = false;
                multiplyByAlpha.Enabled = false;
            }

            ditheringLevel.Enabled = palette.Checked;
            ditheringLabel.Enabled = palette.Checked;
            transThresh.Enabled = palette.Checked;
            transThreshLabel.Enabled = palette.Checked;

            if (optimize.Checked)
            {
                interlace.Enabled = true;
                compression.Enabled = true;
                compressionComment.Enabled = true;
            }
            else
            {
                interlace.Checked = false;
                interlace.Enabled = false;
                compression.Enabled = false;
                compressionComment.Enabled = false;
            }
        }
    }
}
