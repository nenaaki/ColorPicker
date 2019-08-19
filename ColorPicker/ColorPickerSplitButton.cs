using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Oniqys.Wpf.Controls.ColorPicker
{
    /// <summary>
    /// このカスタム コントロールを XAML ファイルで使用するには、手順 1a または 1b の後、手順 2 に従います。
    ///
    /// 手順 1a) 現在のプロジェクトに存在する XAML ファイルでこのカスタム コントロールを使用する場合
    /// この XmlNamespace 属性を使用場所であるマークアップ ファイルのルート要素に
    /// 追加します:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Oniqys.Wpf.Controls.ColorPicker"
    ///
    ///
    /// 手順 1b) 異なるプロジェクトに存在する XAML ファイルでこのカスタム コントロールを使用する場合
    /// この XmlNamespace 属性を使用場所であるマークアップ ファイルのルート要素に
    /// 追加します:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Oniqys.Wpf.Controls.ColorPicker;assembly=Oniqys.Wpf.Controls.ColorPicker"
    ///
    /// また、XAML ファイルのあるプロジェクトからこのプロジェクトへのプロジェクト参照を追加し、
    /// リビルドして、コンパイル エラーを防ぐ必要があります:
    ///
    ///     ソリューション エクスプローラーで対象のプロジェクトを右クリックし、
    ///     [参照の追加] の [プロジェクト] を選択してから、このプロジェクトを参照し、選択します。
    ///
    ///
    /// 手順 2)
    /// コントロールを XAML ファイルで使用します。
    ///
    ///     <MyNamespace:ColorPickerSplitButton/>
    ///
    /// </summary>
    public class ColorPickerSplitButton : ComboBox
    {
        public Color CurrentValue
        {
            get { return (Color)GetValue(CurrentValueProperty); }
            set { SetValue(CurrentValueProperty, value); }
        }
        public static readonly DependencyProperty CurrentValueProperty =
            DependencyProperty.Register(nameof(CurrentValue), typeof(Color), typeof(ColorPickerSplitButton), new PropertyMetadata(Colors.White));

        public string DefaultColorName
        {
            get { return (string)GetValue(DefaultColorNameProperty); }
            set { SetValue(DefaultColorNameProperty, value); }
        }
        public static readonly DependencyProperty DefaultColorNameProperty =
            DependencyProperty.Register(nameof(DefaultColorName), typeof(string), typeof(ColorPickerSplitButton), new PropertyMetadata(string.Empty));

        public Color DefaultColor
        {
            get => (Color)GetValue(DefaultColorProperty);
            set => SetValue(DefaultColorProperty, value);
        }
        public static readonly DependencyProperty DefaultColorProperty
            = DependencyProperty.Register(nameof(DefaultColor), typeof(Color), typeof(ColorPickerSplitButton),
            new FrameworkPropertyMetadata(Colors.Transparent,
            FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public object Content
        {
            get { return (object)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Content.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(object), typeof(ColorPickerSplitButton), new PropertyMetadata(null));
    }
}