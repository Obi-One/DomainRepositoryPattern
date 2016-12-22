using System;
using ObiOne.DomainRepositoryPattern.Specialized.Specs.Infra;
using ObiOne.DomainRepositoryPattern.Specialized.Specs.Model;

namespace ObiOne.DomainRepositoryPattern.Specialized.Specs.DataContext {
    /// <summary>
    /// Interface IDataContext
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public interface IDataContext : IDisposable
    {
        /// <summary>
        /// Saves the changes.
        /// </summary>
        /// <returns>System.Int32.</returns>
        int SaveChanges();
        /// <summary>
        /// Synchronizes the state of the object.
        /// </summary>
        /// <typeparam name="TEntity">The type of the t entity.</typeparam>
        /// <typeparam name="TKey">The type of the t key.</typeparam>
        /// <param name="aEntity">a entity.</param>
        void SyncObjectState<TEntity, TKey>(TEntity aEntity) where TEntity : class, IEntity<TKey>, IObjectState;
        /// <summary>
        /// Synchronizes the objects state post commit.
        /// </summary>
        void SyncObjectsStatePostCommit();
    }
}
