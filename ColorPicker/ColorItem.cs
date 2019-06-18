using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace ColorPicker
{
    public class ColorItem : FrameworkElement
    {
        private readonly SolidColorBrush _brush = new SolidColorBrush();

        public Color CurrentColor
        {
            get { return (Color)GetValue(CurrentColorProperty); }
            set { SetValue(CurrentColorProperty, value); }
        }
        public static readonly DependencyProperty CurrentColorProperty =
            DependencyProperty.Register(nameof(CurrentColor), typeof(Color), typeof(ColorItem), new FrameworkPropertyMetadata(Colors.Transparent,
            FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.AffectsRender));

        public Color Value
        {
            get { return (Color)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(nameof(Value), typeof(Color), typeof(ColorItem), new FrameworkPropertyMetadata(Colors.Transparent,
            FrameworkPropertyMetadataOptions.AffectsRender,
            (d, e) => { ((ColorItem)d)._brush.Color = (Color)e.NewValue; }));

        public ColorItem()
        {
            MouseDown += (d, e) => CurrentColor = Value;
            MouseMove += (d, e) =>
            {
                if (e.LeftButton != MouseButtonState.Pressed)
                    return;

                CurrentColor = Value;
            };
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            if (CurrentColor == Value)
            {
                var renderSize = RenderSize;
                drawingContext.DrawRectangle(_brush, ColorPickerUtils.WhitePen, new Rect(0.5, 0.5, renderSize.Width - 1, renderSize.Height - 1));
                drawingContext.DrawRectangle(null, ColorPickerUtils.BlackPen, new Rect(1.5, 1.5, RenderSize.Width - 3, RenderSize.Height - 3));
            }
            else
            {
                drawingContext.DrawRectangle(_brush, null, new Rect(RenderSize));
            }
        }
    }
}