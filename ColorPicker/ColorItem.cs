using System.Windows;
using System.Windows.Media;

namespace ColorPicker
{
    public sealed class ColorItem : ColorItemBase
    {
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            if (CurrentColor == Value)
            {
                var renderSize = RenderSize;
                drawingContext.DrawRectangle(Brush, WhitePen, new Rect(0.5, 0.5, renderSize.Width - 1, renderSize.Height - 1));
                drawingContext.DrawRectangle(null, BlackPen, new Rect(1.5, 1.5, renderSize.Width - 3, renderSize.Height - 3));
            }
            else
            {
                drawingContext.DrawRectangle(Brush, null, new Rect(RenderSize));
            }
        }
    }
}