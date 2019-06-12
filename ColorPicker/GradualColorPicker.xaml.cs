using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ColorPicker
{
    /// <summary>
    /// GradualColorPicker.xaml の相互作用ロジック
    /// </summary>
    public partial class GradualColorPicker : UserControl
    {
        public Color BaseColor
        {
            get => (Color)GetValue(BaseColorProperty);
            set => SetValue(BaseColorProperty, value);
        }
        public static readonly DependencyProperty BaseColorProperty
            = DependencyProperty.Register(nameof(BaseColor), typeof(Color), typeof(GradualColorPicker), new PropertyMetadata(Colors.Black,
            (d, e) => ((GradualColorPicker)d).EnsureColors()));

        public int Count
        {
            get => (int)GetValue(CountProperty);
            set => SetValue(CountProperty, value);
        }
        public static readonly DependencyProperty CountProperty
            = DependencyProperty.Register(nameof(Count), typeof(int), typeof(GradualColorPicker), new PropertyMetadata(8,
            (d, e) => ((GradualColorPicker)d).EnsureColors()));

        public Selectable<Brush>[] BrushArray
        {
            get => (Selectable<Brush>[])GetValue(BrushArrayProperty);
            private set => SetValue(BrushArrayProperty, value);
        }
        public static readonly DependencyProperty BrushArrayProperty
            = DependencyProperty.Register(nameof(BrushArray), typeof(Selectable<Brush>[]), typeof(GradualColorPicker), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange));

        public Color CurrentColor
        {
            get { return (Color)GetValue(CurrentColorProperty); }
            set { SetValue(CurrentColorProperty, value); }
        }
        public static readonly DependencyProperty CurrentColorProperty
            = DependencyProperty.Register(nameof(CurrentColor), typeof(Color), typeof(GradualColorPicker), new PropertyMetadata(Colors.Transparent));

        private void EnsureColors()
        {
            var baseColor = BaseColor;

            HsvColor color1;
            HsvColor color2;

            if (baseColor == Colors.Black)
            {
                color1 = HsvColor.FromRgb(0, 0, 0);
                color2 = HsvColor.FromRgb(255, 255, 255);
            }
            else if (baseColor == Colors.White)
            {
                color1 = HsvColor.FromRgb(255, 255, 255);
                color2 = HsvColor.FromRgb(0, 0, 0);
            }
            else
            {
                color1 = HsvColor.FromRgb(baseColor.R, baseColor.G, baseColor.B);

                // MEMO : グレースケール化したときの明度が50%未満かで白に向けるか黒に向けるかを切り替える。
                if (color1.GetBrightness() < 0.5)
                {
                    color2 = new HsvColor(color1.H, 0.0, 0.9);
                }
                else
                {
                    color2 = new HsvColor(color1.H, color1.S, 0.20);
                }
            }

            var count = Count;

            var brushes = new Selectable<Brush>[count + 1];
            for (int idx = 0; idx <= count; idx++)
            {
                double ratio = idx / (double)count;
                var hsvColor = HsvColor.Blend(color1, color2, ratio);
                brushes[idx] = new Selectable<Brush> { Value = new SolidColorBrush(hsvColor.ToRgb()) };
            }
            BrushArray = brushes;
        }

        public GradualColorPicker()
        {
            InitializeComponent();
            EnsureColors();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 1)
            {
                CurrentColor = ((SolidColorBrush)((Selectable<Brush>)e.AddedItems[0]).Value).Color;
            }
        }
    }
}