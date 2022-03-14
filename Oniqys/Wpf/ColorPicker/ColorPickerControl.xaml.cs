using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Oniqys.Wpf.Controls.ColorPicker
{
    /// <summary>
    /// ColorPickerControl.xaml の相互作用ロジック
    /// </summary>
    public partial class ColorPickerControl : UserControl
    {
        public string DefaultColorName
        {
            get { return (string)GetValue(DefaultColorNameProperty); }
            set { SetValue(DefaultColorNameProperty, value); }
        }
        public static readonly DependencyProperty DefaultColorNameProperty =
            DependencyProperty.Register(nameof(DefaultColorName), typeof(string), typeof(ColorPickerControl), new PropertyMetadata(string.Empty));

        public Color DefaultColor
        {
            get => (Color)GetValue(DefaultColorProperty);
            set => SetValue(DefaultColorProperty, value);
        }
        public static readonly DependencyProperty DefaultColorProperty
            = DependencyProperty.Register(nameof(DefaultColor), typeof(Color), typeof(ColorPickerControl),
            new FrameworkPropertyMetadata(Colors.Transparent,
            FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public Color BaseColor
        {
            get => (Color)GetValue(BaseColorProperty);
            set => SetValue(BaseColorProperty, value);
        }
        public static readonly DependencyProperty BaseColorProperty
            = DependencyProperty.Register(nameof(BaseColor), typeof(Color), typeof(ColorPickerControl),
            new FrameworkPropertyMetadata(Colors.Transparent,
            FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public Color CurrentColor
        {
            get => (Color)GetValue(CurrentColorProperty);
            set => SetValue(CurrentColorProperty, value);
        }
        public static readonly DependencyProperty CurrentColorProperty
            = DependencyProperty.Register(nameof(CurrentColor), typeof(Color), typeof(ColorPickerControl),
            new FrameworkPropertyMetadata(Colors.Transparent,
            FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public Color SelectedColor
        {
            get => (Color)GetValue(SelectedColorProperty);
            set => SetValue(SelectedColorProperty, value);
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

        public RecentColorManager RecentColors
        {
            get => (RecentColorManager)GetValue(RecentColorsProperty);
            set => SetValue(RecentColorsProperty, value);
        }
        public static readonly DependencyProperty RecentColorsProperty
            = DependencyProperty.Register(nameof(RecentColors), typeof(RecentColorManager), typeof(ColorPickerControl),
            new FrameworkPropertyMetadata(RecentColorManager.DefaultInstance,
            FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange));

        public Command SelectColorCommand
        {
            get { return (Command)GetValue(SelectColorCommandProperty); }
            set { SetValue(SelectColorCommandProperty, value); }
        }
        public static readonly DependencyProperty SelectColorCommandProperty =
            DependencyProperty.Register(nameof(SelectColorCommand), typeof(Command), typeof(ColorPickerControl), new PropertyMetadata(null));

        public bool IsSelecting
        {
            get { return (bool)GetValue(IsSelectingProperty); }
            set { SetValue(IsSelectingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsSelecting.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsSelectingProperty =
            DependencyProperty.Register(nameof(IsSelecting), typeof(bool), typeof(ColorPickerControl), new PropertyMetadata(false));

        public event EventHandler<SelectedColorChangedEventArgs> SelectedColorChanged;

        public ColorPickerControl()
        {
            InitializeComponent();

            SelectColorCommand = new Command(() =>
            {
                if (_isOpenedPallet.IsChecked == false)
                {
                    SelectedColor = CurrentColor;
                    RecentColors.Register(SelectedColor);
                    IsSelecting = false;
                }
            });
        }

        private void OnSelectedColorChanged(Color newColor)
        {
            SelectedColorChanged?.Invoke(this, new SelectedColorChangedEventArgs(newColor));
        }

        private void OnOkClicked(object sender, RoutedEventArgs e)
        {
            SelectedColor = CurrentColor;
            RecentColors.Register(SelectedColor);
            IsSelecting = false;
        }

        /// <summary>
        /// 初回だけ<see cref="BaseColor"/>を現在の色に同期します。
        /// ただし、灰色や透明の場合は何もしません。
        /// </summary>
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            var newColor = CurrentColor;
            var hsv = HsvColor.FromColor(newColor);
            if (newColor != Colors.Transparent && !hsv.IsAchromatic())
            {
                BaseColor = new HsvColor(hsv.H, 1.0, 1.0).ToColor();
            }
        }
    }
}