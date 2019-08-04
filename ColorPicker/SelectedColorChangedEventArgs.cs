using System;
using System.Windows.Media;

namespace Oniqys.Wpf.ColorPicker
{
    public class SelectedColorChangedEventArgs : EventArgs
    {
        public Color Color { get; }

        public SelectedColorChangedEventArgs(Color color)
        {
            Color = color;
        }
    }
}
