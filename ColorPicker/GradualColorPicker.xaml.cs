using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Input;
using System.ComponentModel;
using System.Linq;

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
            = DependencyProperty.Register(nameof(BaseColor), typeof(Color), typeof(GradualColorPicker),
                new FrameworkPropertyMetadata(Colors.Black, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange,
                (d, e) => ((GradualColorPicker)d).SetupColors()));

        public int Count
        {
            get => (int)GetValue(CountProperty);
            set => SetValue(CountProperty, value);
        }
        public static readonly DependencyProperty CountProperty
            = DependencyProperty.Register(nameof(Count), typeof(int), typeof(GradualColorPicker),
                new FrameworkPropertyMetadata(8, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange,
                (d, e) => ((GradualColorPicker)d).SetupColors()));

        public Color[] ColorArray
        {
            get => (Color[])GetValue(ColorArrayProperty);
            private set => SetValue(ColorArrayProperty, value);
        }
        public static readonly DependencyProperty ColorArrayProperty
            = DependencyProperty.Register(nameof(ColorArray), typeof(Color[]), typeof(GradualColorPicker),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange));

        public Color CurrentColor
        {
            get { return (Color)GetValue(CurrentColorProperty); }
            set { SetValue(CurrentColorProperty, value); }
        }
        public static readonly DependencyProperty CurrentColorProperty
            = DependencyProperty.Register(nameof(CurrentColor), typeof(Color), typeof(GradualColorPicker),
                new FrameworkPropertyMetadata(Colors.Transparent,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.AffectsParentArrange,
                (d, e) => ((GradualColorPicker)d).SyncColor(true)));

        private void SetupColors()
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
                var brightness = color1.GetBrightness();
                color2 = brightness < 0.5 ? new HsvColor(color1.H, 0, 1) : new HsvColor(color1.H, 0, 0);
            }

            var count = Count;

            var brushes = new Color[count + 1];
            for (int idx = 0; idx <= count; idx++)
            {
                float ratio = idx / (float)(count + 1);
                brushes[idx] = HsvColor.Blend(color1, color2, ratio).ToRgb();
            }
            ColorArray = brushes;
        }

        public GradualColorPicker()
        {
            InitializeComponent();
            SetupColors();
        }


        private bool _updating;
        private void SyncColor(bool colorChanged)
        {
            if (_updating)
                return;

            try
            {
                _updating = true;
                if(colorChanged)
                {
                    var currentColor = CurrentColor;
                    var colorArray = ColorArray;
                    if (ColorArray == null)
                        return;

                    for(var idx=0;idx<colorArray.Length; idx++)
                    {
                        if (colorArray[idx] == currentColor)
                        {
                        }
                    }
                }
                else
                {

                }
            }
            finally
            {
                _updating = false;
            }
        }

        private void ColorItem_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CurrentColor = ((ColorItem)sender).Value;
        }

        private void ColorItem_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed)
                return;

            CurrentColor = ((ColorItem)sender).Value;
        }
    }
}