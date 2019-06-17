using System.Windows;
using System.Windows.Media;

namespace ColorPicker
{
    public class ColorItem : FrameworkElement
    {
        private static readonly Pen _blackPen = new Pen(Brushes.Black, 1);

        private static readonly Pen _whitePen = new Pen(Brushes.White, 1);

        private readonly SolidColorBrush _brush = new SolidColorBrush();

        public Color CurrentColor
        {
            get { return (Color)GetValue(CurrentColorProperty); }
            set { SetValue(CurrentColorProperty, value); }
        }
        public static readonly DependencyProperty CurrentColorProperty =
            DependencyProperty.Register(nameof(CurrentColor), typeof(Color), typeof(ColorItem), new FrameworkPropertyMetadata(Colors.Transparent,
            FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public Color Value
        {
            get { return (Color)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(nameof(Value), typeof(Color), typeof(ColorItem), new FrameworkPropertyMetadata(Colors.Transparent,
            FrameworkPropertyMetadataOptions.AffectsRender,
            (d, e) => { ((ColorItem)d)._brush.Color = (Color)e.NewValue; }));

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            if (CurrentColor == Value)
            {
                drawingContext.DrawRectangle(_brush, _whitePen, new Rect(RenderSize));
                drawingContext.DrawRectangle(null, _blackPen, new Rect(1, 1, RenderSize.Width - 2, RenderSize.Height - 2));
            }
            else
            {
                drawingContext.DrawRectangle(_brush, null, new Rect(RenderSize));
            }
        }
    }
}
