using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ColorPicker.Converters
{
    public class ColorToGradualColorsConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is Color color1 && int.TryParse(values[1].ToString(), out int stepCount))
            {
                Color color2;

                if (color1 == Colors.Black)
                {
                    color2 = Colors.White;
                }
                else if (color1 == Colors.White)
                {
                    color2 = Colors.Black;
                }
                else
                {
                    // MEMO : グレースケール化したときの明度が50%未満かで白に向けるか黒に向けるかを切り替える。
                    var brightness = HsvColor.FromColor(color1).GetBrightness();
                    color2 = (brightness < 0.5) ? Colors.White : Colors.Black;
                }

                var colors = new Color[stepCount + 1];
                for (int idx = 0; idx <= stepCount; idx++)
                {
                    float ratio = idx / (float)(stepCount + 1);
                    colors[idx] = Color.FromRgb(
                        (byte)(color2.R * ratio + color1.R * (1 - ratio)),
                        (byte)(color2.G * ratio + color1.G * (1 - ratio)),
                        (byte)(color2.B * ratio + color1.B * (1 - ratio)));
                }
                return colors;
            }
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            if (value is Color[] colors && colors.Length > 0)
                return new object[] { colors[0], colors.Length };

            return new object[] { Colors.Transparent, 0 };
        }
    }
}
