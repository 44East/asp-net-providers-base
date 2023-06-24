namespace ProvidersBase.Services.Mappers
{
    /// <summary>
    /// Interface for data mapping, for the database entities 
    /// </summary>
    /// <typeparam name="TSource">Data from</typeparam>
    /// <typeparam name="TDestination">Data to</typeparam>
    public interface IMapper<TSource, TDestination> where TDestination : class where TSource : class
    {
        /// <summary>
        /// Entity to Entity mapping
        /// </summary>
        /// <param name="source">Data 'from' object</param>
        /// <returns>Mapped object</returns>
        TDestination Map(TSource source);

        /// <summary>
        /// Get the collection mapping by <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="source">Collection of entities</param>
        /// <returns>Mapped collection</returns>
        IEnumerable<TDestination> Map(IEnumerable<TSource> source);

        /// <summary>
        /// Entity to Entity mapping
        /// </summary>
        /// <param name="destination">DTO object</param>
        /// <returns>Mapped entity for DB</returns>
        TSource ReverseMap(TDestination destination);

        /// <summary>
        /// Get the remapped collection by <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="destination">DTO collection</param>
        /// <returns>Mapped entities collection for DB</returns>
        IEnumerable<TSource> ReverseMap(IEnumerable<TDestination> destination);
    }
}
