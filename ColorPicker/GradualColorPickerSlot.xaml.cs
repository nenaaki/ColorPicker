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
    /// GradualColorPickerSlot.xaml の相互作用ロジック
    /// </summary>
    public partial class GradualColorPickerSlot : UserControl
    {
        public Color[] BaseColors
        {
            get => (Color[])GetValue(BaseColorsProperty);
            set => SetValue(BaseColorsProperty, value);
        }
        public static readonly DependencyProperty BaseColorsProperty =
            DependencyProperty.Register(nameof(BaseColors), typeof(Color[]), typeof(GradualColorPickerSlot), new PropertyMetadata(null));

        public GradualColorPickerSlot()
        {
            InitializeComponent();
        }
    }
}
