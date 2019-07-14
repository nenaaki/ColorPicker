using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Threading;
using System.Windows.Media;

namespace ColorPicker
{
    /// <summary>
    /// 最近使った色を管理するコレクション
    /// </summary>
    public class RecentColorManager : ObservableCollection<Color>
    {
        /// <summary>
        /// デフォルトの保持数です。
        /// </summary>
        public const int DefaultCapacity = 16;

        /// <summary>
        /// デフォルトで使用するマネージャーです。
        /// </summary>
        public static RecentColorManager DefaultInstane = new RecentColorManager();

        /// <summary>
        /// 保持数を取得または設定します。
        /// </summary>
        public int Capacity { get; set; }

        /// <summary>
        /// コンストラクターです。
        /// </summary>
        public RecentColorManager(int capacity = DefaultCapacity)
        {
            Capacity = capacity;

            CollectionChanged += OnCollectionChanged;
        }

        private bool _updating;

        /// <summary>
        /// コレクション変更時に保持数内に収めます。
        /// </summary>
        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (_updating)
                return;

            _updating = true;

            Dispatcher.CurrentDispatcher.BeginInvoke(new Action(() =>
            {
                while (Capacity < Count)
                    RemoveAt(Count - 1);

                _updating = false;

            }), DispatcherPriority.DataBind);
        }

        /// <summary>
        /// 複数件追加します。
        /// </summary>
        public void AddRange(IEnumerable<Color> colors)
        {
            foreach (var color in colors)
                Add(color);
        }
    }
}
