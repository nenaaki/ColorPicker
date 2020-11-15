using System.Windows;

namespace Oniqys.Wpf.Core
{
    public readonly struct ReadonlySize
    {
        public readonly double Width;
        public readonly double Height;

        public ReadonlySize(double width, double height)
        {
            Width = width;
            Height = height;
        }

        public static explicit operator Size(in ReadonlySize size) => new Size(size.Width, size.Height);

        public static explicit operator ReadonlySize(Size size) => new ReadonlySize(size.Width, size.Height);
    }
}
