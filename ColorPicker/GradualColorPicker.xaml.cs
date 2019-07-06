using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ColorPicker
{
    /// <summary>
    /// GradualColorPicker.xaml の相互作用ロジック
    /// </summary>
    public partial class GradualColorPicker : ItemsControl
    {
        public Color BaseColor
        {
            get => (Color)GetValue(BaseColorProperty);
            set => SetValue(BaseColorProperty, value);
        }
        public static readonly DependencyProperty BaseColorProperty
            = DependencyProperty.Register(nameof(BaseColor), typeof(Color), typeof(GradualColorPicker),
                new FrameworkPropertyMetadata(Colors.Black, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange,
                (d, _) => ((GradualColorPicker)d).SetupColors()));

        public int StepCount
        {
            get => (int)GetValue(StepCountProperty);
            set => SetValue(StepCountProperty, value);
        }
        public static readonly DependencyProperty StepCountProperty
            = DependencyProperty.Register(nameof(StepCount), typeof(int), typeof(GradualColorPicker),
                new FrameworkPropertyMetadata(8, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange,
                (d, _) => ((GradualColorPicker)d).SetupColors()));

        public Color CurrentColor
        {
            get => (Color)GetValue(CurrentColorProperty);
            set => SetValue(CurrentColorProperty, value);
        }
        public static readonly DependencyProperty CurrentColorProperty
            = DependencyProperty.Register(nameof(CurrentColor), typeof(Color), typeof(GradualColorPicker),
                new FrameworkPropertyMetadata(Colors.Transparent,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        private void SetupColors()
        {
            var color1 = BaseColor;
            Color color2;

            if (color1 == Colors.Black)
            {
                color2 = Colors.White;
            }
            else if (color1 == Colors.White)
            {
                color2 = Colors.Black;
            }
            else
            {
                // MEMO : グレースケール化したときの明度が50%未満かで白に向けるか黒に向けるかを切り替える。
                var brightness = HsvColor.FromColor(color1).GetBrightness();
                color2 = (brightness < 0.5) ? Colors.White : Colors.Black;
            }

            var count = StepCount;

            var colors = new Color[count + 1];
            for (int idx = 0; idx <= count; idx++)
            {
                float ratio = idx / (float)(count + 1);
                colors[idx] = Color.FromRgb(
                    (byte)(color2.R * ratio + color1.R * (1 - ratio)),
                    (byte)(color2.G * ratio + color1.G * (1 - ratio)),
                    (byte)(color2.B * ratio + color1.B * (1 - ratio)));
            }
            ItemsSource = colors;
        }

        public GradualColorPicker()
        {
            InitializeComponent();
            SetupColors();
        }
    }
}