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
                    case "de":
                        return "Optimiertes PNG";
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
                    case "de":
                        return "Heuristik (1 Durchlauf)";
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
                    case "de":
                        return "8 Durchläufe";
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
                    case "de":
                        return "16 Durchläufe";
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
                    case "de":
                        return "24 Durchläufe";
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
                    case "de":
                        return "48 Durchläufe";
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
                    case "de":
                        return "120 Durchläufe";
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
                    case "de":
                        return "240 Durchläufe";
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
                    case "de":
                        return "nutzt OptiPNG";
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
                    case "de":
                        return "Farbreduktion";
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
                    case "de":
                        return "&Graustufen";
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
                    case "de":
                        return "Schwarzweiß ohne Transparenz";
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
                    case "de":
                        return "&RGB";
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
                    case "de":
                        return "RGB ohne Transparenz";
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
                    case "de":
                        return "mit Alphakanal &multiplizieren";
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
                    case "de":
                        return "In Farbmod ohne Unterstützung für einen Alphakanal, mit diesem multiplizieren";
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
                    case "de":
                        return "Nicht mehr als 256 separate Farben";
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
                    case "de":
                        return "Rasterstufe";
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
                    case "de":
                        return "&Transparenzgrenze";
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
                    case "de":
                        return "Pixel mit einem Alphawert unterhalb der Grenze werden völlig transparent.";
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
                    case "de":
                        return "G&raustufen mit Alphakanal";
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
                    case "de":
                        return "Schwarzweiß mit Transparenz";
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
                    case "de":
                        return "RG&B mit Alphakanal";
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
                    case "de":
                        return "RGB mit Transparenz (stets verlustfrei)";
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
                    case "de":
                        return "Komprimierung";
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
                    case "de":
                        return "Diese Vorgeisntellungen bestimmen wie brachial OptiPNG arbeiten soll.\r\nHöhere Werte ergeben keine kleinere Dateigröße, falls eine niedrigere Einstellung bereits zum optimalen Ergebnis führt.";
                    default:
                        return "These presets tell OptiPNG how much brute force to use.\r\nHigher values may not always give smaller sizes if a lower preset already has optimal settings.";
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
                    case "de":
                        return "&Optimieren";
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
                    case "de":
                        return "Abwählen falls nur Vorschau erwünscht.\r\nDie Optimierung wird durch Schließen des Fensters sicher beendet.";
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
                    case "de":
                        return "Ze&ilensprung";
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
                    case "de":
                        return "Erlaubt Browsern zuerst eine geringer aufgelöste Version des Bildes anzuzeigen und mit zunehmendem Ladefortschritt mehr Details.\r\nDiese Option erhöht die Dateigröße meistens etwas.";
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
                    case "de":
                        return "Stummer Modus";
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
                        return "С помощью этого флажка OptiPNG будет запущен в тихом режиме.";
                    case "de":
                        return "Mit dieser Option werden nur schwere Fehlermeldungen von OptiPNG gemeldet.";
                    default:
                        return "With this option checked, only fatal errors will be reported from OptiPNG.";
                }
            }
        }
    }
}
