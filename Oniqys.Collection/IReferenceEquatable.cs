namespace Oniqys.Collection
{
    /// <summary>
    /// readonly struct 専用の<see cref="Equatable{T}"/>
    /// </summary>
    public interface IReferenceEquatable<T>
        where T : struct
    {
        bool Equals(in T other);
    }
}
