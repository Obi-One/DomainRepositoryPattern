using System.ComponentModel.DataAnnotations.Schema;
using ObiOne.DomainRepositoryPattern.Specialized.Specs.Infra;
using ObiOne.DomainRepositoryPattern.Specialized.Specs.Model;

namespace ObiOne.DomainRepositoryPattern.Specialized.EF.Model {
    public abstract class EFEntity<TKey> : IEntity<TKey>, IObjectState
    {
        public abstract TKey Id { get; set; }

        [NotMapped]
        public EnObjectState ObjectState { get; set; }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public object Clone(){
            return MemberwiseClone();
        }
    }
}
