using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ColorPicker
{
    /// <summary>
    /// RingColorPicker.xaml の相互作用ロジック
    /// </summary>
    public partial class RingColorPicker : ColorPickerBase
    {
        private const int width = 128;
        private const int height = 128;

        public RingColorPicker()
        {
            InitializeComponent();

            Ring.Source = MakeHueRountRect(width, height);

            double ringWidth = 10;
            var pg = new PathGeometry();
            pg.AddGeometry(new EllipseGeometry(new Rect(0, 0, width, height)));
            double xCenter = width / 2.0;
            double yCenter = height / 2.0;
            double xRadius = xCenter - ringWidth;
            double yRadius = yCenter - ringWidth;
            pg.AddGeometry(new EllipseGeometry(new Point(xCenter, yCenter), xRadius, yRadius));

            Ring.Clip = pg;
            SyncColor(true);
        }

        private BitmapSource MakeHueRountRect(int width, int height)
        {
            var wb = new WriteableBitmap(width, height, 96, 96, PixelFormats.Rgb24, null);
            int stride = wb.BackBufferStride;
            byte[] pixels = new byte[height * stride * 8];

            int xDiff = width / 2;
            int yDiff = height / 2;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    double radian = Math.Atan2(y - yDiff, x - xDiff);
                    var c = HsvColor.ToRgb(radian, 1.0, 1.0);
                    var p = y * stride + x * 3;
                    pixels[p] = c.R;
                    pixels[p + 1] = c.G;
                    pixels[p + 2] = c.B;
                }
            }
            wb.WritePixels(new Int32Rect(0, 0, width, height), pixels, stride, 0);
            return wb;
        }

        private SolidColorBrush _brush = new SolidColorBrush();

        private bool _colorUpdating;

        protected override void SyncColor(bool currentChanged)
        {
            if (_colorUpdating)
                return;

            double xCenter = width / 2.0;
            double yCenter = height / 2.0;

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
                    BaseColor = new HsvColor(Hue, 1.0, 1.0).ToRgb();
                }
                else
                {
                    BaseColor = new HsvColor(Hue, Saturation, Brightness).ToRgb();
                }
                Canvas.SetLeft(Current, (Math.Cos(Hue) * 0.9 + 1.0) * xCenter - 8.0);
                Canvas.SetTop(Current, (Math.Sin(Hue) * 0.9 + 1.0) * yCenter - 8.0);
                Pointer.Fill = _brush;
                _brush.Color = BaseColor;
            }
            finally
            {
                _colorUpdating = false;
            }
        }
    }
}