using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Input;

namespace ColorPicker
{
    /// <summary>
    /// RingColorPicker.xaml の相互作用ロジック
    /// </summary>
    public partial class RingColorPicker : HsvColorPickerBase
    {
        private const int CANVAS_WIDTH = 128;
        private const int CANVAS_HEIGHT = 128;
        private const double RING_THICKNESS = 10;

        private readonly SolidColorBrush _brush = new SolidColorBrush();

        private static BitmapSource _colorMap;

        public RingColorPicker()
        {
            InitializeComponent();

            if (_colorMap == null)
                _colorMap = MakeHueRountRect(CANVAS_WIDTH, CANVAS_HEIGHT);

            Ring.Source = _colorMap;
            Pointer.Fill = _brush;

            var pg = new PathGeometry();
            pg.AddGeometry(new EllipseGeometry(new Rect(0, 0, CANVAS_WIDTH, CANVAS_HEIGHT)));
            double xCenter = CANVAS_WIDTH / 2.0;
            double yCenter = CANVAS_HEIGHT / 2.0;
            double xRadius = xCenter - RING_THICKNESS;
            double yRadius = yCenter - RING_THICKNESS;
            pg.AddGeometry(new EllipseGeometry(new Point(xCenter, yCenter), xRadius, yRadius));

            Ring.Clip = pg;
            SyncColor(true);

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

            UpdateColor(e.GetPosition(_canvas));
        }

        private void RingColorPicker_MouseDown(object sender, MouseButtonEventArgs e)
        {
            UpdateColor(e.GetPosition(_canvas));

            CaptureMouse();
        }

        private void UpdateColor(Point position)
        {
            var radian = -Math.Atan2(position.X - _canvas.ActualWidth / 2, position.Y - _canvas.ActualHeight / 2) + Math.PI / 2;
            CurrentColor = HsvColor.ToRgb(radian, Saturation, Brightness);
        }

        /// <summary>
        /// 色相環のBitmapを生成します。
        /// </summary>
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
                    float radian = (float)Math.Atan2(y - yDiff, x - xDiff);
                    var c = HsvColor.ToRgb(radian, 1.0f, 1.0f);
                    var p = y * stride + x * 3;
                    pixels[p] = c.R;
                    pixels[p + 1] = c.G;
                    pixels[p + 2] = c.B;
                }
            }
            wb.WritePixels(new Int32Rect(0, 0, width, height), pixels, stride, 0);
            return wb;
        }

        private bool _colorUpdating;

        protected override void SyncColor(bool currentChanged)
        {
            if (_colorUpdating)
                return;

            double xCenter = CANVAS_WIDTH / 2.0;
            double yCenter = CANVAS_HEIGHT / 2.0;

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
                    BaseColor = new HsvColor((float)Hue, 1.0f, 1.0f).ToRgb();
                }
                else
                {
                    BaseColor = new HsvColor((float)Hue, (float)Saturation, (float)Brightness).ToRgb();
                }
                Canvas.SetLeft(Current, (Math.Cos(Hue) * 0.92 + 1.0) * xCenter - 8.0);
                Canvas.SetTop(Current, (Math.Sin(Hue) * 0.92 + 1.0) * yCenter - 8.0);
                _brush.Color = BaseColor;
            }
            finally
            {
                _colorUpdating = false;
            }
        }
    }
}