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
using System.Windows.Forms;

namespace ILikePi.FileTypes.OptiPng
{
    internal partial class OptiPngSaveConfigWidget
    {
        private static readonly Padding firstIndent = new Padding(0, 0, 0, 0);
        private static readonly Padding secondIndent = new Padding(16, 0, 0, 0);
        private readonly int NUD_WIDTH = 50;

        private RadioButton grayscale;
        private RadioButton rgb;
        private CheckBox multiplyByAlpha;
        private RadioButton palette;
        private Label ditheringLabel;
        private NumericUpDown ditheringLevel;
        private Label transThreshLabel;
        private NumericUpDown transThresh;
        private RadioButton grayscaleA;
        private RadioButton rgbA;

        private CheckBox optimize;
        private NumericUpDown compression;
        private Label compressionComment;
        private CheckBox interlace;
        private CheckBox quiet;

        private LinkLabel optiPng;

        public OptiPngSaveConfigWidget()
            : base(new OptiPngFileType())
        {
            AutoSize = true;

            NUD_WIDTH = (int)(NUD_WIDTH * this.AutoScaleDimensions.Width / 96f);

            TableLayoutPanel main = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoSize = true
            };

            ToolTip toolTip = new ToolTip();
            initColors(toolTip, main);
            initCompression(toolTip, main);

            string poweredBy = L10nStrings.PoweredByOptiPNG;

            optiPng = new LinkLabel
            {
                AutoSize = true,
                LinkArea = new LinkArea(poweredBy.Length - 7, 7),
                Margin = new Padding(3, 10, 0, 0),
                Text = poweredBy
            };
            optiPng.LinkClicked += optiPng_LinkClicked;
            main.Controls.Add(optiPng);

            InitWidgetFromToken(new OptiPngSaveConfigToken());

            Controls.Add(main);
        }

        private void initColors(ToolTip toolTip, TableLayoutPanel main)
        {
            HeaderLabel colorHeader = newHeader(L10nStrings.ColorReduction);
            main.Controls.Add(colorHeader);

            grayscale = createBaseRadioBtn();
            grayscale.Text = L10nStrings.Grayscale;
            grayscale.Margin = firstIndent;
            toolTip.SetToolTip(grayscale, L10nStrings.GrayscaleDescription);
            grayscale.CheckedChanged += tokenChanged;
            main.Controls.Add(grayscale);

            rgb = createBaseRadioBtn();
            rgb.Text = L10nStrings.Rgb;
            rgb.Margin = firstIndent;
            toolTip.SetToolTip(rgb, L10nStrings.RgbDescription);
            rgb.CheckedChanged += tokenChanged;
            main.Controls.Add(rgb);

            multiplyByAlpha = createBaseChkBox();
            multiplyByAlpha.Text = L10nStrings.MultiplyByAlpha;
            multiplyByAlpha.Margin = secondIndent;
            toolTip.SetToolTip(multiplyByAlpha, L10nStrings.MultiplyByAlphaDescription);
            multiplyByAlpha.CheckedChanged += tokenChanged;
            main.Controls.Add(multiplyByAlpha);

            palette = createBaseRadioBtn();
            palette.Text = L10nStrings.Palette;
            palette.Margin = firstIndent;
            toolTip.SetToolTip(palette, L10nStrings.PaletteDescription);
            palette.CheckedChanged += tokenChanged;
            main.Controls.Add(palette);

            TableLayoutPanel dithering = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                Margin = secondIndent
            };

            ditheringLabel = new Label
            {
                Anchor = AnchorStyles.Left,
                AutoSize = true,
                Text = L10nStrings.DitheringLevel,
                Margin = Padding.Empty
            };
            dithering.Controls.Add(ditheringLabel, 0, 0);

            ditheringLevel = new NumericUpDown
            {
                Width = NUD_WIDTH,
                Margin = Padding.Empty,
                Maximum = 8
            };
            ditheringLevel.ValueChanged += tokenChanged;
            dithering.Controls.Add(ditheringLevel, 1, 0);

            transThreshLabel = new Label
            {
                Anchor = AnchorStyles.Left,
                AutoSize = true,
                Text = L10nStrings.TransparencyThreshold,
                Margin = Padding.Empty
            };
            dithering.Controls.Add(transThreshLabel, 0, 1);

            transThresh = new NumericUpDown
            {
                Width = NUD_WIDTH,
                Margin = new Padding(0, 3, 0, 0),
                Maximum = 255
            };
            toolTip.SetToolTip(transThresh, L10nStrings.TransparencyThresholdDescription);
            transThresh.ValueChanged += tokenChanged;
            dithering.Controls.Add(transThresh, 1, 1);

            main.Controls.Add(dithering);

            grayscaleA = createBaseRadioBtn();
            grayscaleA.Text = L10nStrings.GrayScaleWithAlpha;
            grayscaleA.Margin = firstIndent;
            toolTip.SetToolTip(grayscaleA, L10nStrings.GrayScaleWithAlphaDescription);
            grayscaleA.CheckedChanged += tokenChanged;
            main.Controls.Add(grayscaleA);

            rgbA = createBaseRadioBtn();
            rgbA.Text = L10nStrings.RgbWithAlpha;
            rgbA.Margin = firstIndent;
            toolTip.SetToolTip(rgbA, L10nStrings.RgbWithAlphaDescription);
            rgbA.CheckedChanged += tokenChanged;
            main.Controls.Add(rgbA);
        }

        private void initCompression(ToolTip toolTip, TableLayoutPanel main)
        {
            HeaderLabel compressionHeader = newHeader(L10nStrings.Compression);
            main.Controls.Add(compressionHeader);

            optimize = createBaseChkBox();
            optimize.Text = L10nStrings.Optimize;
            optimize.Margin = firstIndent;
            toolTip.SetToolTip(optimize, L10nStrings.OptimizeDescription);
            optimize.CheckedChanged += tokenChanged;
            main.Controls.Add(optimize);

            TableLayoutPanel compressionPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                Margin = firstIndent
            };

            compression = new NumericUpDown
            {
                Width = NUD_WIDTH,
                Maximum = 7,
                Minimum = 1,
                Margin = Padding.Empty
            };
            toolTip.SetToolTip(compression, L10nStrings.CompressionDescription);
            compression.ValueChanged += tokenChanged;
            compression.ValueChanged += compression_ValueChanged;
            compressionPanel.Controls.Add(compression, 0, 0);

            compressionComment = new Label
            {
                Anchor = AnchorStyles.Left,
                AutoSize = true,
                Margin = Padding.Empty
            };
            compressionPanel.Controls.Add(compressionComment, 1, 0);

            main.Controls.Add(compressionPanel);

            interlace = createBaseChkBox();
            interlace.Text = L10nStrings.Interlace;
            interlace.Margin = firstIndent;
            toolTip.SetToolTip(interlace, L10nStrings.InterlaceDescription);
            interlace.CheckedChanged += tokenChanged;
            main.Controls.Add(interlace);

            quiet = createBaseChkBox();
            quiet.Text = L10nStrings.QuietMode;
            quiet.Margin = firstIndent;
            toolTip.SetToolTip(quiet, L10nStrings.QuietModeDescription);
            quiet.CheckedChanged += tokenChanged;
            main.Controls.Add(quiet);
        }

        private RadioButton createBaseRadioBtn()
        {
            return new RadioButton
            {
                AutoSize = true
            };
        }

        private CheckBox createBaseChkBox()
        {
            return new CheckBox
            {
                AutoSize = true
            };
        }

        private HeaderLabel newHeader(string text)
        {
            return new HeaderLabel
            {
                Dock = DockStyle.Fill,
                Text = text
            };
        }

        protected override void OnForeColorChanged(EventArgs e)
        {
            base.OnForeColorChanged(e);

            optiPng.LinkColor = ForeColor;
        }
    }
}
