using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        public Brush[] BrushArray
        {
            get => (Brush[])GetValue(BrushArrayProperty);
            private set => SetValue(BrushArrayProperty, value);
        }
        public static readonly DependencyProperty BrushArrayProperty
            = DependencyProperty.Register(nameof(BrushArray), typeof(Brush[]), typeof(GradualColorPicker), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange));

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
                // TODO : ここが間違えている。
                bool toWhite = color1.V < 0.5;
                color2 = new HsvColor(color1.H, toWhite ? color1.S : 0.0, toWhite ? 0.0 : color1.V);
            }

            var count = Count;

            var brushes = new Brush[count + 1];
            for(int idx=0; idx<=count ; idx++)
            {
                double ratio = idx / (double)count;
                var hsvColor = HsvColor.Blend(color1, color2, ratio);
                brushes[idx] = new SolidColorBrush(hsvColor.ToRgb());
            }
            BrushArray = brushes;
        }

        public GradualColorPicker()
        {
            InitializeComponent();
            EnsureColors();
        }
    }
}