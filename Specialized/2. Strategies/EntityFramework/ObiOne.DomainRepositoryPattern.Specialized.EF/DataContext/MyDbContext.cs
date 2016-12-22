using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SqlClient;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using ObiOne.DomainRepositoryPattern.Specialized.EF.Infra;
using ObiOne.DomainRepositoryPattern.Specialized.Specs.Infra;
using ObiOne.DomainRepositoryPattern.Specialized.Specs.Model;

namespace ObiOne.DomainRepositoryPattern.Specialized.EF.DataContext
{
    public class MyDbContext : DbContext
    {
        public Assembly ModelConfigurationsAssembly;

        #region Private Fields
        bool mDisposed;
        #endregion Private Fields
        
        public MyDbContext(EFConnectionInfo aEFConnectionInfo) : base(new SqlConnection(aEFConnectionInfo.Conn), true) {
            ModelConfigurationsAssembly = Assembly.GetAssembly(GetType());

            switch (aEFConnectionInfo.Initializer){
                case EnInitializer.SuppressDatabaseInitialization:
                    Database.SetInitializer<MyDbContext>(null);
                    break;
                case EnInitializer.CreateDatabaseIfNotExists:
                    Database.SetInitializer(new CreateDatabaseIfNotExists<MyDbContext>());
                    break;
                case EnInitializer.DropCreateDatabaseIfModelChanges:
                    Database.SetInitializer(new DropCreateDatabaseIfModelChanges<MyDbContext>());
                    break;
                case EnInitializer.DropCreateDatabaseAlways:
                    Database.SetInitializer(new DropCreateDatabaseAlways<MyDbContext>());
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            InstanceId = Guid.NewGuid();
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder aModelBuilder)
        {
            // Configure Code First to ignore PluralizingTableName convention 
            aModelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // Change datetime properties to SQL Datetime2
            aModelBuilder.Properties<DateTime>()
                         .Configure(aConfiguration => aConfiguration.HasColumnType("datetime2"));

            aModelBuilder.Configurations.AddFromAssembly(ModelConfigurationsAssembly);

            base.OnModelCreating(aModelBuilder);
        }

        public Guid InstanceId { get; }

        /// <summary>
        ///     Saves all changes made in this context to the underlying database.
        /// </summary>
        /// <exception cref="System.Data.Entity.Infrastructure.DbUpdateException">
        ///     An error occurred sending updates to the database.</exception>
        /// <exception cref="System.Data.Entity.Infrastructure.DbUpdateConcurrencyException">
        ///     A database command did not affect the expected number of rows. This usually
        ///     indicates an optimistic concurrency violation; that is, a row has been changed
        ///     in the database since it was queried.</exception>
        /// <exception cref="System.Data.Entity.Validation.DbEntityValidationException">
        ///     The save was aborted because validation of entity property values failed.</exception>
        /// <exception cref="System.NotSupportedException">
        ///     An attempt was made to use unsupported behavior such as executing multiple
        ///     asynchronous commands concurrently on the same context instance.</exception>
        /// <exception cref="System.ObjectDisposedException">
        ///     The context or connection have been disposed.</exception>
        /// <exception cref="System.InvalidOperationException">
        ///     Some error occurred attempting to process entities in the context either
        ///     before or after sending commands to the database.</exception>
        /// <seealso cref="DbContext.SaveChanges"/>
        /// <returns>The number of objects written to the underlying database.</returns>
        public override int SaveChanges(){
            SyncObjectsStatePreCommit();
            var lChanges = base.SaveChanges();
            SyncObjectsStatePostCommit();
            return lChanges;
        }

        /// <summary>
        ///     Asynchronously saves all changes made in this context to the underlying database.
        /// </summary>
        /// <exception cref="System.Data.Entity.Infrastructure.DbUpdateException">
        ///     An error occurred sending updates to the database.</exception>
        /// <exception cref="System.Data.Entity.Infrastructure.DbUpdateConcurrencyException">
        ///     A database command did not affect the expected number of rows. This usually
        ///     indicates an optimistic concurrency violation; that is, a row has been changed
        ///     in the database since it was queried.</exception>
        /// <exception cref="System.Data.Entity.Validation.DbEntityValidationException">
        ///     The save was aborted because validation of entity property values failed.</exception>
        /// <exception cref="System.NotSupportedException">
        ///     An attempt was made to use unsupported behavior such as executing multiple
        ///     asynchronous commands concurrently on the same context instance.</exception>
        /// <exception cref="System.ObjectDisposedException">
        ///     The context or connection have been disposed.</exception>
        /// <exception cref="System.InvalidOperationException">
        ///     Some error occurred attempting to process entities in the context either
        ///     before or after sending commands to the database.</exception>
        /// <seealso cref="DbContext.SaveChangesAsync(System.Threading.CancellationToken)"/>
        /// <returns>A task that represents the asynchronous save operation.  The 
        ///     <see cref="Task.FromResult{TResult}">Task.Result</see> contains the number of 
        ///     objects written to the underlying database.</returns>
        public override async Task<int> SaveChangesAsync(){
            return await SaveChangesAsync(CancellationToken.None);
        }

        /// <summary>
        ///     Asynchronously saves all changes made in this context to the underlying database.
        /// </summary>
        /// <exception cref="System.Data.Entity.Infrastructure.DbUpdateException">
        ///     An error occurred sending updates to the database.</exception>
        /// <exception cref="System.Data.Entity.Infrastructure.DbUpdateConcurrencyException">
        ///     A database command did not affect the expected number of rows. This usually
        ///     indicates an optimistic concurrency violation; that is, a row has been changed
        ///     in the database since it was queried.</exception>
        /// <exception cref="System.Data.Entity.Validation.DbEntityValidationException">
        ///     The save was aborted because validation of entity property values failed.</exception>
        /// <exception cref="System.NotSupportedException">
        ///     An attempt was made to use unsupported behavior such as executing multiple
        ///     asynchronous commands concurrently on the same context instance.</exception>
        /// <exception cref="System.ObjectDisposedException">
        ///     The context or connection have been disposed.</exception>
        /// <exception cref="System.InvalidOperationException">
        ///     Some error occurred attempting to process entities in the context either
        ///     before or after sending commands to the database.</exception>
        /// <seealso cref="DbContext.SaveChangesAsync()"/>
        /// <returns>A task that represents the asynchronous save operation.  The 
        ///     <see cref="Task.FromResult{TResult}">Task.Result</see> contains the number of 
        ///     objects written to the underlying database.</returns>
        public override async Task<int> SaveChangesAsync(CancellationToken aCancellationToken){
            SyncObjectsStatePreCommit();
            var lChangesAsync = await base.SaveChangesAsync(aCancellationToken);
            SyncObjectsStatePostCommit();
            return lChangesAsync;
        }

        public void SyncObjectState<TEntity, TKey>(TEntity aEntity) where TEntity : class, IEntity<TKey>, IObjectState{
            Entry(aEntity)
                .State = StateHelper.ConvertState(aEntity.ObjectState);
        }

        private void SyncObjectsStatePreCommit(){
            foreach (var lDbEntityEntry in ChangeTracker.Entries()){
                lDbEntityEntry.State = StateHelper.ConvertState(((IObjectState) lDbEntityEntry.Entity).ObjectState);
            }
        }

        public void SyncObjectsStatePostCommit(){
            foreach (var lDbEntityEntry in ChangeTracker.Entries()){
                ((IObjectState) lDbEntityEntry.Entity).ObjectState = StateHelper.ConvertState(lDbEntityEntry.State);
            }
        }

        protected override void Dispose(bool aDisposing){
            if (!mDisposed){
                if (aDisposing){
                    // free other managed objects that implement
                    // IDisposable only
                }

                // release any unmanaged objects
                // set object references to null

                mDisposed = true;
            }

            base.Dispose(aDisposing);
        }
    }
}
