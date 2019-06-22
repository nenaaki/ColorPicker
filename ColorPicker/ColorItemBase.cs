using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace ColorPicker
{
    public abstract class ColorItemBase : FrameworkElement
    {
        protected SolidColorBrush Brush { get; } = new SolidColorBrush();

        public Color CurrentColor
        {
            get { return (Color)GetValue(CurrentColorProperty); }
            set { SetValue(CurrentColorProperty, value); }
        }
        public static readonly DependencyProperty CurrentColorProperty =
            DependencyProperty.Register(nameof(CurrentColor), typeof(Color), typeof(ColorItemBase),
            new FrameworkPropertyMetadata(Colors.Transparent,
            FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.AffectsRender));

        public Color Value
        {
            get { return (Color)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(nameof(Value), typeof(Color), typeof(ColorItemBase),
            new FrameworkPropertyMetadata(Colors.Transparent,
            FrameworkPropertyMetadataOptions.AffectsRender,
            (d, e) => ((ColorItemBase)d).Brush.Color = (Color)e.NewValue));

        protected ColorItemBase()
        {
            MouseDown += (d, e) => CurrentColor = Value;
            MouseMove += (d, e) =>
            {
                if (e.LeftButton != MouseButtonState.Pressed)
                    return;

                CurrentColor = Value;
            };
        }
    }
}
