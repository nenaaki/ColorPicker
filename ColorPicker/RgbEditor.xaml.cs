using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ColorPicker
{
    /// <summary>
    /// RgbEditor.xaml の相互作用ロジック
    /// </summary>
    public partial class RgbEditor : UserControl
    {
        public byte Red
        {
            get => (byte)GetValue(RedProperty);
            set => SetValue(RedProperty, value);
        }
        public static readonly DependencyProperty RedProperty
            = DependencyProperty.Register(nameof(Red), typeof(byte), typeof(RgbEditor), new PropertyMetadata((byte)0,
            (d, e) => ((RgbEditor)d).SyncColor(false)));

        public byte Blue
        {
            get => (byte)GetValue(BlueProperty);
            set => SetValue(BlueProperty, value);
        }
        public static readonly DependencyProperty BlueProperty
            = DependencyProperty.Register(nameof(Blue), typeof(byte), typeof(RgbEditor), new PropertyMetadata((byte)0,
            (d, e) => ((RgbEditor)d).SyncColor(false)));

        public byte Green
        {
            get => (byte)GetValue(GreenProperty);
            set => SetValue(GreenProperty, value);
        }
        public static readonly DependencyProperty GreenProperty
            = DependencyProperty.Register(nameof(Green), typeof(byte), typeof(RgbEditor), new PropertyMetadata((byte)0,
            (d, e) => ((RgbEditor)d).SyncColor(false)));

        public double Saturation
        {
            get => (double)GetValue(SaturationProperty);
            set => SetValue(SaturationProperty, value);
        }
        public static readonly DependencyProperty SaturationProperty
            = DependencyProperty.Register(nameof(Saturation), typeof(double), typeof(RgbEditor), new PropertyMetadata(0.0,
            (d, e) => ((RgbEditor)d).SyncColor(false)));

        public double Brightness
        {
            get => (double)GetValue(BrightnessProperty);
            set => SetValue(BrightnessProperty, value);
        }
        public static readonly DependencyProperty BrightnessProperty
            = DependencyProperty.Register(nameof(Brightness), typeof(double), typeof(RgbEditor), new PropertyMetadata(0.0,
            (d, e) => ((RgbEditor)d).SyncColor(false)));


        public Color CurrentColor
        {
            get => (Color)GetValue(CurrentColorProperty);
            set => SetValue(CurrentColorProperty, value);
        }
        public static readonly DependencyProperty CurrentColorProperty
            = DependencyProperty.Register(nameof(CurrentColor), typeof(Color), typeof(RgbEditor), new FrameworkPropertyMetadata(Colors.Black,
            FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
            (d, e) => ((RgbEditor)d).SyncColor(true)));

        public RgbEditor()
        {
            InitializeComponent();
        }

        private bool _colorUpdating;

        private void SyncColor(bool currentChanged)
        {
            if (_colorUpdating)
                return;

            try
            {
                _colorUpdating = true;
                if (currentChanged)
                {
                    var current = CurrentColor;
                    Red = current.R;
                    Green = current.G;
                    Blue = current.B;
                }
                else
                {
                    CurrentColor = Color.FromRgb(Red, Green, Blue);
                }
                var hsv = HsvColor.FromColor(CurrentColor);
                Saturation = Math.Round(hsv.S * 100);
                Brightness = Math.Round(hsv.V * 100);
            }
            finally
            {
                _colorUpdating = false;
            }
        }
    }
}