using System;
using ObiOne.DomainRepositoryPattern.Specialized.RestAPI.Infra;
using ObiOne.DomainRepositoryPattern.Specialized.RestAPI.Model;
using ObiOne.DomainRepositoryPattern.Specialized.Specs.DataContext;
using ObiOne.DomainRepositoryPattern.Specialized.Specs.Infra;
using ObiOne.DomainRepositoryPattern.Specialized.Specs.Model;

namespace ObiOne.DomainRepositoryPattern.Specialized.RestAPI.DataContext{
    public abstract class APIContext : IDataContext
    {
        private readonly APIConnectionInfo mAPIConnectionInfo;

        public APIRequestMapping APIRequestMapping { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        protected APIContext(APIConnectionInfo aAPIConnectionInfo)
        {
            mAPIConnectionInfo = aAPIConnectionInfo;

            APIRequestMapping = new APIRequestMapping();

            // ReSharper disable once VirtualMemberCallInContructor
            OnModelCreating(APIRequestMapping);
        }

        protected virtual void OnModelCreating(APIRequestMapping aAPIRequestMapping)
        {
        }

        public APIRequest<TAPIEntity, TAPIKey> Set<TAPIEntity, TAPIKey>() 
            where TAPIEntity : APIEntity<TAPIKey>, new() where TAPIKey : new(){
            return APIRequestMapping.CreateInstance<TAPIEntity, TAPIKey>(mAPIConnectionInfo);
        }

        //public APIRepository<TAPIEntity, TAPIKey> GetAPIRepository<TAPIEntity, TAPIKey>()
        //    where TAPIEntity : APIEntity<TAPIKey>, new() where TAPIKey : new()
        //{
        //    return new APIRepository<TAPIEntity, TAPIKey>(this);
        //}

        #region IDisposable
        private bool mDisposed;

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

        public int SaveChanges(){
            throw new NotSupportedException();
        }

        public void SyncObjectState<TEntity, TKey>(TEntity aEntity) where TEntity : class, IEntity<TKey>, IObjectState{
            throw new NotSupportedException();
        }

        public void SyncObjectsStatePostCommit(){
            throw new NotSupportedException();
        }
    }
}