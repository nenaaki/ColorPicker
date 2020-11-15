using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using System.Globalization;

namespace Oniqys.Wpf.Controls.ColorPicker.Converters
{
    public class WhiteOrBlackConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value is Color color ? HsvColor.FromColor(color).GetBrightness() > 0.5 ? Colors.Black : Colors.White : Colors.Black;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotSupportedException();
    }
}
