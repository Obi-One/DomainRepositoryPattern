using System;

namespace ObiOne.DomainRepositoryPattern.Core.Specs.Model {
    /// <summary>
    /// Interface IDomainEntity
    /// </summary>
    public interface IDomainEntity<T, TId> : IEquatable<T> where T : IDomainEntity<T, TId>
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        TId Id { get; set; }

        /// <summary>
        /// Gets or sets the global unique identifier.
        /// </summary>
        /// <value>The global unique identifier.</value>
        Guid Guid { get; set; }
    }
}
