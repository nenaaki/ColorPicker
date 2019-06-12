using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ColorPicker
{
    public class Selectable<T> : INotifyPropertyChanged
    {
        private T _value;
        public T Value
        {
            get => _value;
            set => UpdateProperty(ref _value, value);
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set => UpdateProperty(ref _isSelected, value);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void UpdateProperty<TValue>(ref TValue field, TValue value, [CallerMemberName]string propertyName = null, string[] dependedProperties = null)
        {
            field = value;
            OnPropertyChanged(propertyName);
        }

        public void OnPropertyChanged([CallerMemberName]string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}