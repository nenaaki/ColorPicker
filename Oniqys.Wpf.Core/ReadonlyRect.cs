using System.Windows;

namespace Oniqys.Wpf.Core
{
    public readonly struct ReadonlyRect
    {
        public readonly double X;
        public readonly double Y;
        public readonly double Width;
        public readonly double Height;

        public ReadonlyRect(double x, double y, double width, double height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public ReadonlyRect(in ReadonlyPoint point, in ReadonlySize size)
        {
            X = point.X;
            Y = point.Y;
            Width = size.Width;
            Height = size.Height;
        }

        public static explicit operator Rect(in ReadonlyRect rect) => new Rect(rect.X, rect.Y, rect.Width, rect.Height);

        public static explicit operator ReadonlyRect(in Rect rect) => new ReadonlyRect(rect.X, rect.Y, rect.Width, rect.Height);

        public ReadonlyPoint Location => new ReadonlyPoint(X, Y);

        public ReadonlySize Size => new ReadonlySize(Width, Height);

        public ReadonlyPoint BottomRight => new ReadonlyPoint(X + Width, Y + Height);
    }
}
