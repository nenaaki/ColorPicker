using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using Oniqys.Wpf.Controls.ColorPicker.Enums;

namespace Oniqys.Wpf.Controls.ColorPicker
{
    /// <summary>
    /// 四角い色アイテムです。
    /// </summary>
    [ContentProperty("Content")]
    public sealed class ColorItem : ColorItemBase
    {
        public UIElement Content
        {
            get { return (UIElement)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register(nameof(Content), typeof(UIElement), typeof(ColorItem),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            var renderSize = RenderSize;
            if (CurrentColor == SourceColor || SelectionMode == SelectionMode.ClickMode && IsMouseOver && Mouse.LeftButton == MouseButtonState.Pressed)
            {
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