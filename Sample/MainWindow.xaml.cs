using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using System.Globalization;
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

    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public Color[] BaseColors => new Color[]
        {
            Colors.White, Colors.Black, Colors.Red, Colors.Blue, Colors.Green, Colors.LightGreen, Colors.Pink, Colors.SkyBlue, Colors.Azure, Colors.BlueViolet
        };

        public Color[] RecentColors => new Color[]
        {
            Colors.Green, Colors.LightGreen, Colors.Pink, Colors.SkyBlue, Colors.Azure, Colors.BlueViolet, Colors.White, Colors.Black, Colors.Red, Colors.Blue,
        };

        private Color _baseColor = Colors.Green;
        public Color BaseColor
        {
            get => _baseColor;
            set => UpdateProperty(ref _baseColor, value);
        }

        private Color _currentColor = Colors.Green;
        public Color CurrentColor
        {
            get => _currentColor;
            set => UpdateProperty(ref _currentColor, value);
        }

        public event PropertyChangedEventHandler PropertyChanged;

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