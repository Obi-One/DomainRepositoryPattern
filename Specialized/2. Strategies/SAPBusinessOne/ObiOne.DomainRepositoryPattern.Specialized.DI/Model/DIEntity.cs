using ObiOne.DomainRepositoryPattern.Specialized.Specs.Model;

namespace ObiOne.DomainRepositoryPattern.Specialized.DI.Model {
    public abstract class DIEntity<TKey> : IEntity<TKey>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        protected DIEntity(){
        }

        public TKey Id { get; set; }

        public abstract DIEntity<TKey> FromPersistable(dynamic aBusinessObject);

        public abstract dynamic ToPersistable(dynamic aBusinessObject);

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public object Clone(){
            return MemberwiseClone();
        }
    }
}
