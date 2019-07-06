using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ColorPicker
{
    /// <summary>
    /// RgbEditor.xaml の相互作用ロジック
    /// </summary>
    public partial class RgbEditor : UserControl
    {
        public byte Red
        {
            get => (byte)GetValue(RedProperty);
            set => SetValue(RedProperty, value);
        }
        public static readonly DependencyProperty RedProperty
            = DependencyProperty.Register(nameof(Red), typeof(byte), typeof(RgbEditor), new FrameworkPropertyMetadata((byte)0,
            FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
            (d, e) => ((RgbEditor)d).SyncColor(e.Property.Name)));

        public byte Blue
        {
            get => (byte)GetValue(BlueProperty);
            set => SetValue(BlueProperty, value);
        }
        public static readonly DependencyProperty BlueProperty
            = DependencyProperty.Register(nameof(Blue), typeof(byte), typeof(RgbEditor), new FrameworkPropertyMetadata((byte)0,
            FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
            (d, e) => ((RgbEditor)d).SyncColor(e.Property.Name)));

        public byte Green
        {
            get => (byte)GetValue(GreenProperty);
            set => SetValue(GreenProperty, value);
        }
        public static readonly DependencyProperty GreenProperty
            = DependencyProperty.Register(nameof(Green), typeof(byte), typeof(RgbEditor), new FrameworkPropertyMetadata((byte)0,
            FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
            (d, e) => ((RgbEditor)d).SyncColor(e.Property.Name)));

        public int Saturation
        {
            get => (int)GetValue(SaturationProperty);
            set => SetValue(SaturationProperty, value);
        }
        public static readonly DependencyProperty SaturationProperty
            = DependencyProperty.Register(nameof(Saturation), typeof(int), typeof(RgbEditor), new FrameworkPropertyMetadata(0,
            FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
            (d, e) => ((RgbEditor)d).SyncColor(e.Property.Name)), e => IntRangeRule((int)e, 0, 100));

        public int Brightness
        {
            get => (int)GetValue(BrightnessProperty);
            set => SetValue(BrightnessProperty, value);
        }
        public static readonly DependencyProperty BrightnessProperty
            = DependencyProperty.Register(nameof(Brightness), typeof(int), typeof(RgbEditor), new FrameworkPropertyMetadata(0,
            FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
            (d, e) => ((RgbEditor)d).SyncColor(e.Property.Name)), e => IntRangeRule((int)e, 0, 100));

        public int Hue
        {
            get => (int)GetValue(HueProperty);
            set => SetValue(HueProperty, value);
        }
        public static readonly DependencyProperty HueProperty
            = DependencyProperty.Register(nameof(Hue), typeof(int), typeof(RgbEditor), new FrameworkPropertyMetadata(0,
            FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
            (d, e) => ((RgbEditor)d).SyncColor(e.Property.Name)), e => IntRangeRule((int)e, 0, 360));

        public Color BaseColor
        {
            get => (Color)GetValue(BaseColorProperty);
            set => SetValue(BaseColorProperty, value);
        }
        public static readonly DependencyProperty BaseColorProperty
            = DependencyProperty.Register(nameof(BaseColor), typeof(Color), typeof(RgbEditor), new FrameworkPropertyMetadata(Colors.Black,
            FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
            (d, e) => ((RgbEditor)d).SyncColor(e.Property.Name)));


        public Color CurrentColor
        {
            get => (Color)GetValue(CurrentColorProperty);
            set => SetValue(CurrentColorProperty, value);
        }
        public static readonly DependencyProperty CurrentColorProperty
            = DependencyProperty.Register(nameof(CurrentColor), typeof(Color), typeof(RgbEditor), new FrameworkPropertyMetadata(Colors.Black,
            FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
            (d, e) => ((RgbEditor)d).SyncColor(e.Property.Name)));

        public RgbEditor()
        {
            InitializeComponent();
        }

        private Updater _updater;

        private void SyncColor(string propertyName)
        {
            _updater.Update(() =>
            {
                switch (propertyName)
                {
                    case nameof(Red):
                    case nameof(Green):
                    case nameof(Blue):
                        {
                            CurrentColor = Color.FromRgb(Red, Green, Blue);
                            var hsv = HsvColor.FromColor(CurrentColor);
                            if (!hsv.IsAchromatic())
                            {
                                BaseColor = new HsvColor(hsv.H, 1.0, 1.0).ToColor();
                                Hue = (int)Math.Round(hsv.H / (Math.PI * 2) * 360);
                            }
                            Saturation = (int)Math.Round(hsv.S * 100);
                            Brightness = (int)Math.Round(hsv.V * 100);
                        }
                        break;

                    case nameof(Hue):
                    case nameof(Brightness):
                    case nameof(Saturation):
                        {
                            if(propertyName == nameof(Brightness))
                            {
                                var brightness = Brightness;
                                var total = brightness + Saturation;
                                if(brightness + Saturation < 100)
                                {
                                    Saturation = 100 - brightness;
                                }
                            }
                            else if(propertyName == nameof(Saturation))
                            {
                                var saturation = Saturation;
                                var total = Brightness + saturation;
                                if (Brightness + saturation < 100)
                                {
                                    Brightness = 100 - saturation;
                                }
                            }
                            var hsv = new HsvColor((double)Hue * (Math.PI * 2) / 360, (double)Saturation / 100, (double)Brightness / 100);
                            var color = hsv.ToColor();
                            CurrentColor = color;
                            if(propertyName == nameof(Hue))
                            {
                                BaseColor = new HsvColor(hsv.H, 1.0, 1.0).ToColor();
                            }
                            Red = color.R;
                            Green = color.G;
                            Blue = color.B;
                        }
                        break;

                    default:
                        {
                            var current = CurrentColor;
                            Red = current.R;
                            Green = current.G;
                            Blue = current.B;
                            var hsv = HsvColor.FromColor(CurrentColor);
                            if (!hsv.IsAchromatic())
                            {
                                BaseColor = new HsvColor(hsv.H, 1.0, 1.0).ToColor();
                                Hue = (int)Math.Round(hsv.H / (Math.PI * 2) * 360);
                            }
                            Saturation = (int)Math.Round(hsv.S * 100);
                            Brightness = (int)Math.Round(hsv.V * 100);
                        }
                        break;
                }
            });
        }

        private static bool IntRangeRule(int value, int minValue, int maxValue)
        {
            return minValue <= value && value <= maxValue;
        }
    }
}