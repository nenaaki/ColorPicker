using System;
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
            FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public Color CurrentColor
        {
            get { return (Color)GetValue(CurrentColorProperty); }
            set { SetValue(CurrentColorProperty, value); }
        }
        public static readonly DependencyProperty CurrentColorProperty
            = DependencyProperty.Register(nameof(CurrentColor), typeof(Color), typeof(ColorPickerControl),
            new FrameworkPropertyMetadata(Colors.Transparent,
            FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
            (d, e) => ((ColorPickerControl)d).OnCurrentColorChanged((Color)e.NewValue)));

        public Color SelectedColor
        {
            get { return (Color)GetValue(SelectedColorProperty); }
            set { SetValue(SelectedColorProperty, value); }
        }
        public static readonly DependencyProperty SelectedColorProperty
            = DependencyProperty.Register(nameof(SelectedColor), typeof(Color), typeof(ColorPickerControl),
            new FrameworkPropertyMetadata(Colors.Transparent,
            FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
            (d, e) => ((ColorPickerControl)d).OnSelectedColorChanged((Color)e.NewValue)));

        public int GroupLength
        {
            get => (int)GetValue(GroupLengthProperty);
            set => SetValue(GroupLengthProperty, value);
        }
        public static readonly DependencyProperty GroupLengthProperty
            = DependencyProperty.Register(nameof(GroupLength), typeof(int), typeof(ColorPickerControl),
            new FrameworkPropertyMetadata(8,
            FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange));

        public Color[] BaseColors
        {
            get => (Color[])GetValue(BaseColorsProperty);
            set => SetValue(BaseColorsProperty, value);
        }
        public static readonly DependencyProperty BaseColorsProperty
            = DependencyProperty.Register(nameof(BaseColors), typeof(Color[]), typeof(ColorPickerControl),
            new FrameworkPropertyMetadata(default,
            FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange));

        public event EventHandler<SelectedColorChangedEventArgs> SelectedColorChanged;

        public ColorPickerControl()
        {
            InitializeComponent();
        }

        private void OnCurrentColorChanged(Color newColor)
        {
            // MEMO : 詳細を開いている場合は選択色を確定しません。
            if (_isOpenedPallet.IsChecked == true)
                return;

            SelectedColor = newColor;
        }

        private void OnSelectedColorChanged(Color newColor)
        {
            SelectedColorChanged?.Invoke(this, new SelectedColorChangedEventArgs(newColor));
        }

        private void OnOkClick(object sender, RoutedEventArgs e)
        {
            SelectedColor = CurrentColor;
        }
    }
}
