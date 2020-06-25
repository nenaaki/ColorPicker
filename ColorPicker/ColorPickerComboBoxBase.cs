using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Oniqys.Wpf.Controls.ColorPicker
{
    public abstract class ColorPickerComboBoxBase : ComboBox
    {
        public Color CurrentValue
        {
            get { return (Color)GetValue(CurrentValueProperty); }
            set { SetValue(CurrentValueProperty, value); }
        }
        public static readonly DependencyProperty CurrentValueProperty =
            DependencyProperty.Register(nameof(CurrentValue), typeof(Color), typeof(ColorPickerComboBoxBase), new PropertyMetadata(Colors.White));

        public string DefaultColorName
        {
            get { return (string)GetValue(DefaultColorNameProperty); }
            set { SetValue(DefaultColorNameProperty, value); }
        }
        public static readonly DependencyProperty DefaultColorNameProperty =
            DependencyProperty.Register(nameof(DefaultColorName), typeof(string), typeof(ColorPickerComboBoxBase), new PropertyMetadata(string.Empty));

        public Color DefaultColor
        {
            get => (Color)GetValue(DefaultColorProperty);
            set => SetValue(DefaultColorProperty, value);
        }
        public static readonly DependencyProperty DefaultColorProperty
            = DependencyProperty.Register(nameof(DefaultColor), typeof(Color), typeof(ColorPickerComboBoxBase),
            new FrameworkPropertyMetadata(Colors.Transparent,
            FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
    }
}