namespace Oniqys.Collection
{
    public interface IReferenceEnumerable<T, TEnumerator>
        where TEnumerator : struct, IReferenceEnumerator<T>
    {
        TEnumerator GetEnumerator();
    }
}
