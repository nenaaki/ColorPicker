using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Media;
using System.Windows.Threading;

namespace Oniqys.Wpf.Controls.ColorPicker
{
    /// <summary>
    /// 最近使った色を管理するコレクションです。
    /// UIスレッド以外で使用することはできません。
    /// </summary>
    /// <remarks>
    /// UIスレッド以外で使用すると<see cref="Capacity"/>が正しく機能しません。
    /// 動作に<see cref="Dispatcher"/>を必要とします。
    /// </remarks>
    public class RecentColorManager : ObservableCollection<Color>
    {
        /// <summary>
        /// デフォルトの保持数です。
        /// </summary>
        public const int DefaultCapacity = 16;

        /// <summary>
        /// 更新処理中の状態を保持します。
        /// </summary>
        private bool _updating;

        /// <summary>
        /// デフォルトで使用するマネージャーです。
        /// </summary>
        public static RecentColorManager DefaultInstance = new RecentColorManager();

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

        /// <summary>
        /// コレクション変更時に保持数内に収めます。
        /// イベント内では処理できないので、Dispatcherに処理を送ります。
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
        /// 最近使った色を登録して、順序を整えます。
        /// </summary>
        public void Register(Color newColor)
        {
            for (int idx = Count - 1; idx >= 0; idx--)
            {
                if (this[idx] == newColor)
                    RemoveAt(idx);
            }
            Insert(0, newColor);
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
