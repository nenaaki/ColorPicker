using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ColorPicker
{
    /// <summary>
    /// GradualColorPickerSlot.xaml の相互作用ロジック
    /// </summary>
    public partial class GradualColorPickerSlot : UserControl
    {
        public Color CurrentColor
        {
            get { return (Color)GetValue(CurrentColorProperty); }
            set { SetValue(CurrentColorProperty, value); }
        }
        public static readonly DependencyProperty CurrentColorProperty
            = DependencyProperty.Register(nameof(CurrentColor), typeof(Color), typeof(GradualColorPickerSlot), new PropertyMetadata(default(Color),
            (d, e) => { }));

        public Color[] BaseColors
        {
            get => (Color[])GetValue(BaseColorsProperty);
            set => SetValue(BaseColorsProperty, value);
        }
        public static readonly DependencyProperty BaseColorsProperty =
            DependencyProperty.Register(nameof(BaseColors), typeof(Color[]), typeof(GradualColorPickerSlot), new PropertyMetadata(null));

        public GradualColorPickerSlot()
        {
            InitializeComponent();
        }
    }
}