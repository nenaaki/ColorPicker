using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Oniqys.Wpf.Controls.ColorPicker
{
    /// <summary>
    /// HexColorPicker.xaml の相互作用ロジック
    /// </summary>
    public partial class HexColorPicker : UserControl
    {
        public Color CurrentColor
        {
            get => (Color)GetValue(CurrentColorProperty);
            set => SetValue(CurrentColorProperty, value);
        }
        public static readonly DependencyProperty CurrentColorProperty =
            DependencyProperty.Register(nameof(CurrentColor), typeof(Color), typeof(HexColorPicker), new FrameworkPropertyMetadata(Colors.White,
            FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public HexColorPicker()
        {
            InitializeComponent();

            const double CENTER_X = 112.0;
            const double CENTER_Y = 112.0;

            // 個々の6角形の形状を作成
            const double hexRadius = 10.0;
            const double pi3 = Math.PI / 3;
            double sqrt3 = Math.Sqrt(3);

            for (int h1 = 0; h1 < 6; h1++)
            {
                var sx = sqrt3 * hexRadius * Math.Cos(h1 * pi3);
                var sy = sqrt3 * hexRadius * Math.Sin(h1 * pi3);
                var h2x = sqrt3 * hexRadius * Math.Cos((h1 + 2) * pi3);
                var h2y = sqrt3 * hexRadius * Math.Sin((h1 + 2) * pi3);

                for (int s = 0; s < 7; s++)
                {
                    for (int h2 = 0; h2 < s || h1 == 0 && s == 0 && h2 == 0; h2++)
                    {
                        double cx = CENTER_X + s * sx + h2 * h2x;
                        double cy = CENTER_Y + s * sy + h2 * h2y;

                        var item = new HexColorItem { SourceColor = HsvColor.ToColor((float)((h1 + h2 / (double)s) * pi3), s / 6.0f, 1.0f) };

                        Canvas.SetLeft(item, cx);
                        Canvas.SetTop(item, cy);
                        canvas.Children.Add(item);

                        item.SetBinding(ColorItemBase.CurrentColorProperty, new Binding(nameof(CurrentColor)) { Source = this });
                    }
                }
            }
        }
    }
}