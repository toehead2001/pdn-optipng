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

using System.Windows.Forms;
using PaintDotNet;

namespace ILikePi.FileTypes.OptiPng {
    internal partial class OptiPngSaveConfigWidget {
        private static readonly Padding firstIndent = new Padding(8, 0, 0, 0);
        private static readonly Padding secondIndent = new Padding(23, 0, 0, 0);
        private const int NUD_WIDTH = 50;

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

        public OptiPngSaveConfigWidget()
            : base(new OptiPngFileType()) {
            AutoSize = true;

            TableLayoutPanel main = new TableLayoutPanel {
                Dock = DockStyle.Fill,
                AutoSize = true
            };

            ToolTip toolTip = new ToolTip();
            initColors(toolTip, main);
            initCompression(toolTip, main);

            LinkLabel optiPng = new LinkLabel {
                AutoSize = true,
                LinkArea = new LinkArea(11, 7),
                Margin = new Padding(3, 10, 0, 0),
                Text = "Powered by OptiPNG"
            };
            optiPng.LinkClicked += optiPng_LinkClicked;
            main.Controls.Add(optiPng);

            InitWidgetFromToken(new OptiPngSaveConfigToken());

            Controls.Add(main);
        }

        private void initColors(ToolTip toolTip, TableLayoutPanel main) {
            HeaderLabel colorHeader = newHeader("Color reduction");
            main.Controls.Add(colorHeader);

            grayscale = createBaseRadioBtn();
            grayscale.Text = "&Grayscale";
            grayscale.Margin = firstIndent;
            toolTip.SetToolTip(grayscale, "Black/white without transparency");
            grayscale.CheckedChanged += tokenChanged;
            main.Controls.Add(grayscale);

            rgb = createBaseRadioBtn();
            rgb.Text = "&RGB";
            rgb.Margin = firstIndent;
            toolTip.SetToolTip(rgb, "RGB without transparency");
            rgb.CheckedChanged += tokenChanged;
            main.Controls.Add(rgb);

            multiplyByAlpha = createBaseChkBox();
            multiplyByAlpha.Text = "&Multiply by alpha channel";
            multiplyByAlpha.Margin = secondIndent;
            toolTip.SetToolTip(multiplyByAlpha, "For color modes that do not support the alpha channel, multipy by the alpha channel");
            multiplyByAlpha.CheckedChanged += tokenChanged;
            main.Controls.Add(multiplyByAlpha);

            palette = createBaseRadioBtn();
            palette.Text = "&Palette";
            palette.Margin = firstIndent;
            toolTip.SetToolTip(palette, "No more than 256 distinct colors");
            palette.CheckedChanged += tokenChanged;
            main.Controls.Add(palette);

            TableLayoutPanel dithering = new TableLayoutPanel {
                Dock = DockStyle.Fill,
                AutoSize = true,
                Margin = secondIndent
            };

            ditheringLabel = new Label {
                Anchor = AnchorStyles.Left,
                AutoSize = true,
                Text = "&Dithering level:",
                Margin = Padding.Empty
            };
            dithering.Controls.Add(ditheringLabel, 0, 0);

            ditheringLevel = new NumericUpDown {
                Width = NUD_WIDTH,
                Margin = Padding.Empty,
                Maximum = 8
            };
            ditheringLevel.ValueChanged += tokenChanged;
            dithering.Controls.Add(ditheringLevel, 1, 0);

            transThreshLabel = new Label {
                Anchor = AnchorStyles.Left,
                AutoSize = true,
                Text = "&Transparency threshold:",
                Margin = Padding.Empty
            };
            dithering.Controls.Add(transThreshLabel, 0, 1);

            transThresh = new NumericUpDown {
                Width = NUD_WIDTH,
                Margin = new Padding(0, 3, 0, 0),
                Maximum = 255
            };
            toolTip.SetToolTip(transThresh, "Pixels with an alpha value less than the threshold will be fully transparent.");
            transThresh.ValueChanged += tokenChanged;
            dithering.Controls.Add(transThresh, 1, 1);

            main.Controls.Add(dithering);

            grayscaleA = createBaseRadioBtn();
            grayscaleA.Text = "Gr&ayscale with alpha";
            grayscaleA.Margin = firstIndent;
            toolTip.SetToolTip(grayscaleA, "Black/white with transparency");
            grayscaleA.CheckedChanged += tokenChanged;
            main.Controls.Add(grayscaleA);

            rgbA = createBaseRadioBtn();
            rgbA.Text = "RG&B with alpha";
            rgbA.Margin = firstIndent;
            toolTip.SetToolTip(rgbA, "RGB with transparency (always lossless)");
            rgbA.CheckedChanged += tokenChanged;
            main.Controls.Add(rgbA);
        }

        private void initCompression(ToolTip toolTip, TableLayoutPanel main) {
            HeaderLabel compressionHeader = newHeader("Compression");
            main.Controls.Add(compressionHeader);

            optimize = createBaseChkBox();
            optimize.Text = "&Optimize";
            optimize.Margin = firstIndent;
            toolTip.SetToolTip(optimize, "Uncheck this if you just want to preview.\n" +
                                         "You can safely end optimization by closing the console window.");
            optimize.CheckedChanged += tokenChanged;
            main.Controls.Add(optimize);

            TableLayoutPanel compressionPanel = new TableLayoutPanel {
                Dock = DockStyle.Fill,
                AutoSize = true,
                Margin = firstIndent
            };

            compression = new NumericUpDown {
                Width = NUD_WIDTH,
                Maximum = 7,
                Minimum = 1,
                Margin = Padding.Empty
            };
            toolTip.SetToolTip(compression, "These are OptiPNG presets. They essentially tell OptiPNG how much brute force to use.\r\n" +
                                            "Higher values may not always give smaller sizes if a lower preset already has optimal settings.");
            compression.ValueChanged += tokenChanged;
            compression.ValueChanged += compression_ValueChanged;
            compressionPanel.Controls.Add(compression, 0, 0);

            compressionComment = new Label {
                Anchor = AnchorStyles.Left,
                AutoSize = true,
                Margin = Padding.Empty
            };
            compressionPanel.Controls.Add(compressionComment, 1, 0);

            main.Controls.Add(compressionPanel);

            interlace = createBaseChkBox();
            interlace.Text = "&Interlace";
            interlace.Margin = firstIndent;
            toolTip.SetToolTip(interlace, "Allows browsers to display low detail versions of an image first and improve the detail as more is downloaded.\n" + 
                                          "Turning this on generally increases the file size.");
            interlace.CheckedChanged += tokenChanged;    
            main.Controls.Add(interlace);

            quiet = createBaseChkBox();
            quiet.Text = "&Quiet mode";
            quiet.Margin = firstIndent;
            toolTip.SetToolTip(quiet, "With this check box checked optipng.exe will be launched in quiet mode.");
            quiet.CheckedChanged += tokenChanged;
            main.Controls.Add(quiet);
        }

        private RadioButton createBaseRadioBtn() {
            return new RadioButton {
                AutoSize = true,
                FlatStyle = FlatStyle.System
            };
        }

        private CheckBox createBaseChkBox() {
            return new CheckBox {
                AutoSize = true,
                FlatStyle = FlatStyle.System
            };
        }

        private HeaderLabel newHeader(string text) {
            return new HeaderLabel {
                Dock = DockStyle.Fill,
                Text = text
            };
        }
    }
}