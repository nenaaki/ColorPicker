using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ColorPicker
{
    /// <summary>
    /// ColorPickerControl.xaml の相互作用ロジック
    /// </summary>
    public partial class ColorPickerControl : UserControl
    {
        public Color BaseColor
        {
            get { return (Color)GetValue(BaseColorProperty); }
            set { SetValue(BaseColorProperty, value); }
        }
        public static readonly DependencyProperty BaseColorProperty
            = DependencyProperty.Register(nameof(BaseColor), typeof(Color), typeof(ColorPickerControl),
            new FrameworkPropertyMetadata(Colors.Transparent,
            FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
            (d, e) =>{ }));


        public Color CurrentColor
        {
            get { return (Color)GetValue(CurrentColorProperty); }
            set { SetValue(CurrentColorProperty, value); }
        }
        public static readonly DependencyProperty CurrentColorProperty
            = DependencyProperty.Register(nameof(CurrentColor), typeof(Color), typeof(ColorPickerControl),
            new FrameworkPropertyMetadata(Colors.Transparent,
            FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public ColorPickerControl()
        {
            InitializeComponent();
        }
    }
}
