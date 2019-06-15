using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ColorPicker
{
    /// <summary>
    /// TriangleColorPicker.xaml の相互作用ロジック
    /// </summary>
    public partial class TriangleColorPicker : ColorPickerBase
    {
        private readonly SolidColorBrush _brush = new SolidColorBrush();
        private readonly SolidColorBrush _brushBaseColor = new SolidColorBrush();

        private bool _colorUpdating;

        public TriangleColorPicker()
        {
            InitializeComponent();
            _image.Source = MakeHueRountRect();
            Pointer.Fill = _brush;
            _baseColor.Fill = _brushBaseColor;

            SyncColor(true);
            MouseDown += TriangleColorPicker_MouseDown;
            MouseMove += TriangleColorPicker_MouseMove;
            MouseUp += TriangleColorPicker_MouseUp;
        }

        private void TriangleColorPicker_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ReleaseMouseCapture();
        }

        private void TriangleColorPicker_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed)
                return;

            var (s, v) = ToSV(_canvas.ActualWidth, _canvas.ActualHeight, e.GetPosition(_canvas));

            Saturation = s;
            Brightness = v;
        }

        private void TriangleColorPicker_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var (s, v) = ToSV(_canvas.ActualWidth, _canvas.ActualHeight, e.GetPosition(_canvas));

            Saturation = s;
            Brightness = v;

            CaptureMouse();
        }

        private (double s, double v) ToSV(double width, double height, Point location)
        {
            var s = Math.Max(0.0, Math.Min(1.0, location.X / width));
            var y = Math.Max(0.0, Math.Min(1.0, 1.0 - location.Y / height));
            double v = s >= 1.0 ? 1.0 : Math.Max(0.0, Math.Min(1.0, (y - s / 2) / Math.Min(1.0, 1.0 - s)));
            return (s, v);
        }

        private Point ToLocation(double width, double height, double s, double v)
        {
            double x = s * width;

            double y = (1.0 - Math.Max(0.0, Math.Min(1.0, v * (1.0 - s) + s / 2))) * height;
            return new Point(x, y);
        }

        private BitmapSource MakeHueRountRect()
        {
            const int height = 128;
            const int width = 128;

            var wb = new WriteableBitmap(width, height, 96, 96, PixelFormats.Bgra32, null);
            int stride = wb.BackBufferStride;
            byte[] pixels = new byte[height * stride];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var p = y * stride + x * 4;
                    var (s, v) = ToSV(width, height, new Point(x, y));

                    pixels[p + 2] = pixels[p + 1] = pixels[p] = (byte)Math.Min(255, Math.Max(0, v * 255));
                    pixels[p + 3] = (byte)Math.Min(255, Math.Max(0, (1.0 - s) * 255));
                }
            }
            wb.WritePixels(new Int32Rect(0, 0, width, height), pixels, stride, 0);
            return wb;
        }

        protected override void SyncColor(bool currentChanged)
        {
            if (_colorUpdating)
                return;

            try
            {
                _colorUpdating = true;
                if (currentChanged)
                {
                    var color = HsvColor.FromColor(CurrentColor);
                    if (color.S > 0)
                        Hue = color.H;

                    Saturation = color.S;
                    Brightness = color.V;
                }
                else
                {
                    CurrentColor = new HsvColor((float)Hue, (float)Saturation, (float)Brightness).ToRgb();
                }
                BaseColor = new HsvColor((float)Hue, 1.0f, 1.0f).ToRgb();
                var saturation = Saturation;

                var location = ToLocation(_canvas.ActualWidth, _canvas.ActualHeight, Saturation, Brightness);

                Canvas.SetLeft(Current, location.X - 8.0);
                Canvas.SetTop(Current, location.Y - 8.0);
                _brush.Color = CurrentColor;
                _brushBaseColor.Color = BaseColor;
            }
            finally
            {
                _colorUpdating = false;
            }
        }
    }
}