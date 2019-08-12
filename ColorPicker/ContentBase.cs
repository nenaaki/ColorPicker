using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Oniqys.Wpf.Controls.ColorPicker
{
    /// <summary>
    /// 通知機能の基底クラスです。
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
        /// <typeparam name="T"></typeparam>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        /// <param name="dependedProperties"></param>
        /// <returns>変更の有無</returns>
        public bool UpdateProperty<T>(ref T field, T value, [CallerMemberName]string propertyName = null, string[] dependedProperties = null)
            where T : struct, IEquatable<T>
        {
            if (field.Equals(value))
                return false;

            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        public bool UpdateReferenceProperty<T>(ref T field, T value, [CallerMemberName]string propertyName = null, string[] dependedProperties = null)
            where T : class
        {
            if (field == value)
                return false;

            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// <see cref="ProerptyChanged"/>を発生します。
        /// </summary>
        /// <param name="propertyName">プロパティから使用する場合はnullにしてください</param>
        public void OnPropertyChanged([CallerMemberName]string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}