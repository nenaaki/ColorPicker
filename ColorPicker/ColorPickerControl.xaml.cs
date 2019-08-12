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
        #region Inner Classes

        public class ColorPickerControlContent : ContentBase
        {
            private readonly ColorPickerControl _owner;

            private bool _isSelecting;

            public bool IsSelecting
            {
                get => _isSelecting;
                set => UpdateProperty(ref _isSelecting, value);
            }

            private bool _isDetailOpened;

            public bool IsDetailOpened
            {
                get => _isDetailOpened;
                set => UpdateProperty(ref _isDetailOpened, value);
            }

            private Color _currentColor;

            public Color CurrentColor
            {
                get => _currentColor;
                set => UpdateProperty(ref _currentColor, value);
            }

            private Color _baseColor;

            public Color BaseColor
            {
                get => _baseColor;
                set => UpdateProperty(ref _baseColor, value);
            }

            private RecentColorManager _recentColorManager = RecentColorManager.DefaultInstane;

            public RecentColorManager RecentColors
            {
                get => _recentColorManager;
                set => UpdateReferenceProperty(ref _recentColorManager, value);
            }


            public ColorPickerControlContent(ColorPickerControl owner)
            {
                _owner = owner;

                IsSelecting = true;

            }
        }

        #endregion

        /// <summary>
        /// DataContextをユーザーに明け渡すために内部で使用するBinding情報を保持します。
        /// </summary>
        public ColorPickerControlContent Source { get; }

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
            new FrameworkPropertyMetadata(RecentColorManager.DefaultInstane,
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
            Source = new ColorPickerControlContent(this);

            InitializeComponent();

            SelectColorCommand = new Command(() =>
            {
                if(_isOpenedPallet.IsChecked == false)
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
    }
}