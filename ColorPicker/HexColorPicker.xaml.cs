using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Data;

namespace ColorPicker
{
    /// <summary>
    /// HexColorPicker.xaml の相互作用ロジック
    /// </summary>
    public partial class HexColorPicker : UserControl
    {
        public Color CurrentColor
        {
            get { return (Color)GetValue(CurrentColorProperty); }
            set { SetValue(CurrentColorProperty, value); }
        }
        public static readonly DependencyProperty CurrentColorProperty =
            DependencyProperty.Register(nameof(CurrentColor), typeof(Color), typeof(HexColorPicker), new FrameworkPropertyMetadata(Colors.White,
            FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        private readonly List<Path> _pathList = new List<Path>();

        public HexColorPicker()
        {
            InitializeComponent();

            // 個々の6角形の形状を作成
            var hexRadius = 10;
            var pi3 = Math.PI / 3;
            var sqrt3 = Math.Sqrt(3);

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
                        double cx = 112 + s * sx + h2 * h2x;
                        double cy = 112 + s * sy + h2 * h2y;

                        var item = new HexColorItem { Value = HsvColor.ToRgb((float)((h1 + h2 / (double)s) * Math.PI / 3), s / 6.0f, 1.0f) };

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