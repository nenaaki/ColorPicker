using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ColorPicker
{
    /// <summary>
    /// グラデーション型のカラーピッカーのスロットです。
    /// </summary>
    public partial class GradualColorPickerSlot : ItemsControl
    {
        public int StepCount
        {
            get { return (int)GetValue(StepCountProperty); }
            set { SetValue(StepCountProperty, value); }
        }
        public static readonly DependencyProperty StepCountProperty
            = DependencyProperty.Register(nameof(StepCount), typeof(int), typeof(GradualColorPickerSlot),
                new FrameworkPropertyMetadata(8));

        public bool Expanded
        {
            get { return (bool)GetValue(ExpandedProperty); }
            set { SetValue(ExpandedProperty, value); }
        }
        public static readonly DependencyProperty ExpandedProperty
            = DependencyProperty.Register(nameof(Expanded), typeof(bool), typeof(GradualColorPickerSlot),
                new FrameworkPropertyMetadata(false));

        public Color CurrentColor
        {
            get { return (Color)GetValue(CurrentColorProperty); }
            set { SetValue(CurrentColorProperty, value); }
        }
        public static readonly DependencyProperty CurrentColorProperty
            = DependencyProperty.Register(nameof(CurrentColor), typeof(Color), typeof(GradualColorPickerSlot),
                new FrameworkPropertyMetadata(Colors.Transparent,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public IList<Color> BaseColors
        {
            get => (IList<Color>)GetValue(BaseColorsProperty);
            set => SetValue(BaseColorsProperty, value);
        }
        public static readonly DependencyProperty BaseColorsProperty =
            DependencyProperty.Register(nameof(BaseColors), typeof(IList<Color>), typeof(GradualColorPickerSlot),
                new FrameworkPropertyMetadata(null,
                (d, e) => { ((GradualColorPickerSlot)d).ItemsSource = e.NewValue as IList<Color>; }));

        public GradualColorPickerSlot()
        {
            InitializeComponent();
        }
    }
}