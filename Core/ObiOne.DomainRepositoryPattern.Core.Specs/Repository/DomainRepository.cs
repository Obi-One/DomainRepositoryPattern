using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ObiOne.DomainRepositoryPattern.Core.Specs.Model;
using ObiOne.DomainRepositoryPattern.Specialized.Specs.DataContext;
using ObiOne.DomainRepositoryPattern.Specialized.Specs.Model;
using ObiOne.DomainRepositoryPattern.Specialized.Specs.Repository;

namespace ObiOne.DomainRepositoryPattern.Core.Specs.Repository
{
    /// <summary>
    /// Class DomainRepository.
    /// </summary>
    /// <typeparam name="TDomainEntity">The type of the t domain entity.</typeparam>
    /// <typeparam name="TDomainId">The type of the t domain identifier.</typeparam>
    /// <typeparam name="TEntity">The type of the t entity.</typeparam>
    /// <typeparam name="TId">The type of the t identifier.</typeparam>
    /// <seealso cref="ObiOne.DomainRepositoryPattern.Core.Specs.Repository.IDomainRepository{TDomainEntity, TDomainId}" />
    /// <seealso cref="ObiOne.DomainRepositoryPattern.Core.Specs.Repository.IDomainRepository{TDomainEntity, TId}" />
    public abstract class DomainRepository<TDomainEntity, TDomainId, TEntity, TId> : IDomainRepository<TDomainEntity, TDomainId> 
        where TDomainEntity : class, IDomainEntity<TDomainEntity, TDomainId>
        where TEntity : class, IEntity<TId>
    {
        /// <summary>
        /// The concrete repository
        /// </summary>
        protected IRepository<TEntity, TId> ConcreteRepository;

        /// <summary>
        /// The mapper
        /// </summary>
        protected readonly IMapper MapperInstance;

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainRepository{TDomainEntity, TDomainId, TEntity, TId}" /> class.
        /// </summary>
        /// <param name="aDataContext">a data context.</param>
        /// <param name="aMapperInstance">a mapper.</param>
        protected DomainRepository(IDataContext aDataContext, IMapper aMapperInstance){
            MapperInstance = aMapperInstance;

            // ReSharper disable once VirtualMemberCallInContructor
            Initialize(aDataContext);
        }

        /// <summary>
        /// Initializes the specified a data context.
        /// </summary>
        /// <param name="aDataContext">a data context.</param>
        protected abstract void Initialize(IDataContext aDataContext);

        /// <summary>
        /// Tries to get an entity by the identifier, if it is not found a exception will be thrown.
        /// </summary>
        /// <param name="aDomainId">The identifier.</param>
        /// <returns>An entity specified by the pedicate.</returns>
        /// <exception cref="System.Collections.Generic.KeyNotFoundException"></exception>
        public TDomainEntity Single(TDomainId aDomainId){
            var lId = MapperInstance.Map<TId>(aDomainId);

            // REPASSA A REQUISIÇÃO PARA O REPOSITORIO ESPECIALIZADO
            var lEntity = ConcreteRepository.Select(lId);

            if (lEntity == null) throw new KeyNotFoundException();

            // CONVERTE A ENTIDADE PERSISTENTE PARA A ENTIDADE DO DOMINIO
            var lDomainEntity = MapperInstance.Map<TDomainEntity>(lEntity);

            // RETORNA A LISTA DE ENTIDADES DE DOMINIO
            return lDomainEntity;
        }

        /// <summary>
        /// Tries to get an entity by the identifier, if it is not found a exception null will be return.
        /// </summary>
        /// <param name="aDomainId">The identifier.</param>
        /// <returns>An entity specified by the pedicate.</returns>
        public TDomainEntity SingleOrDefault(TDomainId aDomainId)
        {
            var lId = MapperInstance.Map<TId>(aDomainId);

            // REPASSA A REQUISIÇÃO PARA O REPOSITORIO ESPECIALIZADO
            var lEntity = ConcreteRepository.Select(lId);

            // CONVERTE A ENTIDADE PERSISTENTE PARA A ENTIDADE DO DOMINIO
            var lDomainEntity = MapperInstance.Map<TDomainEntity>(lEntity);

            // RETORNA A LISTA DE ENTIDADES DE DOMINIO
            return lDomainEntity;
        }

        /// <summary>
        /// Tries to load an entity by the specified identifier, if it is not found a exception will be thrown.
        /// </summary>
        /// <param name="aPredicate">The predicate.</param>
        /// <returns>An entity specified by the identifier.</returns>
        /// <exception cref="System.Collections.Generic.KeyNotFoundException"></exception>
        public TDomainEntity Single(Func<TDomainEntity, bool> aPredicate)
        {
            var lDomainEntity = SingleOrDefault(aPredicate);
            if (lDomainEntity == null) throw new KeyNotFoundException();
            
            return lDomainEntity;
        }

        /// <summary>
        /// Gets the collection with all of domain entities filtered by predicate.
        /// </summary>
        /// <param name="aPredicate">The predicate.</param>
        /// <returns>The collection{TDomainEntity}.</returns>
        public TDomainEntity SingleOrDefault(Func<TDomainEntity, bool> aPredicate)
        {
            // RETORNA A LISTA DE ENTIDADES DE DOMINIO
            var lDomainEntityList = Get();

            // FILTRA A LISTA DE ENTIDADES DE DOMINIO PELO PARAMETRO RECEBIDO
            var lDomainEntity = lDomainEntityList.SingleOrDefault(aPredicate);

            // RETORNA A ENTIDADE DE DOMINIO
            return lDomainEntity;
        }

        /// <summary>
        /// Gets the collection with all of domain entities filtered by predicate.
        /// </summary>
        /// <param name="aPredicate">The predicate.</param>
        /// <returns>The collection{TDomainEntity}.</returns>
        public IList<TDomainEntity> Get(Func<TDomainEntity, bool> aPredicate){
            // RETORNA A LISTA DE ENTIDADES DE DOMINIO
            var lDomainEntityList = Get();

            // FILTRA A LISTA DE ENTIDADES DE DOMINIO PELO PARAMETRO RECEBIDO
            var lDomainEntityListFiltered = lDomainEntityList.Where(aPredicate);

            // RETORNA A LISTA FILTRADA DE ENTIDADES DE DOMINIO
            return lDomainEntityListFiltered.ToList();
        }

        /// <summary>
        /// Gets the collection with all of domain entities.
        /// </summary>
        /// <returns>The collection{TDomainEntity}.</returns>
        public IList<TDomainEntity> Get(){
            // REPASSA A REQUISIÇÃO PARA O REPOSITORIO ESPECIALIZADO
            var lEntityList = ConcreteRepository.Select();

            // CONVERTE A LISTA DE ENTIDADES PERSISTENTE PARA A LISTA DE ENTIDADES DO DOMINIO
            var lDomainEntityList = MapperInstance.Map<List<TDomainEntity>>(lEntityList);

            // RETORNA A LISTA DE ENTIDADES DE DOMINIO
            return lDomainEntityList;
        }

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="aDomainEntity">The entity.</param>
        /// <returns>The identifier.</returns>
        public TDomainEntity Add(TDomainEntity aDomainEntity){
            // CONVERTE A ENTIDADE DO DOMINIO PARA A ENTIDADE PERSISTENTE
            var lEntity = MapperInstance.Map<TEntity>(aDomainEntity);

            // REPASSA A REQUISIÇÃO PARA O REPOSITORIO ESPECIALIZADO
            var lInsert = ConcreteRepository.Insert(lEntity);

            // CONVERTE A ENTIDADE PERSISTENTE PARA A ENTIDADE DO DOMINIO
            var lDomainEntity = MapperInstance.Map<TDomainEntity>(lInsert);

            return lDomainEntity;
        }

        /// <summary>
        /// Upserts the specified a entity.
        /// </summary>
        /// <param name="aDomainEntity">a entity.</param>
        /// <returns>TDomainKey.</returns>
        public TDomainEntity AddOrUpdate(TDomainEntity aDomainEntity)
        {
            var lDomainEntity = SingleOrDefault(aDomainEntity.Id);

            var lAddOrUpdate = lDomainEntity == null
                                   ? Add(aDomainEntity)
                                   : Update(aDomainEntity);

            return lAddOrUpdate;
        }

        /// <summary>
        /// Upserts the specified a entity.
        /// </summary>
        /// <param name="aDomainEntity">a entity.</param>
        /// <param name="aPredicate">The predicate.</param>
        /// <returns>TDomainKey.</returns>
        public TDomainEntity AddOrUpdate(TDomainEntity aDomainEntity, Func<TDomainEntity, bool> aPredicate){
            var lDomainEntity = SingleOrDefault(aPredicate);

            var lAddOrUpdate = lDomainEntity == null
                              ? Add(aDomainEntity)
                              : Update(aDomainEntity);

            return lAddOrUpdate;
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="aDomainEntity">The entity.</param>
        /// <returns>TDomainKey.</returns>
        public TDomainEntity Update(TDomainEntity aDomainEntity){
            // CONVERTE A ENTIDADE DO DOMINIO PARA A ENTIDADE PERSISTENTE
            var lEntity = MapperInstance.Map<TEntity>(aDomainEntity);

            // REPASSA A REQUISIÇÃO PARA O REPOSITORIO ESPECIALIZADO
            var lUpdate = ConcreteRepository.Update(lEntity);

            // CONVERTE A ENTIDADE PERSISTENTE PARA A ENTIDADE DO DOMINIO
            var lDomainEntity = MapperInstance.Map<TDomainEntity>(lUpdate);

            return lDomainEntity;
        }

        /// <summary>
        /// Removes the specified identifier.
        /// </summary>
        /// <param name="aPredicate">a predicate.</param>
        public void Remove(Func<TDomainEntity, bool> aPredicate){
            var lDomainEntity = Single(aPredicate);

            var lEntity = MapperInstance.Map<TEntity>(lDomainEntity);

            ConcreteRepository.Delete(lEntity.Id);
        }

        /// <summary>
        /// Removes the specified identifier.
        /// </summary>
        /// <param name="aDomainId">a identifier.</param>
        public void Remove(TDomainId aDomainId)
        {
            var lDomainEntity = Single(aDomainId);

            var lEntity = MapperInstance.Map<TEntity>(lDomainEntity);

            ConcreteRepository.Delete(lEntity.Id);
        }
    }
}
