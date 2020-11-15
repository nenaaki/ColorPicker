using System;
using System.Windows.Media;

namespace Oniqys.Wpf.Controls.ColorPicker
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
