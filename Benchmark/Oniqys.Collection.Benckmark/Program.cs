using BenchmarkDotNet.Running;

namespace Oniqys.Collection.Benckmark
{
    public static class Program
    {
        public static void Main()
        {
#if DEBUG
            var benchmark = new CollectionBenckmark();
            benchmark.Setup();

            benchmark.OniqysCollectionListAdd();
#else
            BenchmarkRunner.Run<CollectionBenckmark>();
#endif
        }
    }
}
