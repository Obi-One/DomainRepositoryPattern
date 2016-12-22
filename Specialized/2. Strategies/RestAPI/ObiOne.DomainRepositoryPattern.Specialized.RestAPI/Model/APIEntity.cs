
using ObiOne.DomainRepositoryPattern.Specialized.Specs.Model;

namespace ObiOne.DomainRepositoryPattern.Specialized.RestAPI.Model{
    public abstract class APIEntity<TKey> : IEntity<TKey>{

        public virtual TKey Id { get; set; }

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