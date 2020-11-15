using System;

namespace Oniqys.Wpf
{
    public struct Updater
    {
        private bool _updating;

        public void Update(Action updater)
        {
            if (_updating)
                return;

            try
            {
                _updating = true;
                updater?.Invoke();
            }
            finally
            {
                _updating = false;
            }
        }
    }
}
