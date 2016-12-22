using System;
using System.Collections.Generic;
using ObiOne.DomainRepositoryPattern.Specialized.DI.DataContext;
using ObiOne.DomainRepositoryPattern.Specialized.DI.Infra;
using ObiOne.DomainRepositoryPattern.Specialized.DI.Model;
using ObiOne.DomainRepositoryPattern.Specialized.Specs.Repository;

namespace ObiOne.DomainRepositoryPattern.Specialized.DI.Repository
{
    public abstract class DIRepository<TDIEntity, TDIKey> : IRepository<TDIEntity, TDIKey> where TDIEntity : DIEntity<TDIKey>
    {
        private readonly DIContext mDIContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        protected DIRepository(DIContext aDIContext){
            mDIContext = aDIContext;
            mDIContext.Set<TDIEntity>();
        }

        public TDIEntity Select(TDIKey aID){
            dynamic lBusinessObject = mDIContext.Set<TDIEntity>();

            if (!lBusinessObject.GetByKey(aID)) return null;

            var lDIEntity = (TDIEntity) Activator.CreateInstance(typeof (TDIEntity));
            lDIEntity.FromPersistable(lBusinessObject);

            System.Runtime.InteropServices.Marshal.ReleaseComObject(lBusinessObject);
            // ReSharper disable once RedundantAssignment
            lBusinessObject = null;
            GC.Collect();

            return lDIEntity;
        }
        
        public TDIEntity Insert(TDIEntity aEntity){
            dynamic lBusinessObject = mDIContext.Set<TDIEntity>();
            lBusinessObject = aEntity.ToPersistable(lBusinessObject);
            mDIContext.AsException((int)lBusinessObject.Add());
            var lNewObjectKey = mDIContext.SboCompany.GetNewObjectKey();
            var lTypedNewObjectKey = (TDIKey)Convert.ChangeType(lNewObjectKey, typeof (TDIKey));

            System.Runtime.InteropServices.Marshal.ReleaseComObject(lBusinessObject);
            // ReSharper disable once RedundantAssignment
            lBusinessObject = null;
            GC.Collect();

            return Select(lTypedNewObjectKey);
        }
        
        public TDIEntity Update(TDIEntity aEntity){
            dynamic lBusinessObject = mDIContext.Set<TDIEntity>();
            if (!lBusinessObject.GetByKey(aEntity.Id)) throw new KeyNotFoundException();
            lBusinessObject = aEntity.ToPersistable(lBusinessObject);
            mDIContext.AsException((int)lBusinessObject.Update());

            System.Runtime.InteropServices.Marshal.ReleaseComObject(lBusinessObject);
            // ReSharper disable once RedundantAssignment
            lBusinessObject = null;
            GC.Collect();

            return Select(aEntity.Id);
        }

        public void Delete(TDIKey aID){
            dynamic lBusinessObject = mDIContext.Set<TDIEntity>();
            if (!lBusinessObject.GetByKey(aID)) throw new KeyNotFoundException();

            mDIContext.AsException((int)lBusinessObject.Remove());

            System.Runtime.InteropServices.Marshal.ReleaseComObject(lBusinessObject);
            // ReSharper disable once RedundantAssignment
            lBusinessObject = null;
            GC.Collect();

        }

        public IList<TDIEntity> Select(){
            throw new NotSupportedException();
        }
    }
}
