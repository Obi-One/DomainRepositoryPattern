using System;
using ObiOne.DomainRepositoryPattern.Specialized.EF.Infra;
using ObiOne.DomainRepositoryPattern.Specialized.Specs.DataContext;
using ObiOne.DomainRepositoryPattern.Specialized.Specs.Infra;
using ObiOne.DomainRepositoryPattern.Specialized.Specs.Model;

namespace ObiOne.DomainRepositoryPattern.Specialized.EF.DataContext {
    public abstract class EFContext : IDataContext
    {
        protected internal readonly MyDbContext MyDbContext;

        protected EFContext(EFConnectionInfo aEFConnectionInfo) {
            // The terrible hack
            // The Entity Framework provider type 'System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer'
            // for the 'System.Data.SqlClient' ADO.NET provider could not be loaded. 
            // Make sure the provider assembly is available to the running application. 
            // See http://go.microsoft.com/fwlink/?LinkId=260882 for more information.
            // ReSharper disable once UnusedVariable
            var lEnsureDllIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;

            MyDbContext = new MyDbContext(aEFConnectionInfo);
        }

        public int SaveChanges(){
            return MyDbContext.SaveChanges();
        }

        public void SyncObjectState<TEntity, TKey>(TEntity aEntity) where TEntity : class, IEntity<TKey>, IObjectState{
            MyDbContext.SyncObjectState<TEntity, TKey>(aEntity);
        }

        public void SyncObjectsStatePostCommit(){
            MyDbContext.SyncObjectsStatePostCommit();
        }
        
        #region IDisposable
        private bool mDisposed;

        protected virtual void Dispose(bool aDisposing)
        {
            if (!mDisposed)
            {
                if (aDisposing)
                {
                    MyDbContext.Dispose();
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
