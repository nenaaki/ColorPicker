using System;
using System.Windows;
using System.Windows.Media;

namespace ColorPicker
{
    /// <summary>
    /// 六角形の選択色アイテムです。
    /// </summary>
    public sealed class HexColorItem : ColorItemBase
    {
        private static readonly StreamGeometry _hexGeometry = new StreamGeometry();
        private static readonly StreamGeometry _edgeGeometry = new StreamGeometry();
        private static readonly StreamGeometry _cursorGeometry = new StreamGeometry();

        private static bool _initialized;

        /// <summary>
        /// コンストラクターです。
        /// </summary>
        /// <remarks>
        /// 初回でジオメトリを構築しFreezeします。
        /// </remarks>
        public HexColorItem()
        {
            if (!_initialized)
            {
                _initialized = true;
                UpdateHexGeometory(_hexGeometry, 10);
                UpdateHexGeometory(_edgeGeometry, 9.5);
                UpdateHexGeometory(_cursorGeometry, 8.5);
            }

            void UpdateHexGeometory(StreamGeometry geometry, double radius)
            {
                const double pi3 = Math.PI / 3;

                using (var c = geometry.Open())
                {
                    c.BeginFigure(new Point(0, radius), true, true);
                    for (int idx = 1; idx < 6; idx++)
                    {
                        var p = new Point(radius * Math.Sin(idx * pi3), radius * Math.Cos(idx * pi3));
                        c.LineTo(p, true, false);
                    }
                }
                geometry.Freeze();
            }
        }

        /// <inheritdoc />
        /// <remarks>
        /// ６角系を描画します。
        /// </remarks>
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            drawingContext.DrawGeometry(Brush, null, _hexGeometry);

            if (CurrentColor == Value)
            {
                drawingContext.DrawGeometry(null, WhitePen, _edgeGeometry);
                drawingContext.DrawGeometry(null, BlackPen, _cursorGeometry);
            }
        }
    }
}
