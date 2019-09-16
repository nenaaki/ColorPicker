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
        /// <summary>
        /// 内部に配置する<see cref="UIElement"/>を設定します。
        /// </summary>
        public UIElement Content
        {
            get { return (UIElement)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        /// <summary>
        /// <see cref="Content"/>の依存関係プロパティです。
        /// </summary>
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register(nameof(Content), typeof(UIElement), typeof(ColorItem),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// 単色のピッカーを描画します。
        /// </summary>
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            var renderSize = RenderSize;
            var isKeyboardFocused = IsKeyboardFocused;
            if (isKeyboardFocused)
            {
                if (renderSize.Width >= 2 && renderSize.Height >= 2)
                    drawingContext.DrawRectangle(Brush, BlackDashPen, new Rect(0.5, 0.5, renderSize.Width - 1, renderSize.Height - 1));
                if (renderSize.Width >= 4 && renderSize.Height >= 4)
                    drawingContext.DrawRectangle(Brush, WhitePen, new Rect(1.5, 1.5, renderSize.Width - 3, renderSize.Height - 3));
                if (renderSize.Width >= 6 && renderSize.Height >= 6)
                    drawingContext.DrawRectangle(null, BlackPen, new Rect(2.5, 2.5, renderSize.Width - 5, renderSize.Height - 5));
            }

            if (isKeyboardFocused || CurrentColor == SourceColor || SelectionMode == SelectionMode.ClickMode && IsMouseOver && Mouse.LeftButton == MouseButtonState.Pressed)
            {
                if (renderSize.Width >= 2 && renderSize.Height >= 2)
                    drawingContext.DrawRectangle(Brush, WhitePen, new Rect(0.5, 0.5, renderSize.Width - 1, renderSize.Height - 1));
                if (renderSize.Width >= 4 && renderSize.Height >= 4)
                    drawingContext.DrawRectangle(null, BlackPen, new Rect(1.5, 1.5, renderSize.Width - 3, renderSize.Height - 3));
            }
            else
            {
                drawingContext.DrawRectangle(Brush, null, new Rect(RenderSize));
            }
        }
    }
}