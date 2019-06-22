using System;
using System.Windows;
using System.Windows.Media;

namespace ColorPicker
{
    public sealed class HexColorItem : ColorItemBase
    {
        private static readonly StreamGeometry _hexGeometry = new StreamGeometry();
        private static readonly StreamGeometry _edgeGeometry = new StreamGeometry();
        private static readonly StreamGeometry _cursorGeometry = new StreamGeometry();


        static HexColorItem()
        {
            UpdateHexGeometory(_hexGeometry, 10);
            UpdateHexGeometory(_edgeGeometry, 9.5);
            UpdateHexGeometory(_cursorGeometry, 8.5);
        }

        private static void UpdateHexGeometory(StreamGeometry geometry, double radius)
        {
            const double pi3 = Math.PI / 3;

            using (var c = geometry.Open())
            {
                for (var i = 0; i < 6; i++)
                {
                    var p = new Point(radius * Math.Sin(i * pi3), radius * Math.Cos(i * pi3));
                    if (i == 0)
                    {
                        c.BeginFigure(p, true, true);
                    }
                    else
                    {
                        c.LineTo(p, true, false);
                    }
                }
            }
            geometry.Freeze();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            drawingContext.DrawGeometry(Brush, null, _hexGeometry);

            if (CurrentColor == Value)
            {
                drawingContext.DrawGeometry(null, ColorPickerUtils.WhitePen, _edgeGeometry);
                drawingContext.DrawGeometry(null, ColorPickerUtils.BlackPen, _cursorGeometry);
            }
        }
    }
}
