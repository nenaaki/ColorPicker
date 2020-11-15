using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Oniqys.Wpf.Controls.ColorPicker
{
    /// <summary>
    /// GradualColorPickerMultiSlot.xaml の相互作用ロジック
    /// </summary>
    public partial class GradualColorPickerMultiSlot : ListBox
    {
        public int GroupLength
        {
            get => (int)GetValue(GroupLengthProperty);
            set => SetValue(GroupLengthProperty, value);
        }
        public static readonly DependencyProperty GroupLengthProperty
            = DependencyProperty.Register(nameof(GroupLength), typeof(int), typeof(GradualColorPickerMultiSlot),
            new FrameworkPropertyMetadata(8,
            FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange,
            (d, _) => ((GradualColorPickerMultiSlot)d).SetupContent()));

        public Color[] BaseColors
        {
            get => (Color[])GetValue(BaseColorsProperty);
            set => SetValue(BaseColorsProperty, value);
        }
        public static readonly DependencyProperty BaseColorsProperty
            = DependencyProperty.Register(nameof(BaseColors), typeof(Color[]), typeof(GradualColorPickerMultiSlot),
            new FrameworkPropertyMetadata(default,
            FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange,
            (d, _) => ((GradualColorPickerMultiSlot)d).SetupContent()));

        public IList<Color>[] ColorSlots
        {
            get => (IList<Color>[])GetValue(ColorSlotsProperty);
            private set => SetValue(ColorSlotsPropertyKey, value);
        }
        public static readonly DependencyPropertyKey ColorSlotsPropertyKey
            = DependencyProperty.RegisterReadOnly(nameof(ColorSlots), typeof(IList<Color>[]), typeof(GradualColorPickerMultiSlot),
            new FrameworkPropertyMetadata(default,
            FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange));
        private static readonly DependencyProperty ColorSlotsProperty = ColorSlotsPropertyKey.DependencyProperty;

        public Color CurrentColor
        {
            get { return (Color)GetValue(CurrentColorProperty); }
            set { SetValue(CurrentColorProperty, value); }
        }
        public static readonly DependencyProperty CurrentColorProperty
            = DependencyProperty.Register(nameof(CurrentColor), typeof(Color), typeof(GradualColorPickerMultiSlot),
                new FrameworkPropertyMetadata(Colors.Transparent,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        /// <summary>
        /// 色が確定する操作をしたときにコマンドを発します。
        /// </summary>
        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(GradualColorPickerMultiSlot), new PropertyMetadata(null));

        public GradualColorPickerMultiSlot()
        {
            InitializeComponent();
        }

        private void SetupContent()
        {
            var baseColors = BaseColors;
            if (baseColors == null)
                return;

            var groupLength = GroupLength;
            if (groupLength == 0)
                return;

            var colorSlotList = new List<List<Color>>();
            for (int idx = 0; idx < BaseColors.Length; idx++)
            {
                if (idx % groupLength == 0)
                {
                    colorSlotList.Add(new List<Color>(groupLength));
                }
                var slot = colorSlotList[idx / groupLength];

                slot.Add(BaseColors[idx]);
            }

            var colorSlots = new List<Color>[colorSlotList.Count];
            for (int idx = 0; idx < colorSlots.Length; idx++)
            {
                colorSlots[idx] = colorSlotList[idx];
            }
            ColorSlots = colorSlots;
            ItemsSource = colorSlots;
        }
    }
}
