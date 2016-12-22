using System.Collections.Generic;
using ObiOne.DomainRepositoryPattern.Specialized.Specs.Model;

namespace ObiOne.DomainRepositoryPattern.Specialized.Specs.Repository {
    /// <summary>
    /// Interface IRepository
    /// </summary>
    /// <typeparam name="TEntity">The type of the t entity.</typeparam>
    /// <typeparam name="TKey">The type of the t key.</typeparam>
    public interface IRepository<TEntity, TKey> where TEntity : IEntity<TKey>{
        /// <summary>
        /// Selects this instance.
        /// </summary>
        /// <returns>IQueryable&lt;TEntity&gt;.</returns>
        IList<TEntity> Select();
        /// <summary>
        /// Selects the specified a identifier.
        /// </summary>
        /// <param name="aID">a identifier.</param>
        /// <returns>TEntity.</returns>
        TEntity Select(TKey aID);
        /// <summary>
        /// Inserts the specified a entity.
        /// </summary>
        /// <param name="aEntity">a entity.</param>
        /// <returns>TEntity.</returns>
        TEntity Insert(TEntity aEntity);
        /// <summary>
        /// Updates the specified a entity.
        /// </summary>
        /// <param name="aEntity">a entity.</param>
        /// <returns>TEntity.</returns>
        TEntity Update(TEntity aEntity);
        /// <summary>
        /// Deletes the specified a identifier.
        /// </summary>
        /// <param name="aID">a identifier.</param>
        void Delete(TKey aID);
    }
}
