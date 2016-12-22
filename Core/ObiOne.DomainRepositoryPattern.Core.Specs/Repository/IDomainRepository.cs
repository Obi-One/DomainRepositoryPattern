using System;
using System.Collections.Generic;
using ObiOne.DomainRepositoryPattern.Core.Specs.Model;

namespace ObiOne.DomainRepositoryPattern.Core.Specs.Repository {
    /// <summary>
    /// Interface IDomainRepository
    /// </summary>
    /// <typeparam name="TDomainEntity">The type of the domain entity.</typeparam>
    /// <typeparam name="TDomainId">The type of the t identifier.</typeparam>
    public interface IDomainRepository<TDomainEntity, TDomainId> where TDomainEntity : IDomainEntity<TDomainEntity, TDomainId>
    {
        /// <summary>
        /// Tries to get an entity by the identifier, if it is not found a exception will be thrown.
        /// </summary>
        /// <param name="aDomainId">The identifier.</param>
        /// <returns>An entity specified by the pedicate.</returns>
        TDomainEntity Single(TDomainId aDomainId);

        /// <summary>
        /// Tries to get an entity by the identifier, if it is not found null will be return.
        /// </summary>
        /// <param name="aDomainId">The identifier.</param>
        /// <returns>An entity specified by the pedicate.</returns>
        TDomainEntity SingleOrDefault(TDomainId aDomainId);

        /// <summary>
        /// Tries to get an entity by the pedicate, if it is not found a exception will be thrown.
        /// </summary>
        /// <param name="aPredicate">The predicate.</param>
        /// <returns>An entity specified by the pedicate.</returns>
        TDomainEntity Single(Func<TDomainEntity, bool> aPredicate);

        /// <summary>
        /// Tries to get an entity by the pedicate, if it is not found null will be return.
        /// </summary>
        /// <param name="aPredicate">The predicate.</param>
        /// <returns>An entity specified by the pedicate.</returns>
        TDomainEntity SingleOrDefault(Func<TDomainEntity, bool> aPredicate);

        /// <summary>
        /// Gets the collection with all of domain entities.
        /// </summary>
        /// <returns>The collection{TDomainEntity}.</returns>
        IList<TDomainEntity> Get();

        /// <summary>
        /// Gets the collection with all of domain entities.
        /// </summary>
        /// <param name="aPredicate">The predicate.</param>
        /// <returns>The collection{TDomainEntity}.</returns>
        IList<TDomainEntity> Get(Func<TDomainEntity, bool> aPredicate);

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="aDomainEntity">The entity.</param>
        /// <returns>The identifier.</returns>
        TDomainEntity Add(TDomainEntity aDomainEntity);

        /// <summary>
        /// Upserts the specified a entity.
        /// </summary>
        /// <param name="aDomainEntity">a entity.</param>
        /// <returns>TDomainKey.</returns>
        TDomainEntity AddOrUpdate(TDomainEntity aDomainEntity);

        /// <summary>
        /// Adds the or update.
        /// </summary>
        /// <param name="aDomainEntity">a domain entity.</param>
        /// <param name="aPredicate">a predicate.</param>
        TDomainEntity AddOrUpdate(TDomainEntity aDomainEntity, Func<TDomainEntity, bool> aPredicate);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="aDomainEntity">The entity.</param>
        /// <returns>TDomainKey.</returns>
        TDomainEntity Update(TDomainEntity aDomainEntity);

        /// <summary>
        /// Removes the specified identifier.
        /// </summary>
        /// <param name="aPredicate">a predicate.</param>
        void Remove(Func<TDomainEntity, bool> aPredicate);

        /// <summary>
        /// Removes the specified a identifier.
        /// </summary>
        /// <param name="aDomainId">a identifier.</param>
        void Remove(TDomainId aDomainId);
    }
}
