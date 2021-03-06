﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using Oniqys.Wpf;
using Oniqys.Wpf.Controls.ColorPicker;

namespace Sample
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainWindowViewModel();
        }
    }

    public class DummyComboBox : ColorPickerComboBox
    {
        public new IEnumerable<Color> ItemsSource
        {
            get { return (IEnumerable<Color>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }
        public static readonly new DependencyProperty ItemsSourceProperty
            = DependencyProperty.Register(nameof(ItemsSource), typeof(IEnumerable<Color>), typeof(DummyComboBox), new PropertyMetadata(null,
            (d, e) =>
            {
                var owner = (ColorPickerComboBox)d;

                var newValue = ((IEnumerable<Color>)e.NewValue)?.ToArray();
                if (newValue?.Length > 0)
                {
                    var defaultColor = newValue[0];
                    owner.DefaultColor = defaultColor;
                    if (defaultColor == Colors.Transparent)
                    {
                        owner.DefaultColorName = "Transparent";
                    }
                    else
                    {
                        owner.DefaultColorName = "Default Color";
                    }
                    owner.ItemsSource = newValue.Skip(1).ToArray();
                }
                else
                {
                    owner.DefaultColorName = "Transparent";
                    owner.DefaultColor = Colors.Transparent;
                    owner.ItemsSource = (Color[])Enumerable.Empty<Color>();
                }
            }));
    }

    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public Command SampleCommand { get; }

        public Command DisabledCommand { get; }

        public Color[] BaseColors => new Color[]
        {
            Colors.Black, Colors.White, Colors.Red, Colors.Blue, Colors.Green, Colors.LightGreen, Colors.Pink, Colors.SkyBlue, Colors.Azure, Colors.BlueViolet
        };

        public Color[] RecentColors => new Color[]
        {
            Colors.Green, Colors.LightGreen, Colors.Pink, Colors.SkyBlue, Colors.Azure, Colors.BlueViolet, Colors.White, Colors.Black, Colors.Red, Colors.Blue,
        };

        private Color _currentColor = Colors.Green;
        public Color CurrentColor
        {
            get => _currentColor;
            set => UpdateProperty(ref _currentColor, value);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindowViewModel()
        {
            SampleCommand = new Command(() => CurrentColor = Colors.Red);
            DisabledCommand = new Command(() => CurrentColor = Colors.Red, () => false);
        }

        public void UpdateProperty<T>(ref T field, T value, [CallerMemberName]string propertyName = null, string[] dependedProperties = null)
            where T : IEquatable<T>
        {
            if (field.Equals(value))
                return;

            field = value;
            OnPropertyChanged(propertyName);
        }

        public void OnPropertyChanged([CallerMemberName]string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}