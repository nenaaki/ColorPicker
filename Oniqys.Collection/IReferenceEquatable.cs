namespace Oniqys.Collection
{
    public interface IReferenceEquatable<T>
        where T : struct
    {
        bool Equals(ref T other);
    }
}
