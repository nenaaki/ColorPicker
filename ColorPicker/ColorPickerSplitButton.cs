using System.Windows;
using System.Windows.Input;

namespace Oniqys.Wpf.Controls.ColorPicker
{
    /// <summary>
    /// カラーピッカーを持つSplitButtonです。
    /// </summary>
    public class ColorPickerSplitButton : ColorPickerComboBoxBase, ICommandSource
    {
        /// <summary>
        /// ボタン内に配置するコンテントを取得または設定します。
        /// </summary>
        public object Content
        {
            get { return GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register(nameof(Content), typeof(object), typeof(ColorPickerSplitButton), new PropertyMetadata(null));

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(ColorPickerSplitButton), new PropertyMetadata(null));

        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }
        public static readonly DependencyProperty CommandParameterProperty
            = DependencyProperty.Register(nameof(CommandParameter), typeof(object), typeof(ColorPickerSplitButton), new PropertyMetadata(null));

        public IInputElement CommandTarget
        {
            get { return (IInputElement)GetValue(CommandTargetProperty); }
            set { SetValue(CommandTargetProperty, value); }
        }
        public static readonly DependencyProperty CommandTargetProperty
            = DependencyProperty.Register(nameof(CommandTarget), typeof(IInputElement), typeof(ColorPickerSplitButton), new PropertyMetadata(null));

        static ColorPickerSplitButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorPickerSplitButton), new FrameworkPropertyMetadata(typeof(ColorPickerSplitButton)));
        }
    }
}