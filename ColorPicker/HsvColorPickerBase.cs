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
            = DependencyProperty.Register(nameof(Hue), typeof(double), typeof(HsvColorPickerBase), new PropertyMetadata((double)0,
            (d, e) => ((HsvColorPickerBase)d).SyncColor(false)));

        public double Saturation
        {
            get { return (double)GetValue(SaturationProperty); }
            set { SetValue(SaturationProperty, value); }
        }
        public static readonly DependencyProperty SaturationProperty
            = DependencyProperty.Register(nameof(Saturation), typeof(double), typeof(HsvColorPickerBase), new PropertyMetadata((double)1.0,
            (d, e) => ((HsvColorPickerBase)d).SyncColor(false)));

        public double Brightness
        {
            get { return (double)GetValue(BrightnessProperty); }
            set { SetValue(BrightnessProperty, value); }
        }
        public static readonly DependencyProperty BrightnessProperty
            = DependencyProperty.Register(nameof(Brightness), typeof(double), typeof(HsvColorPickerBase), new PropertyMetadata((double)1.0,
            (d, e) => ((HsvColorPickerBase)d).SyncColor(false)));

        public Color BaseColor
        {
            get { return (Color)GetValue(BaseColorProperty); }
            set { SetValue(BaseColorProperty, value); }
        }
        public static readonly DependencyProperty BaseColorProperty
            = DependencyProperty.Register(nameof(BaseColor), typeof(Color), typeof(HsvColorPickerBase), new PropertyMetadata(Colors.Red,
            (d, e) => ((HsvColorPickerBase)d).SyncColor(true)));

        public Color CurrentColor
        {
            get { return (Color)GetValue(CurrentColorProperty); }
            set { SetValue(CurrentColorProperty, value); }
        }
        public static readonly DependencyProperty CurrentColorProperty
            = DependencyProperty.Register(nameof(CurrentColor), typeof(Color), typeof(HsvColorPickerBase), new FrameworkPropertyMetadata(Colors.Red,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
            (d, e) => ((HsvColorPickerBase)d).SyncColor(true)));

        protected abstract void SyncColor(bool colorChanged);

        private void SyncBaseColor(bool colorChanged)
        {

        }
    }
}
