using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ColorPicker
{
    /// <summary>
    /// SimpleColorPicker.xaml の相互作用ロジック
    /// </summary>
    public partial class SimpleColorPicker : ItemsControl
    {
        public Color CurrentColor
        {
            get { return (Color)GetValue(CurrentColorProperty); }
            set { SetValue(CurrentColorProperty, value); }
        }
        public static readonly DependencyProperty CurrentColorProperty
            = DependencyProperty.Register(nameof(CurrentColor), typeof(Color), typeof(SimpleColorPicker),
                new FrameworkPropertyMetadata(Colors.Transparent,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


        public SimpleColorPicker()
        {
            InitializeComponent();
        }
    }
}
