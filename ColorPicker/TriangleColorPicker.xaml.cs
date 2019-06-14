using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ColorPicker
{
    /// <summary>
    /// TriangleColorPicker.xaml の相互作用ロジック
    /// </summary>
    public partial class TriangleColorPicker : ColorPickerBase
    {
        public TriangleColorPicker()
        {
            InitializeComponent();
            _image.Source = MakeHueRountRect();
        }

        private BitmapSource MakeHueRountRect()
        {
            int height = 128;
            int width = 128;

            var wb = new WriteableBitmap(width, height, 96, 96, PixelFormats.Bgra32, null);
            int stride = wb.BackBufferStride;
            byte[] pixels = new byte[height * stride];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var p = y * stride + x * 4;
                    double dx = x / (double)width;
                    double dy = y / (double)height;

                    pixels[p + 2] = pixels[p + 1] = pixels[p] = (byte)Math.Min(255, Math.Max(0, (1.0 - dy) * 255));
                    pixels[p + 3] = (byte)Math.Min(255, Math.Max(0, (1.0 - dx) * 255));
                }
            }
            wb.WritePixels(new Int32Rect(0, 0, width, height), pixels, stride, 0);
            return wb;
        }

        private SolidColorBrush _brush = new SolidColorBrush();
        private SolidColorBrush _brushBaseColor = new SolidColorBrush();

        private bool _colorUpdating;

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
                    BaseColor = new HsvColor(Hue, 1.0, 1.0).ToRgb();
                }
                else
                {
                    BaseColor = new HsvColor(Hue, Saturation, Brightness).ToRgb();
                }
                var saturation = Saturation;

                Canvas.SetLeft(Current, (saturation * Brightness) * _canvas.ActualWidth - 8.0);
                Canvas.SetTop(Current, Math.Min((1 - Brightness) * (1 - saturation) * _canvas.ActualHeight + ((saturation + (1 - Brightness)) / 2) * _canvas.ActualHeight, _canvas.ActualHeight) - 8.0);
                Pointer.Fill = _brush;
                _brush.Color = CurrentColor;
                _baseColor.Fill = _brushBaseColor;
                _brushBaseColor.Color = BaseColor;
            }
            finally
            {
                _colorUpdating = false;
            }
        }
    }
}