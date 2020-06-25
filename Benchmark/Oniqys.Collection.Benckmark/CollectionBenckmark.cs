using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace Oniqys.Collection.Benckmark
{
    public class CollectionBenckmark
    {
        public readonly struct Dummy : IEquatable<Dummy>, IReferenceEquatable<Dummy>
        {
            public readonly double Value1;
            public readonly double Value2;
            public readonly double Value3;
            public readonly double Value4;

            public Dummy(double value1, double value2, double value3, double value4)
            {
                Value1 = value1;
                Value2 = value2;
                Value3 = value3;
                Value4 = value4;
            }

            public bool Equals(in Dummy other) => Value1 == other.Value1 && Value2 == other.Value2 && Value3 == other.Value3 && Value4 == other.Value4;
            public bool Equals(Dummy other) => Value1 == other.Value1 && Value2 == other.Value2 && Value3 == other.Value3 && Value4 == other.Value4;
        }

        List<Dummy> _list = new List<Dummy>(10000);

        ReferenceList<Dummy> _reflist = new ReferenceList<Dummy>(10000);

        double[] _results = new double[10000];


        [GlobalSetup]
        public void Setup()
        {
            for (int idx = 0; idx < 10000; idx++)
            {
                _list.Add(new Dummy(idx, idx * 2, idx * 3, idx * 4));
                _reflist.Add(new Dummy(idx, idx * 2, idx * 3, idx * 4));
            }
        }

        [Benchmark]
        public List<Dummy> ToList()
        {
            return GetDummys().ToList();
        }

        [Benchmark]
        public Dummy[] ToArray()
        {
            return GetDummys().ToArray();
        }

        [Benchmark]
        public Dummy[] ToArrayFast()
        {
            return GetDummys().ToArrayFast();
        }

        [Benchmark]
        public ReferenceList<Dummy> ToReferenceList()
        {
            return new ReferenceList<Dummy>(GetDummys());
        }


        [Benchmark]
        public List<Dummy> SystemCollectionListAdd()
        {
            var list = new List<Dummy>();

            for (int idx = 0; idx < 10000; idx++)
            {
                list.Add(new Dummy(idx, idx * 2, idx * 3, idx * 4));
            }
            return list;
        }

        [Benchmark]
        public ReferenceList<Dummy> OniqysCollectionListAdd()
        {
            var list = new ReferenceList<Dummy>();

            for (int idx = 0; idx < 10000; idx++)
            {
                list.Add(new Dummy(idx, idx * 2, idx * 3, idx * 4));
            }
            return list;
        }

        [Benchmark]
        public void SystemCollectionListForeach()
        {
            int idx = 0;
            foreach (var item in _list)
            {
                _results[idx++] = item.Value1 + item.Value2 + item.Value3 + item.Value4;
            }
        }

        [Benchmark]
        public void OniqysCollectionListForeach()
        {
            int idx = 0;
            foreach (ref var item in _reflist)
            {
                _results[idx++] = item.Value1 + item.Value2 + item.Value3 + item.Value4;
            }
        }

        private IEnumerable<Dummy> GetDummys()
        {
            for (int idx = 0; idx < 10000; idx++)
            {
                yield return new Dummy(idx, idx * 2, idx * 3, idx * 4);
            }
        }
    }
}
