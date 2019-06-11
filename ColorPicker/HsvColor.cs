using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ColorPicker
{
    public readonly struct HsvColor
    {
        public readonly double H; // 0-360

        public readonly double S;

        public readonly double V;

        public HsvColor(double hue, double saturation, double brightness)
        {
            H = hue;
            S = saturation;
            V = brightness;
        }

        public static HsvColor FromRgb(byte r, byte g, byte b)
        {
            byte max = Math.Max(r, Math.Max(g, b));
            byte min = Math.Min(r, Math.Min(g, b));

            double hue, saturation;
            if (max == min)
            {
                // 無彩色
                hue = 0.0;
                saturation = 0.0;
            }
            else
            {
                double c = max - min;

                // 色彩
                if (max == r)
                    hue = ((double)g - b) / c;
                else if (max == g)
                    hue = ((double)b - r) / c + 2.0;
                else
                    hue = ((double)r - g) / c + 4.0;

                var pi2 = 2 * Math.PI;
                hue *= pi2 / 6;
                if (hue < 0.0)
                    hue += pi2;

                saturation = c / max;
            }
            return new HsvColor(hue, saturation, max / 255.0);
        }

        public static HsvColor Blend(in HsvColor color1, in HsvColor color2, double ratio)
        {
            double ratio2 = 1.0 - ratio;
            return new HsvColor(
                color1.H * ratio2 + color2.H * ratio,
                color1.S * ratio2 + color2.S * ratio,
                color1.V * ratio2 + color2.V * ratio);
        }  

        public Color ToRgb()
        {
            var vb = ToByte(255 * V);
            if (S == 0)
            {
                return Color.FromRgb(vb, vb, vb);
            }
            var hr = H / (2 * Math.PI);
            var hd = (float)(6 * (hr - Math.Floor(hr)));
            var hb = (int)Math.Floor(hd);
            var p = ToByte(vb * (1 - S));
            var q = ToByte(vb * (1 - S * (hd - hb)));
            var t = ToByte(vb * (1 - S * (1 - hd + hb)));

            switch (hb)
            {
                case 0:
                    return Color.FromRgb(vb, t, p);
                case 1:
                    return Color.FromRgb(q, vb, p);
                case 2:
                    return Color.FromRgb(p, vb, t);
                case 3:
                    return Color.FromRgb(p, q, vb);
                case 4:
                    return Color.FromRgb(t, p, vb);
                default:
                    return Color.FromRgb(vb, p, q);
            }
        }

        public static Color ToRgb(double h, double s, double v)
        {
            return new HsvColor(h, s, v).ToRgb();
        }

        private static byte ToByte(double d)
            => d < 0 ? (byte)0 : d > 255 ? (byte)255 : (byte)Math.Round(d);
    }
}