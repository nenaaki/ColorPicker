using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ColorPicker
{
    /// <summary>
    /// HexColorPicker.xaml の相互作用ロジック
    /// </summary>
    public partial class HexColorPicker : UserControl
    {
        public HexColorPicker()
        {
            InitializeComponent();

            // 個々の6角形の形状を作成
            var hexRadius = 10;
            var pi3 = Math.PI / 3;

            var sg = new StreamGeometry();
            using (var c = sg.Open())
            {
                for (var i = 0; i < 6; i++)
                {
                    var p = new Point(hexRadius * Math.Sin(i * pi3), hexRadius * Math.Cos(i * pi3));
                    if (i == 0)
                    {
                        c.BeginFigure(p, true, true);
                    }
                    else
                    {
                        c.LineTo(p, true, false);
                    }
                }
            }
            sg.Freeze();

            var sqrt3 = Math.Sqrt(3);

            // 6角形を6個の正三角形に分けます
            for (int h1 = 0; h1 < 6; h1++)
            {
                // 単位移動量の計算
                var sx = sqrt3 * hexRadius * Math.Cos(h1 * pi3);
                var sy = sqrt3 * hexRadius * Math.Sin(h1 * pi3);
                var h2x = sqrt3 * hexRadius * Math.Cos((h1 + 2) * pi3);
                var h2y = sqrt3 * hexRadius * Math.Sin((h1 + 2) * pi3);

                // 彩度(半径方向)の繰り返し
                for (int s = 0; s < 7; s++)
                {
                    // 色相(角度方向)の繰り返し
                    for (int h2 = 0; h2 < s || h1 == 0 && s == 0 && h2 == 0; h2++)
                    {
                        // 中心位置の計算
                        double cx = 128 + s * sx + h2 * h2x;
                        double cy = 128 + s * sy + h2 * h2y;

                        // 6角形の追加
                        var path = new Path();
                        path.Data = sg;
                        path.StrokeThickness = 1.0;
                        path.Fill = new SolidColorBrush() { Color = HsvColor.ToRgb((float)((h1 + h2 / (double)s) * Math.PI / 3), s / 6.0f, 1.0f) };
                        Canvas.SetLeft(path, cx);
                        Canvas.SetTop(path, cy);
                        canvas.Children.Add(path);
                    }
                }
            }
        }
    }
}