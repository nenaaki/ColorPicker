using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ColorPicker
{
    public abstract class HsvColorPickerBase : UserControl
    {
        public double Hue
        {
            get { return (double)GetValue(HueProperty); }
            set { SetValue(HueProperty, value); }
        }
        public static readonly DependencyProperty HueProperty
            = DependencyProperty.Register(nameof(Hue), typeof(double), typeof(HsvColorPickerBase), new PropertyMetadata(0.0,
            (d, e) => ((HsvColorPickerBase)d).SyncColor(e.Property.Name)));

        public double Saturation
        {
            get { return (double)GetValue(SaturationProperty); }
            set { SetValue(SaturationProperty, value); }
        }
        public static readonly DependencyProperty SaturationProperty
            = DependencyProperty.Register(nameof(Saturation), typeof(double), typeof(HsvColorPickerBase), new PropertyMetadata(1.0,
            (d, e) => ((HsvColorPickerBase)d).SyncColor(e.Property.Name)));

        public double Brightness
        {
            get { return (double)GetValue(BrightnessProperty); }
            set { SetValue(BrightnessProperty, value); }
        }
        public static readonly DependencyProperty BrightnessProperty
            = DependencyProperty.Register(nameof(Brightness), typeof(double), typeof(HsvColorPickerBase), new PropertyMetadata(1.0,
            (d, e) => ((HsvColorPickerBase)d).SyncColor(e.Property.Name)));

        public Color BaseColor
        {
            get { return (Color)GetValue(BaseColorProperty); }
            set { SetValue(BaseColorProperty, value); }
        }
        public static readonly DependencyProperty BaseColorProperty
            = DependencyProperty.Register(nameof(BaseColor), typeof(Color), typeof(HsvColorPickerBase), new PropertyMetadata(Colors.Red,
            (d, e) => ((HsvColorPickerBase)d).SyncColor(e.Property.Name)));

        public Color CurrentColor
        {
            get { return (Color)GetValue(CurrentColorProperty); }
            set { SetValue(CurrentColorProperty, value); }
        }
        public static readonly DependencyProperty CurrentColorProperty
            = DependencyProperty.Register(nameof(CurrentColor), typeof(Color), typeof(HsvColorPickerBase), new FrameworkPropertyMetadata(Colors.Red,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
            (d, e) => ((HsvColorPickerBase)d).SyncColor(e.Property.Name)));


        private Updater _colorUpdater;
        protected void SyncColor(string propertyName)
        {
            _colorUpdater.Update(() =>
            {
                switch (propertyName)
                {
                    case nameof(Hue):
                    case nameof(Saturation):
                    case nameof(Brightness):
                        BaseColor = new HsvColor(Hue, 1.0, 1.0).ToRgb();
                        CurrentColor = new HsvColor(Hue, Saturation, Brightness).ToRgb();
                        break;

                    case nameof(BaseColor):
                        Hue = HsvColor.FromColor(BaseColor).H;
                        CurrentColor = new HsvColor(Hue, Saturation, Brightness).ToRgb();
                        break;

                    case nameof(CurrentColor):
                        var hsvColor = HsvColor.FromColor(CurrentColor);
                        Saturation = hsvColor.S;
                        Brightness = hsvColor.V;
                        if (!hsvColor.IsAchromatic())
                        {
                            Hue = hsvColor.H;
                            BaseColor = new HsvColor(Hue, 1.0, 1.0).ToRgb();
                        }
                        break;
                }
                SyncColorCore(propertyName);
            });
        }

        protected abstract void SyncColorCore(string propertyName);
    }
}
