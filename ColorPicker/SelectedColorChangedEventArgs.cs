using System;
using System.Windows.Media;

namespace ColorPicker
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
