using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Reflection;
using System.Globalization;

namespace Prakt13
{
    public class ColorUtils
    {
        public static List<Color> GetRainbowColors(int colorCount)
        {
            List<Color> ret = new List<Color>(colorCount);

            double p = 360.0 / (double)colorCount;

            for (int n = 0; n < colorCount; n++)
            {
                ret.Add(ColorUtils.HsvToRgb(n * p, 1.0, 1.0));
            }

            return ret;
        }
        public static Color HsvToRgb(double h, double s, double v)
        {
            int hi = (int)Math.Floor(h / 60.0) % 6;
            double f = (h / 60.0) - Math.Floor(h / 60.0);

            double p = v * (1.0 - s);
            double q = v * (1.0 - (f * s));
            double t = v * (1.0 - ((1.0 - f) * s));

            Color ret;

            switch (hi)
            {
                case 0:
                    ret = ColorUtils.GetRgb(v, t, p);
                    break;
                case 1:
                    ret = ColorUtils.GetRgb(q, v, p);
                    break;
                case 2:
                    ret = ColorUtils.GetRgb(p, v, t);
                    break;
                case 3:
                    ret = ColorUtils.GetRgb(p, q, v);
                    break;
                case 4:
                    ret = ColorUtils.GetRgb(t, p, v);
                    break;
                case 5:
                    ret = ColorUtils.GetRgb(v, p, q);
                    break;
                default:
                    ret = Color.FromArgb(0xFF, 0x00, 0x00, 0x00);
                    break;
            }
            return ret;
        }
        public static Color GetRgb(double r, double g, double b)
        {
            return Color.FromArgb(255, (byte)(r * 255.0), (byte)(g * 255.0), (byte)(b * 255.0));
        }
    }
}
