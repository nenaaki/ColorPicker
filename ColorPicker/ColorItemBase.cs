using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace ColorPicker
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
            MouseDown += (d, e) => ((ColorItemBase)d).UpdateCurrentColor();
            MouseMove += (d, e) =>
            {
                if (e.LeftButton != MouseButtonState.Pressed)
                    return;

                ((ColorItemBase)d).UpdateCurrentColor();
            };
        }

        /// <summary>
        /// 現在の色をこの色アイテムに設定されている色で更新します。
        /// </summary>
        private void UpdateCurrentColor() => CurrentColor = Value;
    }
}
