using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Oniqys.Wpf.Controls.ColorPicker
{
    /// <summary>
    /// ColorPickerSplitButton.xaml の相互作用ロジック
    /// </summary>
    public partial class ColorPickerSplitButton : ComboBox
    {
        public Color CurrentValue
        {
            get { return (Color)GetValue(CurrentValueProperty); }
            set { SetValue(CurrentValueProperty, value); }
        }
        public static readonly DependencyProperty CurrentValueProperty =
            DependencyProperty.Register(nameof(CurrentValue), typeof(Color), typeof(ColorPickerSplitButton), new PropertyMetadata(Colors.White));

        public object Content
        {
            get { return (object)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Content.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register(nameof(Content), typeof(object), typeof(ColorPickerSplitButton), new PropertyMetadata(null));

        public ColorPickerSplitButton()
        {
            InitializeComponent();
        }
    }
}