using System.Globalization;

namespace ILikePi.FileTypes.OptiPng
{
    internal static class L10nStrings
    {
        private static readonly string uiCulture = CultureInfo.CurrentUICulture.Name;

        internal static string EffectName
        {
            get
            {
                switch (uiCulture)
                {
                    case "ru":
                        return "Оптимизированный PNG";
                    default:
                        return "Optimized PNG";
                }
            }
        }

        internal static string Trials1
        {
            get
            {
                switch (uiCulture)
                {
                    case "ru":
                        return "Эвристика (1 проход)";
                    default:
                        return "Heuristics (1 trial)";
                }
            }
        }

        internal static string Trials8
        {
            get
            {
                switch (uiCulture)
                {
                    case "ru":
                        return "8 проходов";
                    default:
                        return "8 trials";
                }
            }
        }

        internal static string Trials16
        {
            get
            {
                switch (uiCulture)
                {
                    case "ru":
                        return "16 проходов";
                    default:
                        return "16 trials";
                }
            }
        }

        internal static string Trials24
        {
            get
            {
                switch (uiCulture)
                {
                    case "ru":
                        return "24 проходов";
                    default:
                        return "24 trials";
                }
            }
        }

        internal static string Trials48
        {
            get
            {
                switch (uiCulture)
                {
                    case "ru":
                        return "48 проходов";
                    default:
                        return "48 trials";
                }
            }
        }

        internal static string Trials120
        {
            get
            {
                switch (uiCulture)
                {
                    case "ru":
                        return "120 проходов";
                    default:
                        return "120 trials";
                }
            }
        }

        internal static string Trials240
        {
            get
            {
                switch (uiCulture)
                {
                    case "ru":
                        return "240 проходов";
                    default:
                        return "240 trials";
                }
            }
        }

        internal static string PoweredByOptiPNG
        {
            get
            {
                switch (uiCulture)
                {
                    case "ru":
                        return "Работает на OptiPNG";
                    default:
                        return "Powered by OptiPNG";
                }
            }
        }

        internal static string ColorReduction
        {
            get
            {
                switch (uiCulture)
                {
                    case "ru":
                        return "Уменьшение цвета";
                    default:
                        return "Color reduction";
                }
            }
        }

        internal static string Grayscale
        {
            get
            {
                switch (uiCulture)
                {
                    case "ru":
                        return "Оттенки серого";
                    default:
                        return "&Grayscale";
                }
            }
        }

        internal static string GrayscaleDescription
        {
            get
            {
                switch (uiCulture)
                {
                    case "ru":
                        return "Черно-белый без прозрачности";
                    default:
                        return "Black/white without transparency";
                }
            }
        }

        internal static string Rgb
        {
            get
            {
                switch (uiCulture)
                {
                    case "ru":
                        return "RGB";
                    default:
                        return "&RGB";
                }
            }
        }

        internal static string RgbDescription
        {
            get
            {
                switch (uiCulture)
                {
                    case "ru":
                        return "RGB без прозрачности";
                    default:
                        return "RGB without transparency";
                }
            }
        }

        internal static string MultiplyByAlpha
        {
            get
            {
                switch (uiCulture)
                {
                    case "ru":
                        return "Умножение на альфа-канал";
                    default:
                        return "&Multiply by alpha channel";
                }
            }
        }

        internal static string MultiplyByAlphaDescription
        {
            get
            {
                switch (uiCulture)
                {
                    case "ru":
                        return "Для цветовых режимов которая не поддерживают альфа - канал,\r\nумножить на альфа-канал";
                    default:
                        return "For color modes that do not support the alpha channel, multiply by the alpha channel";
                }
            }
        }

        internal static string Palette
        {
            get
            {
                switch (uiCulture)
                {
                    case "ru":
                        return "Палитра";
                    default:
                        return "&Palette";
                }
            }
        }

        internal static string PaletteDescription
        {
            get
            {
                switch (uiCulture)
                {
                    case "ru":
                        return "Не более 256 различных цветов";
                    default:
                        return "No more than 256 distinct colors";
                }
            }
        }

        internal static string DitheringLevel
        {
            get
            {
                switch (uiCulture)
                {
                    case "ru":
                        return "Уровень сглаживания";
                    default:
                        return "&Dithering level:";
                }
            }
        }

        internal static string TransparencyThreshold
        {
            get
            {
                switch (uiCulture)
                {
                    case "ru":
                        return "Порог прозрачности:";
                    default:
                        return "&Transparency threshold:";
                }
            }
        }

        internal static string TransparencyThresholdDescription
        {
            get
            {
                switch (uiCulture)
                {
                    case "ru":
                        return "Пиксели с альфа-значением меньше порога будут полностью прозрачными.";
                    default:
                        return "Pixels with an alpha value less than the threshold will be fully transparent.";
                }
            }
        }

        internal static string GrayScaleWithAlpha
        {
            get
            {
                switch (uiCulture)
                {
                    case "ru":
                        return "Оттенки серого с альфа";
                    default:
                        return "Gr&ayscale with alpha";
                }
            }
        }

        internal static string GrayScaleWithAlphaDescription
        {
            get
            {
                switch (uiCulture)
                {
                    case "ru":
                        return "Черно-белый с прозрачностью";
                    default:
                        return "Black/white with transparency";
                }
            }
        }

        internal static string RgbWithAlpha
        {
            get
            {
                switch (uiCulture)
                {
                    case "ru":
                        return "RGB с альфа-каналом";
                    default:
                        return "RG&B with alpha";
                }
            }
        }

        internal static string RgbWithAlphaDescription
        {
            get
            {
                switch (uiCulture)
                {
                    case "ru":
                        return "RGB с прозрачностью (всегда без потерь)";
                    default:
                        return "RGB with transparency (always lossless)";
                }
            }
        }

        internal static string Compression
        {
            get
            {
                switch (uiCulture)
                {
                    case "ru":
                        return "Сжатия";
                    default:
                        return "Compression";
                }
            }
        }

        internal static string CompressionDescription
        {
            get
            {
                switch (uiCulture)
                {
                    case "ru":
                        return "Это пресеты OptiPNG. По сути они говорят OptiPNG, сколько грубой силы использовать.\r\nБолее высокие значения не всегда могут давать меньшие размеры,\r\nесли нижняя предустановка уже имеет оптимальные настройки.";
                    default:
                        return "These are OptiPNG presets. They essentially tell OptiPNG how much brute force to use.\r\nHigher values may not always give smaller sizes if a lower preset already has optimal settings.";
                }
            }
        }

        internal static string Optimize
        {
            get
            {
                switch (uiCulture)
                {
                    case "ru":
                        return "Оптимизировать";
                    default:
                        return "&Optimize";
                }
            }
        }

        internal static string OptimizeDescription
        {
            get
            {
                switch (uiCulture)
                {
                    case "ru":
                        return "Снимите этот флажок, если требуется только предосмотр.\r\nВы можете безопасно завершить оптимизацию закрыв окно консоли.";
                    default:
                        return "Uncheck this if you just want to preview.\r\nYou can safely end optimization by closing the console window.";
                }
            }
        }

        internal static string Interlace
        {
            get
            {
                switch (uiCulture)
                {
                    case "ru":
                        return "Чередование";
                    default:
                        return "&Interlace";
                }
            }
        }

        internal static string InterlaceDescription
        {
            get
            {
                switch (uiCulture)
                {
                    case "ru":
                        return "Позволяет браузерам сначала отображать версии изображения\r\nс низкой детализацией и улучшать детализацию по мере загрузки.\r\nВключение этого параметра обычно увеличивает размер файла.";
                    default:
                        return "Allows browsers to display low detail versions of an image first and improve the detail as more is downloaded.\r\nTurning this on generally increases the file size.";
                }
            }
        }

        internal static string QuietMode
        {
            get
            {
                switch (uiCulture)
                {
                    case "ru":
                        return "Тихий режим";
                    default:
                        return "&Quiet mode";
                }
            }
        }

        internal static string QuietModeDescription
        {
            get
            {
                switch (uiCulture)
                {
                    case "ru":
                        return "С помощью этого флажка optipng.exe будет запущен в тихом режиме.";
                    default:
                        return "With this check box checked optipng.exe will be launched in quiet mode.";
                }
            }
        }
    }
}
