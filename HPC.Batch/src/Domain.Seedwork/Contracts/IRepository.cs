namespace Domain.Seedwork.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// It represent an in memory collection of <see cref="U"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    public interface IRepository<T, U> where T : IEntity<U>
    {
        /// <summary>
        /// Get all <see cref="T"/>
        /// </summary>
        /// <returns>A collection of <see cref="T"/></returns>
        Task<IEnumerable<T>> GetAllAsync();
        /// <summary>
        /// Get an <see cref="IEntity<U>"/> by <see cref="U"/>
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns><see cref="T"/> if it exist.</returns>
        Task<T> GetAsync(U identifier);
        /// <summary>
        /// Persist an <see cref="IEntity<U>"/>
        /// </summary>
        /// <param name="entity">The object to be saved.</param>
        Task AddAsync(T entity);
        /// <summary>
        /// Remove an <see cref="IEntity"/>
        /// </summary>
        Task<bool> RemoveAsync(U identifier);
    }
}
