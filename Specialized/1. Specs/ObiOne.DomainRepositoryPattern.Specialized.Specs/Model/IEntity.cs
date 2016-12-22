using System;

namespace ObiOne.DomainRepositoryPattern.Specialized.Specs.Model{
    /// <summary>
    /// Interface IEntity
    /// </summary>
    /// <typeparam name="TKey">The type of the t key.</typeparam>
    /// <seealso cref="System.ICloneable" />
    public interface IEntity<TKey> : ICloneable {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        TKey Id { get; set; }
    }
}