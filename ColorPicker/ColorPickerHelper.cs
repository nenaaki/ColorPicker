using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Oniqys.Wpf.Controls.ColorPicker
{
    public static class ColorPickerHelper
    {
        public static ICommand GetColorChangeCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(ColorChangeCommandProperty);
        }

        public static void SetColorChangeCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(ColorChangeCommandProperty, value);
        }
        public static readonly DependencyProperty ColorChangeCommandProperty =
            DependencyProperty.RegisterAttached("ColorChangeCommand", typeof(ICommand), typeof(ColorPickerHelper), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));
    }
}
