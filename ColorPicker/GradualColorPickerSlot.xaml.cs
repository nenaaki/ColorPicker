using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ColorPicker
{
    /// <summary>
    /// GradualColorPickerSlot.xaml の相互作用ロジック
    /// </summary>
    public partial class GradualColorPickerSlot : UserControl
    {
        public int ContentLength
        {
            get { return (int)GetValue(ContentLengthProperty); }
            set { SetValue(ContentLengthProperty, value); }
        }
        public static readonly DependencyProperty ContentLengthProperty
            = DependencyProperty.Register(nameof(ContentLength), typeof(int), typeof(GradualColorPickerSlot), new FrameworkPropertyMetadata(8,
                (d, e) => { }));

        public bool Expanded
        {
            get { return (bool)GetValue(ExpandedProperty); }
            set { SetValue(ExpandedProperty, value); }
        }
        public static readonly DependencyProperty ExpandedProperty
            = DependencyProperty.Register(nameof(Expanded), typeof(bool), typeof(GradualColorPickerSlot), new FrameworkPropertyMetadata(false,
                (d, e) => { }));

        public Color CurrentColor
        {
            get { return (Color)GetValue(CurrentColorProperty); }
            set { SetValue(CurrentColorProperty, value); }
        }
        public static readonly DependencyProperty CurrentColorProperty
            = DependencyProperty.Register(nameof(CurrentColor), typeof(Color), typeof(GradualColorPickerSlot), new FrameworkPropertyMetadata(default(Color),
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                (d, e) => { }));

        public Color[] BaseColors
        {
            get => (Color[])GetValue(BaseColorsProperty);
            set => SetValue(BaseColorsProperty, value);
        }
        public static readonly DependencyProperty BaseColorsProperty =
            DependencyProperty.Register(nameof(BaseColors), typeof(Color[]), typeof(GradualColorPickerSlot), new PropertyMetadata(null,
                (d, e) =>
                {
                    var owner = (GradualColorPickerSlot)d;
                    var colors = (Color[])e.NewValue;
                    owner.ColorPickerContents = colors != null
                        ? colors.Select(c => new GradualColorPickerContent(owner) { BaseColor = c }).ToArray()
                        : (new GradualColorPickerContent[0]);
                }));

        public GradualColorPickerContent[] ColorPickerContents
        {
            get => (GradualColorPickerContent[])GetValue(ColorPickerContentsProperty);
            set => SetValue(ColorPickerContentsProperty, value);
        }
        public static readonly DependencyProperty ColorPickerContentsProperty =
            DependencyProperty.Register(nameof(ColorPickerContents), typeof(GradualColorPickerContent[]), typeof(GradualColorPickerSlot), new PropertyMetadata(null));

        public GradualColorPickerSlot()
        {
            InitializeComponent();
        }
    }

    public class GradualColorPickerContent : INotifyPropertyChanged
    {
        private readonly GradualColorPickerSlot _owner;

        private Color _baseColor;
        public Color BaseColor
        {
            get => _baseColor;
            set => UpdateProperty(ref _baseColor, value);
        }

        private Color _currentColor = Colors.Red;
        public Color CurrentColor
        {
            get => _currentColor;
            set
            {
                if (UpdateProperty(ref _currentColor, value))
                {
                    _owner.CurrentColor = value;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public GradualColorPickerContent(GradualColorPickerSlot owner)
        {
            _owner = owner;
        }

        public bool UpdateProperty<T>(ref T field, T value, [CallerMemberName]string propertyName = null, string[] dependedProperties = null)
            where T : IEquatable<T>
        {
            if (field.Equals(value))
                return false;

            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        public void OnPropertyChanged([CallerMemberName]string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}