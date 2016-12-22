using System.ComponentModel.DataAnnotations.Schema;

namespace ObiOne.DomainRepositoryPattern.Specialized.Specs.Infra {
    /// <summary>
    /// Interface IObjectState
    /// </summary>
    public interface IObjectState
    {
        /// <summary>
        /// Gets or sets the state of the object.
        /// </summary>
        /// <value>The state of the object.</value>
        [NotMapped]
        EnObjectState ObjectState { get; set; }
    }
}
