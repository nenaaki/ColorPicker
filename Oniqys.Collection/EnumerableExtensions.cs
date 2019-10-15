﻿using System;
using System.Collections.Generic;

namespace Oniqys.Collection
{
    public static class EnumerableExtensions
    {
        public static T[] ToArrayFast<T>(this IEnumerable<T> source)
        {
            if (source is ICollection<T> collection)
            {
                var count = collection.Count;
                var result = new T[count];

                collection.CopyTo(result, 0);
                return result;
            }
            else
            {
                var list = new List<T[]>(16);

                int size = 16;
                int count = 0;
                var array = new T[size];

                int index = 0;
                foreach (var item in source)
                {
                    if (array.Length == index)
                    {
                        list.Add(array);
                        size += size;
                        array = new T[size];
                        index = 0;
                    }

                    array[index++] = item;
                    count++;
                }

                var result = new T[count];
                index = 0;
                foreach (var items in list)
                {
                    var length = items.Length;
                    Array.Copy(items, 0, result, index, length);
                    index += length;
                }
                Array.Copy(array, 0, result, index, count - index);

                return result;
            }
        }

        public static ReferenceList<T> ToReferenceList<T>(this IEnumerable<T> source) where T : struct, IEquatable<T>
            => new ReferenceList<T>(source);
    }
}