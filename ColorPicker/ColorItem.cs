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
                drawingContext.DrawRectangle(Brush, ColorPickerUtils.WhitePen, new Rect(0.5, 0.5, renderSize.Width - 1, renderSize.Height - 1));
                drawingContext.DrawRectangle(null, ColorPickerUtils.BlackPen, new Rect(1.5, 1.5, RenderSize.Width - 3, RenderSize.Height - 3));
            }
            else
            {
                drawingContext.DrawRectangle(Brush, null, new Rect(RenderSize));
            }
        }
    }
}