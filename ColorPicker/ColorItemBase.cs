using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Oniqys.Wpf.Controls.ColorPicker.Enums;

namespace Oniqys.Wpf.Controls.ColorPicker
{
    /// <summary>
    /// 選択用の色アイテムの基底クラスです。
    /// </summary>
    public abstract class ColorItemBase : FrameworkElement
    {
        /// <summary>
        /// 黒いペンを取得します。
        /// </summary>
        protected static Pen BlackPen { get; } = new Pen(Brushes.Black, 1);

        /// <summary>
        /// 白いペンを取得します。
        /// </summary>
        protected static Pen WhitePen { get; } = new Pen(Brushes.White, 1);

        /// <summary>
        /// 黒の点線を取得します。
        /// </summary>
        protected static Pen BlackDashPen { get; } = new Pen(Brushes.Black, 1) { DashStyle = new DashStyle(new[] { 1.0, 2.0 }, 0) };

        /// <summary>
        /// 塗りつぶし用のブラシを取得します。
        /// </summary>
        /// <remarks>
        /// インスタンスをそのままに色だけを変更するのでFreezeしません。
        /// </remarks>
        protected SolidColorBrush Brush { get; } = new SolidColorBrush();

        /// <summary>
        /// 選択状態を変化するモードを選択します。
        /// </summary>
        public SelectionMode SelectionMode
        {
            get { return (SelectionMode)GetValue(SelectionModeProperty); }
            set { SetValue(SelectionModeProperty, value); }
        }
        /// <summary>
        /// <see cref="SelectionMode"/>の依存関係プロパていxです。
        /// </summary>
        public static readonly DependencyProperty SelectionModeProperty =
            DependencyProperty.Register("SelectionMode", typeof(SelectionMode), typeof(ColorItemBase), new PropertyMetadata(SelectionMode.PressMode));

        /// <summary>
        /// 現在選択中の色です。
        /// </summary>
        public Color CurrentColor
        {
            get => (Color)GetValue(CurrentColorProperty);
            set => SetValue(CurrentColorProperty, value);
        }
        /// <summary>
        /// <see cref="CurrentColor"/>の依存関係プロパティです。
        /// </summary>
        public static readonly DependencyProperty CurrentColorProperty =
            DependencyProperty.Register(nameof(CurrentColor), typeof(Color), typeof(ColorItemBase),
            new FrameworkPropertyMetadata(Colors.Transparent,
            FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// この色アイテムに設定されている色を取得または設定します。
        /// </summary>
        public Color SourceColor
        {
            get => (Color)GetValue(SourceColorProperty);
            set => SetValue(SourceColorProperty, value);
        }
        /// <summary>
        /// <see cref="SourceColor"/>の依存関係プロパティです。
        /// </summary>
        public static readonly DependencyProperty SourceColorProperty =
            DependencyProperty.Register(nameof(SourceColor), typeof(Color), typeof(ColorItemBase),
            new FrameworkPropertyMetadata(Colors.Transparent,
            FrameworkPropertyMetadataOptions.AffectsRender,
            (d, e) => ((ColorItemBase)d).Brush.Color = (Color)e.NewValue));

        /// <summary>
        /// コンストラクターです。
        /// </summary>
        protected ColorItemBase()
        {
            Focusable = true;
        }

        /// <summary>
        /// 当たり判定します。
        /// </summary>
        protected override HitTestResult HitTestCore(PointHitTestParameters hitTestParameters)
        {
            if (SourceColor == Colors.Transparent)
            {
                var point = hitTestParameters.HitPoint;
                var result = new Rect(RenderSize).Contains(hitTestParameters.HitPoint);
                if (result)
                {
                    return new PointHitTestResult(this, point);
                }
            }

            return base.HitTestCore(hitTestParameters);
        }


        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            UpdateCurrentColor();
            InvalidateVisual();
        }

        protected override void OnLostKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            base.OnLostKeyboardFocus(e);
            InvalidateVisual();
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
            UpdateCurrentColor();
            Focus();
            var command = ColorPickerHelper.GetColorChangeCommand(this);
            command?.Execute(SourceColor);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.LeftButton != MouseButtonState.Pressed)
                return;

            UpdateCurrentColor();
        }

        protected override void OnGotKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            base.OnGotKeyboardFocus(e);
            Focus();
            UpdateCurrentColor();
            InvalidateVisual();
        }

        /// <summary>
        /// 現在の色をこの色アイテムに設定されている色で更新します。
        /// </summary>
        private void UpdateCurrentColor() => CurrentColor = SourceColor;
    }
}