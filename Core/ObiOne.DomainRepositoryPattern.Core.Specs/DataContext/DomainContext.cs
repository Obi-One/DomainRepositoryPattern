using System;
using System.Collections.Generic;
using AutoMapper;
using ObiOne.DomainRepositoryPattern.Core.Specs.Model;
using ObiOne.DomainRepositoryPattern.Core.Specs.Repository;
using ObiOne.DomainRepositoryPattern.Specialized.Specs.DataContext;

namespace ObiOne.DomainRepositoryPattern.Core.Specs.DataContext{
    /// <summary>
    /// Class DatasourceModelBase.
    /// </summary>
    public abstract class DomainContext : IDisposable{
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// The data context
        /// </summary>
        protected IDataContext DataContext;

        /// <summary>
        /// The mapper instance
        /// </summary>
        protected IMapper MapperInstance;

        /// <summary>
        /// The m domain repository list
        /// </summary>
        public Dictionary<Type, Type> DomainRepositoryList;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        protected DomainContext(){
            DomainRepositoryList = new Dictionary<Type, Type>();
        }


        /// <summary>
        /// Gets the repository.
        /// </summary>
        /// <typeparam name="TDomainEntity">The type of the t domain entity.</typeparam>
        /// <typeparam name="TId">The type of the t identifier.</typeparam>
        /// <returns>IDomainRepository&lt;TDomainEntity, TDomainKey&gt;.</returns>
        public IDomainRepository<TDomainEntity, TId> GetDomainRepository<TDomainEntity, TId>() where TDomainEntity : IDomainEntity<TDomainEntity, TId>{
            var lType = typeof(TDomainEntity);

            if (!DomainRepositoryList.ContainsKey(lType)) throw new ApplicationException($"Repositorio {lType} não encontrado no contexto {DataContext} do dominio.");

            var lDomainRepository = DomainRepositoryList[lType];

            return (IDomainRepository<TDomainEntity, TId>)Activator.CreateInstance(lDomainRepository, DataContext, MapperInstance);
        }

        #region IDisposable
        private bool mDisposed;

        /// <summary>
        /// Disposes the specified a disposing.
        /// </summary>
        /// <param name="aDisposing">a disposing.</param>
        protected virtual void Dispose(bool aDisposing)
        {
            if (!mDisposed)
            {
                if (aDisposing)
                {
                    //
                }
            }
            mDisposed = true;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}