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

namespace Oniqys.Wpf.Controls.ColorPicker
{
    /// <summary>
    /// ColorPickerComboBox.xaml の相互作用ロジック
    /// </summary>
    public partial class ColorPickerComboBox : ComboBox
    {
        public Color CurrentValue
        {
            get { return (Color)GetValue(CurrentValueProperty); }
            set { SetValue(CurrentValueProperty, value); }
        }
        public static readonly DependencyProperty CurrentValueProperty =
            DependencyProperty.Register(nameof(CurrentValue), typeof(Color), typeof(ColorPickerComboBox), new PropertyMetadata(Colors.White));


        public ColorPickerComboBox()
        {
            InitializeComponent();
        }
    }
}
