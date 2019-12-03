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

            optiPng = new LinkLabel
            {
                AutoSize = true,
                LinkArea = new LinkArea(11, 7),
                Margin = new Padding(3, 10, 0, 0),
                Text = "Работает на OptiPNG"
            };
            optiPng.LinkClicked += optiPng_LinkClicked;
            main.Controls.Add(optiPng);

            InitWidgetFromToken(new OptiPngSaveConfigToken());

            Controls.Add(main);
        }

        private void initColors(ToolTip toolTip, TableLayoutPanel main)
        {
            HeaderLabel colorHeader = newHeader("Уменьшение цвета");
            main.Controls.Add(colorHeader);

            grayscale = createBaseRadioBtn();
            grayscale.Text = "&Оттенки серого";
            grayscale.Margin = firstIndent;
            toolTip.SetToolTip(grayscale, "Черно-белый без прозрачности");
            grayscale.CheckedChanged += tokenChanged;
            main.Controls.Add(grayscale);

            rgb = createBaseRadioBtn();
            rgb.Text = "&RGB";
            rgb.Margin = firstIndent;
            toolTip.SetToolTip(rgb, "RGB без прозрачности");
            rgb.CheckedChanged += tokenChanged;
            main.Controls.Add(rgb);

            multiplyByAlpha = createBaseChkBox();
            multiplyByAlpha.Text = "&Умножение на альфа-канал";
            multiplyByAlpha.Margin = secondIndent;
            toolTip.SetToolTip(multiplyByAlpha, "Для цветовых режимов которая не поддерживают альфа-канал,\n" +
                                                "умножить на альфа-канал");
            multiplyByAlpha.CheckedChanged += tokenChanged;
            main.Controls.Add(multiplyByAlpha);

            palette = createBaseRadioBtn();
            palette.Text = "&Палитра";
            palette.Margin = firstIndent;
            toolTip.SetToolTip(palette, "Не более 256 различных цветов");
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
                Text = "&Уровень сглаживания:",
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
                Text = "&Порог прозрачности :",
                Margin = Padding.Empty
            };
            dithering.Controls.Add(transThreshLabel, 0, 1);

            transThresh = new NumericUpDown
            {
                Width = NUD_WIDTH,
                Margin = new Padding(0, 3, 0, 0),
                Maximum = 255
            };
            toolTip.SetToolTip(transThresh, "Пиксели с альфа-значением меньше порога будут полностью прозрачными.");
            transThresh.ValueChanged += tokenChanged;
            dithering.Controls.Add(transThresh, 1, 1);

            main.Controls.Add(dithering);

            grayscaleA = createBaseRadioBtn();
            grayscaleA.Text = "Оттенки серого с альфа";
            grayscaleA.Margin = firstIndent;
            toolTip.SetToolTip(grayscaleA, "Черно-белый с прозрачностью");
            grayscaleA.CheckedChanged += tokenChanged;
            main.Controls.Add(grayscaleA);

            rgbA = createBaseRadioBtn();
            rgbA.Text = "RGB с альфа-каналом";
            rgbA.Margin = firstIndent;
            toolTip.SetToolTip(rgbA, "RGB с прозрачностью (всегда без потерь)");
            rgbA.CheckedChanged += tokenChanged;
            main.Controls.Add(rgbA);
        }

        private void initCompression(ToolTip toolTip, TableLayoutPanel main)
        {
            HeaderLabel compressionHeader = newHeader("Сжатия");
            main.Controls.Add(compressionHeader);

            optimize = createBaseChkBox();
            optimize.Text = "&Оптимизировать";
            optimize.Margin = firstIndent;
            toolTip.SetToolTip(optimize, "Снимите этот флажок, если требуется только предосмотр.\n" +
                                         "Вы можете безопасно завершить оптимизацию закрыв окно консоли.");
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
            toolTip.SetToolTip(compression, "Это пресеты OptiPNG. По сути они говорят OptiPNG, сколько грубой силы использовать.\r\n" +
                                            "Более высокие значения не всегда могут давать меньшие размеры,\r\n" +
                                            "если нижняя предустановка уже имеет оптимальные настройки.");
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
            interlace.Text = "&Чередование";
            interlace.Margin = firstIndent;
            toolTip.SetToolTip(interlace, "Позволяет браузерам сначала отображать версии изображения\n" +
                                          "с низкой детализацией и улучшать детализацию по мере загрузки.\n" +
                                          "Включение этого параметра обычно увеличивает размер файла.");
            interlace.CheckedChanged += tokenChanged;
            main.Controls.Add(interlace);

            quiet = createBaseChkBox();
            quiet.Text = "&Тихий режим";
            quiet.Margin = firstIndent;
            toolTip.SetToolTip(quiet, "С помощью этого флажка optipng.exe будет запущен в тихом режиме.");
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
