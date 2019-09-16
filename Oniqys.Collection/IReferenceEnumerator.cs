namespace Oniqys.Collection
{
    public interface IReferenceEnumerator<T>
        where T : struct
    {
        ref T Current { get; }

        bool MoveNext();
    }
}
