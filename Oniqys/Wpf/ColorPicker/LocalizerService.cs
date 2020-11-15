using System.Globalization;
using Oniqys.Wpf.Controls.ColorPicker.Properties;

namespace Oniqys.Wpf.Controls.ColorPicker
{
    /// <summary>
    /// 多言語化されたリソースと、言語の切り替え機能を提供します。
    /// </summary>
    public class LocalizerService : ContentBase
    {
        public static LocalizerService Instance { get; } = new LocalizerService();

        /// <summary>
        /// 多言語化されたリソースを取得します。
        /// </summary>
        public Resources Resources { get; } = new Resources();

        public CultureInfo Culture
        {
            get => Resources.Culture;
            set
            {
                if (Resources.Culture == value)
                    return;

                Resources.Culture = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Resources));
            }
        }
    }
}
