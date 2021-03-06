﻿using System;
using System.Windows.Media;

namespace Oniqys.Wpf.Controls.ColorPicker
{
    /// <summary>
    /// HSV色を表現するクラスです。
    /// </summary>
    public readonly struct HsvColor
    {
        /// <summary>
        /// 色相
        /// </summary>
        public readonly double H;

        /// <summary>
        /// 彩度(0-1)
        /// </summary>
        public readonly double S;

        /// <summary>
        /// 明度(0-1)
        /// </summary>
        public readonly double V;

        /// <summary>
        /// コンストラクターです。
        /// </summary>
        /// <param name="hue">色相</param>
        /// <param name="saturation">彩度</param>
        /// <param name="brightness">明度</param>
        public HsvColor(double hue, double saturation, double brightness)
        {
            H = hue;
            S = saturation;
            V = brightness;
        }

        /// <summary>
        /// <see cref="Color"/>から生成します。
        /// </summary>
        public static HsvColor FromColor(Color color)
            => FromRgb(color.R, color.G, color.B);

        /// <summary>
        /// RGB色から生成します。
        /// </summary>
        public static HsvColor FromRgb(byte r, byte g, byte b)
        {
            byte max = Math.Max(r, Math.Max(g, b));
            byte min = Math.Min(r, Math.Min(g, b));

            double hue, saturation;
            if (max == min)
            {
                // 無彩色
                hue = 0.0;
                saturation = min == 0 ? 1.0 : 0.0;
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

        /// <summary>
        /// 無彩色かどうか判定します。
        /// </summary>
        public bool IsAchromatic() => V + S <= 1.0;

        public static HsvColor Blend(in HsvColor color1, in HsvColor color2, double ratio)
        {
            double ratio2 = 1.0 - ratio;
            return new HsvColor(
                color1.H * ratio2 + color2.H * ratio,
                color1.S * ratio2 + color2.S * ratio,
                color1.V * ratio2 + color2.V * ratio);
        }

        /// <summary>
        /// <see cref="Color"/>に変換します。
        /// </summary>
        public Color ToColor()
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

        /// <summary>
        /// <see cref="Color"/>に変換します。
        /// </summary>
        public static Color ToRgb(double h, double s, double v)
        {
            return new HsvColor(h, s, v).ToColor();
        }

        /// <summary>
        /// ITU-R Rec BT.601 によるグレースケールを行い、明度を取得します。
        /// </summary>
        public double GetBrightness()
        {
            var color = ToColor();
            return (0.299 * color.R + 0.587 * color.G + 0.114 * color.B) / 255.0;
        }

        /// <summary>
        /// byteの範囲に丸め、四捨五入します。
        /// </summary>
        private static byte ToByte(double d)
            => d < 0 ? (byte)0 : d > 255 ? (byte)255 : (byte)Math.Round(d);
    }
}