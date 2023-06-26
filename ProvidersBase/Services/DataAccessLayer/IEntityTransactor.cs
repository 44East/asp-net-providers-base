namespace ProvidersBase.Services.DataAccessLayer
{
    public interface IEntityTransactor<TEntity> where TEntity : class 
    {
        /// <summary>
        /// Get all the Entities from DB
        /// </summary>
        /// <returns>Collection of the entities</returns>
        Task <IEnumerable<TEntity>> GetAllEntitiesAsync();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">The entity id</param>
        /// <returns>Current entity</returns>
        Task<TEntity> GetEntityByIdAsync(int id);

        /// <summary>
        /// Get the collection of the entities by a tax number from DB
        /// </summary>
        /// <param name="inn">The tax number</param>
        /// <returns>Entities Collection</returns>
        Task<IEnumerable<TEntity>> GetAllEntityByINNAsync(string inn);

        /// <summary>
        /// Create a new entity object in DB
        /// </summary>
        /// <param name="dto">Data transfer object</param>
        /// <returns>Creation result by <see cref="Boolean"/> logic or one of an <see cref="Exception"/></returns>
        Task<bool> CreateEntityAsync(TEntity dto);

        /// <summary>
        /// Update the existing entity in DB
        /// </summary>
        /// <param name="id">The entity id</param>
        /// <param name="dto">Data transfer object</param>
        /// <returns>Updating result by <see cref="Boolean"/> logic or one of an <see cref="Exception"/></returns>
        Task<bool> UpdateEntityAsync(int id, TEntity dto);

        /// <summary>
        /// Delete the existing entity in DB
        /// </summary>
        /// <param name="id">The entity id</param>
        /// <returns>Deleting result by <see cref="Boolean"/> logic or one of an <see cref="Exception"/></returns>
        Task<bool> DeleteEntityAsync(int? id);
    }
}
