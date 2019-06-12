using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Runtime.CompilerServices;

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
        public Color[] BaseColors => new Color[] { Colors.White, Colors.Black, Colors.Red, Colors.Blue, Colors.Green, Colors.LightGreen, Colors.Pink, Colors.SkyBlue };

        private Color _baseColor;
        public Color BaseColor
        {
            get => _baseColor;
            set => UpdateProperty(ref _baseColor, value);
        }

        private Color _currentColor;
        public Color CurrentColor
        {
            get => _currentColor;
            set => UpdateProperty(ref _currentColor, value);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void UpdateProperty<T>(ref T field, T value, [CallerMemberName]string propertyName = null, string[] dependedProperties = null)
        {
            field = value;
            OnPropertyChanged(propertyName);
        }

        public void OnPropertyChanged([CallerMemberName]string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}