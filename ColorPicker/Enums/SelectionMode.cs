using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oniqys.Wpf.ColorPicker.Enums
{
    /// <summary>
    /// 選択状態を変更するために必要な操作を規定します。
    /// </summary>
    public enum SelectionMode
    {
        /// <summary>
        /// 押下中に選択状態が変化する
        /// </summary>
        PressMode,

        /// <summary>
        /// クリックで選択状態が変化する
        /// </summary>
        ClickMode,
    }
}
