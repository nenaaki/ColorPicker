using System.Windows;

namespace Oniqys.Wpf.Core
{
    public readonly struct ReadonlyPoint
    {
        public readonly double X;
        public readonly double Y;

        public ReadonlyPoint(double x, double y)
        {
            X = x;
            Y = y;
        }

        public static explicit operator Point(in ReadonlyPoint point) => new Point(point.X, point.Y);

        public static explicit operator ReadonlyPoint(Point point) => new ReadonlyPoint(point.X, point.Y);
    }
}
