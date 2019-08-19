using System.Windows;
using System.Windows.Input;

namespace Oniqys.Wpf.Controls.ColorPicker
{
    public class ColorPickerSplitButton : ColorPickerComboBoxBase
    {
        public object Content
        {
            get { return (object)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register(nameof(Content), typeof(object), typeof(ColorPickerSplitButton), new PropertyMetadata(null));


        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(ColorPickerSplitButton), new PropertyMetadata(null));

        static ColorPickerSplitButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorPickerSplitButton), new FrameworkPropertyMetadata(typeof(ColorPickerSplitButton)));
        }
    }
}