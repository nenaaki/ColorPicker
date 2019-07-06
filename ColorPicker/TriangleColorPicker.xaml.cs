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
    public partial class TriangleColorPicker : HsvColorPickerBase
    {
        private readonly SolidColorBrush _brush = new SolidColorBrush();

        private Updater _colorUpdater = new Updater();

        public TriangleColorPicker()
        {
            InitializeComponent();
            _image.Source = MakeSvRect();

            SyncColor(null);
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

            UpdateCurrentColorFromCanvas(e.GetPosition(_canvas));
        }

        private void TriangleColorPicker_MouseDown(object sender, MouseButtonEventArgs e)
        {
            UpdateCurrentColorFromCanvas(e.GetPosition(_canvas));
            CaptureMouse();
        }

        private void UpdateCurrentColorFromCanvas(Point position)
        {
            CurrentColor = ToColor(_canvas.Width, _canvas.Height, position, BaseColor);
        }

        private (double s, double v) ToSV(double width, double height, Point location)
        {
            var x = location.X / width;
            var y = location.Y / height;

            var v = Math.Max(0, Math.Min(1, 1 - y + x / 2));
            var s = Math.Max(0, Math.Min(1, y + x / 2));
            return (s, v);
        }

        private Color ToColor(double width, double height, Point location, Color baseColor)
        {
            var (s, v) = ToSV(width, height, location);

            return new HsvColor(HsvColor.FromColor(BaseColor).H, s, v).ToColor();
        }

        /// <summary>
        /// 色から三角形ピッカー上の座標を計算します。
        /// </summary>
        private Point ToLocation(double width, double height, Color color)
        {
            var hsvColor = HsvColor.FromColor(color);
            var s = hsvColor.S;
            var v = hsvColor.V;

            return new Point(
                width * Math.Max(0, Math.Min(1, s - 1 + v)),
                height * Math.Max(0, Math.Min(1, (s + 1 - v) / 2)));
        }

        /// <summary>
        /// SV色空間の矩形を作ります。
        /// </summary>
        private BitmapSource MakeSvRect()
        {
            const int BMP_WIDTH = 128;
            const int BMP_HEIGHT = 128;

            var wb = new WriteableBitmap(BMP_WIDTH, BMP_HEIGHT, 96, 96, PixelFormats.Bgra32, null);
            int stride = wb.BackBufferStride;
            byte[] pixels = new byte[BMP_HEIGHT * stride];

            for (int y = 0; y < BMP_HEIGHT; y++)
            {
                for (int x = 0; x < BMP_WIDTH; x++)
                {
                    var p = y * stride + x * 4;
                    var (s, v) = ToSV(BMP_WIDTH, BMP_HEIGHT, new Point(x, y));

                    pixels[p + 2] = pixels[p + 1] = pixels[p] = (byte)Math.Min(255, Math.Max(0, v * 255));
                    pixels[p + 3] = (byte)Math.Min(255, Math.Max(0, (1.0 - s) * 255));
                }
            }
            wb.WritePixels(new Int32Rect(0, 0, BMP_WIDTH, BMP_HEIGHT), pixels, stride, 0);
            return wb;
        }

        /// <summary>
        /// 色同期処理です。
        /// </summary>
        protected override void SyncColorCore(string propertyName)
        {
            _colorUpdater.Update(() =>
            {
                var location = ToLocation(100, 100, CurrentColor);

                Canvas.SetLeft(Current, location.X - 8.0);
                Canvas.SetTop(Current, location.Y - 8.0);
            });
        }
    }
}