using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Oniqys.Wpf.Controls
{
    /// <summary>
    /// <see cref="INotifyPropertyChanged"/>実装の基底クラスです。
    /// </summary>
    public abstract class ContentBase : INotifyPropertyChanged
    {
        /// <summary>
        /// プロパティの変更を通知します。
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// プロパティに変化があった場合に<see cref="PropertyChanged"/>イベントを発生します。
        /// </summary>
        /// <returns>変更の有無</returns>
        protected bool UpdateProperty<T>(ref T field, T value, [CallerMemberName]string propertyName = null, string[] dependedProperties = null)
            where T : struct, IEquatable<T>
        {
            if (field.Equals(value))
                return false;

            field = value;
            OnPropertyChanged(propertyName);

            if (dependedProperties != null)
                foreach (var prop in dependedProperties)
                    OnPropertyChanged(propertyName);

            return true;
        }

        /// <summary>
        /// プロパティに変化があった場合に<see cref="PropertyChanged"/>イベントを発生します。
        /// </summary>
        /// <returns>変更の有無</returns>
        protected bool UpdateReferenceProperty<T>(ref T field, T value, [CallerMemberName]string propertyName = null, string[] dependedProperties = null)
            where T : class
        {
            if (field == value)
                return false;

            field = value;
            OnPropertyChanged(propertyName);

            if (dependedProperties != null)
                foreach (var prop in dependedProperties)
                    OnPropertyChanged(propertyName);

            return true;
        }

        /// <summary>
        /// <see cref="ProerptyChanged"/>を発生します。
        /// </summary>
        /// <param name="propertyName">プロパティから使用する場合はnullにしてください</param>
        protected void OnPropertyChanged([CallerMemberName]string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}