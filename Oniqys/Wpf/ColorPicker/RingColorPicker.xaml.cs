using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Oniqys.Wpf.Controls.ColorPicker
{
    /// <summary>
    /// RingColorPicker.xaml の相互作用ロジック
    /// </summary>
    public partial class RingColorPicker : HsvColorPickerBase
    {
        private const int CANVAS_WIDTH = 128;
        private const int CANVAS_HEIGHT = 128;
        private const double RING_THICKNESS = 10;
        private const double X_CENTER = CANVAS_WIDTH / 2.0;
        private const double Y_CENTER = CANVAS_HEIGHT / 2.0;

        private static BitmapSource _colorMap;

        public RingColorPicker()
        {
            InitializeComponent();

            if (_colorMap == null)
                _colorMap = MakeHueRountRect(CANVAS_WIDTH, CANVAS_HEIGHT);

            Ring.Source = _colorMap;

            var pg = new PathGeometry();
            pg.AddGeometry(new EllipseGeometry(new Rect(0, 0, CANVAS_WIDTH, CANVAS_HEIGHT)));
            double xCenter = CANVAS_WIDTH / 2.0;
            double yCenter = CANVAS_HEIGHT / 2.0;
            double xRadius = xCenter - RING_THICKNESS;
            double yRadius = yCenter - RING_THICKNESS;
            pg.AddGeometry(new EllipseGeometry(new Point(xCenter, yCenter), xRadius, yRadius));

            Ring.Clip = pg;
            SyncColor(null);

            MouseDown += RingColorPicker_MouseDown;
            MouseMove += RingColorPicker_MouseMove;
            MouseUp += RingColorPicker_MouseUp;
        }

        private void RingColorPicker_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ReleaseMouseCapture();
        }

        private void RingColorPicker_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed)
                return;

            UpdateHue(e.GetPosition(_canvas));
        }

        private void RingColorPicker_MouseDown(object sender, MouseButtonEventArgs e)
        {
            UpdateHue(e.GetPosition(_canvas));

            CaptureMouse();
        }

        /// <summary>
        /// 色相を更新します。
        /// </summary>
        /// <param name="position"></param>
        private void UpdateHue(Point position)
            => Hue = Math.PI / 2 - Math.Atan2(position.X - _canvas.ActualWidth / 2, position.Y - _canvas.ActualHeight / 2);

        /// <summary>
        /// 色相環のBitmapを生成します。
        /// </summary>
        private static BitmapSource MakeHueRountRect(int width, int height)
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
                    float radian = (float)Math.Atan2(y - yDiff, x - xDiff);
                    var c = HsvColor.ToColor(radian, 1.0f, 1.0f);
                    var p = y * stride + x * 3;
                    pixels[p] = c.R;
                    pixels[p + 1] = c.G;
                    pixels[p + 2] = c.B;
                }
            }
            wb.WritePixels(new Int32Rect(0, 0, width, height), pixels, stride, 0);
            return wb;
        }

        private Updater _colorUpdater = new Updater();

        protected override void SyncColorCore(string propertyName)
        {
            _colorUpdater.Update(() =>
            {
                Canvas.SetLeft(Current, (Math.Cos(Hue) * 0.92 + 1.0) * X_CENTER - 8.0);
                Canvas.SetTop(Current, (Math.Sin(Hue) * 0.92 + 1.0) * Y_CENTER - 8.0);
            });
        }
    }
}