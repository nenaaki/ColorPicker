namespace Oniqys.Collection
{
    public interface IReferenceEnumerator<T>
    {
        ref T Current { get; }

        bool MoveNext();
    }
}
