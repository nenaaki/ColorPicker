using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Oniqys.Wpf.Core
{
    public static class FreezableExtensions
    {
        public static T WithFreeze<T>(this T freezable) where T : Freezable { if (freezable.CanFreeze) { freezable.Freeze(); } return freezable; }
    }
}
