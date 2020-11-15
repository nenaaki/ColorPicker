using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace Oniqys.Wpf.Controls.Slicer
{
    class SlicerContent : ContentBase
    {
        public ObservableCollection<SlicerItemContent> Items { get; } = new ObservableCollection<SlicerItemContent>();

        private string _name;

        /// <summary>
        /// 名称を取得または設定します。
        /// </summary>
        public string Name
        {
            get => _name;
            set => UpdateReferenceProperty(ref _name, value);
        }

        private Color _captionColor;

        public Color CaptionColor
        {
            get => _captionColor;
            set => UpdateProperty(ref _captionColor, value);
        }

        private bool _isEnabled = true;

        public bool IsEnabled
        {
            get => _isEnabled;
            set => UpdateProperty(ref _isEnabled, value);
        }
    }
}
