using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using ObiOne.DomainRepositoryPattern.Specialized.EF.DataContext;
using ObiOne.DomainRepositoryPattern.Specialized.EF.Model;
using ObiOne.DomainRepositoryPattern.Specialized.Specs.DataContext;
using ObiOne.DomainRepositoryPattern.Specialized.Specs.Infra;
using ObiOne.DomainRepositoryPattern.Specialized.Specs.Repository;

namespace ObiOne.DomainRepositoryPattern.Specialized.EF.Repository {
    public abstract class EFRepository<TEFEntity, TEFKey> : IRepository<TEFEntity, TEFKey> where TEFEntity : EFEntity<TEFKey>{
        #region Private Fields

        private readonly IDataContext mContext;
        private readonly DbSet<TEFEntity> mDbSet;

        #endregion Private Fields

        protected EFRepository(EFContext aEFContext){
            if (aEFContext == null) throw new ArgumentNullException(nameof(aEFContext));

            mContext = aEFContext;

            var lDbContext = aEFContext.MyDbContext;
            mDbSet = lDbContext.Set<TEFEntity>();
            //var lCount = mDbSet.Local.Count;
        }

        public virtual TEFEntity Select(TEFKey aID)
        {
            return mDbSet.Find(aID);
        }

        public virtual TEFEntity Insert(TEFEntity aEntity){
            aEntity.ObjectState = EnObjectState.Added;
            var lEFEntity = mDbSet.Attach(aEntity);
            mContext.SyncObjectState<TEFEntity, TEFKey>(aEntity);
            return lEFEntity;
        }

        public virtual TEFEntity Update(TEFEntity aEntity)
        {
            aEntity.ObjectState = EnObjectState.Modified;
            var lEFEntity = mDbSet.Attach(aEntity);
            mContext.SyncObjectState<TEFEntity, TEFKey>(aEntity);
            return lEFEntity;
        }

        public virtual void Delete(TEFKey aID)
        {
            var lEntity = mDbSet.Find(aID);
            Delete(lEntity);
        }

        private void Delete(TEFEntity aEntity)
        {
            aEntity.ObjectState = EnObjectState.Deleted;
            mDbSet.Attach(aEntity);
            mContext.SyncObjectState<TEFEntity, TEFKey>(aEntity);
        }

        public IList<TEFEntity> Select(){
            var lIncludeList = typeof(TEFEntity).GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(aPropertyInfo => aPropertyInfo.CanWrite && aPropertyInfo.GetGetMethod().IsVirtual && aPropertyInfo.PropertyType.IsGenericType).Select(aInfo => aInfo.Name).ToList();

            return (lIncludeList.Any()
                        ? lIncludeList.Aggregate((IQueryable<TEFEntity>) mDbSet, (aCurrent, aExpression) => aCurrent.Include(aExpression))
                        : mDbSet).AsNoTracking().ToList();
        }
    }
}
