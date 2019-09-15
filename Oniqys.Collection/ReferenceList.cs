using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Oniqys.Collection
{
    /// <summary>
    /// 高速化可能なリスト。16バイトより大きな構造体を ref によって高速に扱うことが可能です。
    /// Add以外の編集メソッドは実装していません。追加以外の編集を求める場合、<see cref="List{T}"/>を使うべきです。
    /// 空のまま使うべきではありません。最小でキャパシティを4つ確保します。
    /// </summary>
    [DebuggerDisplay("Count = {Count}")]
    public class ReferenceList<T> : IList<T>, IReferenceEnumerable<T, ReferenceList<T>.ReferenceEnumerator>
        where T : struct, IEquatable<T>
    {
        #region Inner Classes

        public struct ReferenceEnumerator : IReferenceEnumerator<T>
        {
            private readonly ReferenceList<T> _list;

            private int _index;

            public ReferenceEnumerator(ReferenceList<T> list)
            {
                _list = list;
                _index = 0;
            }

            public ref T Current => ref _list[_index];

            public bool MoveNext() => ++_index < _list.Count;
        }

        public struct Enumerator : IEnumerator<T>
        {
            private readonly ReferenceList<T> _list;

            private int _index;

            public Enumerator(ReferenceList<T> list)
            {
                _list = list;
                _index = 0;
            }

            public T Current => _list[_index];

            object IEnumerator.Current => Current;

            public void Dispose() { }

            public bool MoveNext() => ++_index < _list.Count;

            public void Reset() => _index = 0;
        }

        #endregion

        private const int DefaultCapacity = 4;

        private int _count;

        T[] _array;

        public ReferenceList()
        {
            _array = new T[DefaultCapacity];
        }

        public ReferenceList(int capacity)
        {
            _array = new T[capacity];
        }

        public ReferenceList(IEnumerable<T> source)
        {
            if (source is ICollection<T> collection)
            {
                _count = collection.Count;
                _array = new T[_count];
                collection.CopyTo(_array, 0);
            }
            else
            {
                _array = new T[DefaultCapacity];
                foreach (var item in source)
                {
                    Add(item);
                }
            }
        }

        public ref T this[int index] => ref _array[index];

        public int Count => _count;

        public bool IsReadOnly => false;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(T item)
        {
            var array = _array;
            var count = _count;
            if (array.Length == count)
            {
                array = new T[count * 2];
                Array.Copy(_array, 0, array, 0, count);
                _array = array;
            }
            array[count] = item;
            _count = count + 1;
        }

        public void Clear() => _count = 0;

        public bool Contains(in T item) => IndexOf(item) >= 0;

        public void CopyTo(T[] array, int arrayIndex)
            => Array.Copy(_array, 0, array, arrayIndex, _count);

        public void CopyTo(T[] array)
            => Array.Copy(_array, 0, array, 0, _count);

        public void CopyTo(int index, T[] array, int arrayIndex, int count)
            => Array.Copy(_array, index, array, arrayIndex, count);

        public ReferenceEnumerator GetEnumerator() => new ReferenceEnumerator(this);

        public int IndexOf(T item)
        {
            for (int idx = 0; idx < _array.Length; idx++)
            {
                ref T ieach = ref _array[idx];

                if (ieach.Equals(item))
                    return idx;
            }
            return -1;
        }

        public void Insert(int index, in T item) => throw new NotSupportedException();

        public bool Remove(in T item) => throw new NotSupportedException();

        public void RemoveAt(int index) => throw new NotSupportedException();

        #region IList<T>

        T IList<T>.this[int index] { get => _array[index]; set => _array[index] = value; }

        IEnumerator IEnumerable.GetEnumerator() => new Enumerator(this);

        int IList<T>.IndexOf(T item) => throw new NotSupportedException();
        void IList<T>.Insert(int index, T item) => throw new NotSupportedException();

        void ICollection<T>.Add(T item) => Add(item);

        bool ICollection<T>.Contains(T item) => Contains(item);

        bool ICollection<T>.Remove(T item) => Remove(item);

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => new Enumerator(this);

        #endregion
    }
}
