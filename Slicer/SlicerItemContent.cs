using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace Oniqys.Wpf.Controls.Slicer
{
    public class SlicerItemContent : ContentBase
    {
        private string _name;

        /// <summary>
        /// 名称を取得または設定します。
        /// </summary>
        public string Name
        {
            get => _name;
            set => UpdateReferenceProperty(ref _name, value);
        }

        private bool _isSelected;

        public bool IsSelected
        {
            get => _isSelected;
            set => UpdateProperty(ref _isSelected, value);
        }

        /// <summary>
        /// 選択状態を変更するコマンドです。
        /// </summary>
        public Command SelectionChangeCommand { get; set; }
    }
}
