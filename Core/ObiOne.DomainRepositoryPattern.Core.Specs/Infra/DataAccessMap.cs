using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ObiOne.DomainRepositoryPattern.Core.Specs.Model;
using ObiOne.DomainRepositoryPattern.Specialized.Specs.Model;
using ObiOne.DomainRepositoryPattern.Specialized.Specs.Repository;

namespace ObiOne.DomainRepositoryPattern.Core.Specs.Infra{
    /// <summary>
    /// Class DataAccessMap.
    /// </summary>
    [Serializable]
    public class DataAccessMap : Dictionary<Type, Type> {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Collections.Generic.Dictionary`2"/> class with serialized data.
        /// </summary>
        /// <param name="aInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo"/> object containing the information required to serialize the <see cref="T:System.Collections.Generic.Dictionary`2"/>.</param><param name="aContext">A <see cref="T:System.Runtime.Serialization.StreamingContext"/> structure containing the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Generic.Dictionary`2"/>.</param>
        protected DataAccessMap(SerializationInfo aInfo, StreamingContext aContext) : base(aInfo, aContext){
        }

        /// <summary>
        /// Add a data access map to model through repository.
        /// </summary>
        /// <typeparam name="TDomainEntity">The type of the model.</typeparam>
        /// <typeparam name="TId">The type of the t identifier.</typeparam>
        /// <typeparam name="TRepository">The type of the repository.</typeparam>
        public void Add<TDomainEntity, TId, TRepository>() 
            where TDomainEntity : IDomainEntity<TDomainEntity, TId>
        {
            Add(typeof(TDomainEntity), typeof(TRepository));
        }

        /// <summary>
        /// Creates the instance.
        /// </summary>
        /// <typeparam name="TDomainEntity">The type of the model.</typeparam>
        /// <typeparam name="TKey">The type of the t key.</typeparam>
        /// <param name="args">The arguments.</param>
        /// <returns>New instance of .</returns>
        public IRepository<IEntity<dynamic>, dynamic> CreateInstance<TDomainEntity, TKey>(params object[] args) 
            where TDomainEntity : IDomainEntity<TDomainEntity, TKey>
        {
            return (IRepository<IEntity<dynamic>, dynamic>) Activator.CreateInstance(RepositoryOf<TDomainEntity>(), args);
        }

        /// <summary>
        /// Gets the type of the data access repository of the model.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <returns>The data access repository type.</returns>
        public Type RepositoryOf<TModel>(){
            return this[typeof (TModel)];
        }
    }
}