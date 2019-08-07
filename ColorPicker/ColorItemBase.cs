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
        public Color Value
        {
            get => (Color)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }
        /// <summary>
        /// <see cref="Value"/>の依存関係プロパティです。
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(nameof(Value), typeof(Color), typeof(ColorItemBase),
            new FrameworkPropertyMetadata(Colors.Transparent,
            FrameworkPropertyMetadataOptions.AffectsRender,
            (d, e) => ((ColorItemBase)d).Brush.Color = (Color)e.NewValue));

        /// <summary>
        /// コンストラクターです。
        /// </summary>
        /// <remarks>
        /// マウスイベントを設定します。
        /// </remarks>
        protected ColorItemBase()
        {
            MouseDown += OnMouseDown;
            MouseMove += OnMouseMove;
            MouseUp += OnMouseUp;
        }

        /// <summary>
        /// <see cref="FrameworkElement.MouseUp"/>イベントを処理します。
        /// </summary>
        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            UpdateCurrentColor();
        }

        /// <summary>
        /// <see cref="FrameworkElement.MouseDown"/>イベントを処理します。
        /// </summary>
        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (SelectionMode == SelectionMode.ClickMode)
            {
                InvalidateVisual();
                return;
            }

            UpdateCurrentColor();
        }

        /// <summary>
        /// <see cref="FrameworkElement.MouseMove"/>イベントを処理します。
        /// </summary>
        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (SelectionMode == SelectionMode.ClickMode || e.LeftButton != MouseButtonState.Pressed)
                return;

            UpdateCurrentColor();
        }

        /// <summary>
        /// 現在の色をこの色アイテムに設定されている色で更新します。
        /// </summary>
        private void UpdateCurrentColor() => CurrentColor = Value;
    }
}
