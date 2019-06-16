using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

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
            FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
            (d, e) =>
            {
                ((HexColorPicker)d).SyncColor();
            }));

        private readonly List<Path> _pathList = new List<Path>();

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
                        double cx = 128 + s * sx + h2 * h2x;
                        double cy = 128 + s * sy + h2 * h2y;

                        var path = new Path();
                        path.Data = sg;
                        path.StrokeThickness = 1.0;
                        path.Fill = new SolidColorBrush() { Color = HsvColor.ToRgb((float)((h1 + h2 / (double)s) * Math.PI / 3), s / 6.0f, 1.0f) };
                        Canvas.SetLeft(path, cx);
                        Canvas.SetTop(path, cy);
                        canvas.Children.Add(path);

                        path.MouseDown += Path_MouseDown;
                        path.MouseMove += Path_MouseMove;
                        _pathList.Add(path);
                    }
                }
            }
        }

        private void Path_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed)
                return;

            CurrentColor = ((SolidColorBrush)((Path)sender).Fill).Color;
        }

        private void Path_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            CurrentColor = ((SolidColorBrush)((Path)sender).Fill).Color;
        }

        private void SyncColor()
        {
            ClearSelction();
            var currentColor = CurrentColor;
            var selectedPath = _pathList.FirstOrDefault(path => ((SolidColorBrush)((Path)path).Fill).Color == currentColor);
            if (selectedPath != null)
                selectedPath.Stroke = Brushes.Black;
        }

        private void ClearSelction()
        {
            foreach (var path in _pathList)
            {
                path.Stroke = null;
            }
        }
    }
}