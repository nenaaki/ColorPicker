using System;
using System.ComponentModel;
using System.Linq;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ColorPicker
{
    /// <summary>
    /// GradualColorPickerSlot.xaml の相互作用ロジック
    /// </summary>
    public partial class GradualColorPickerSlot : ItemsControl
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
            = DependencyProperty.Register(nameof(CurrentColor), typeof(Color), typeof(GradualColorPickerSlot), 
                new FrameworkPropertyMetadata(Colors.Transparent,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                (d, e) => ((GradualColorPickerSlot)d).SyncColor()));

        public Color[] BaseColors
        {
            get => (Color[])GetValue(BaseColorsProperty);
            set => SetValue(BaseColorsProperty, value);
        }
        public static readonly DependencyProperty BaseColorsProperty =
            DependencyProperty.Register(nameof(BaseColors), typeof(Color[]), typeof(GradualColorPickerSlot), 
                new FrameworkPropertyMetadata(null,
                (d, e)=> { ((GradualColorPickerSlot)d).ItemsSource = e.NewValue as Color[]; }));

        public GradualColorPickerSlot()
        {
            InitializeComponent();
        }

        private bool _updating;
        private void SyncColor()
        {
            if (_updating)
                return;

            try
            {
                _updating = true;
            }
            finally
            {
                _updating = false;
            }
        }
    }
}