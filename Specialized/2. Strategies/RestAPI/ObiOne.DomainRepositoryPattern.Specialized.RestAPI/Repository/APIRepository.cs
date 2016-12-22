using System.Collections.Generic;
using System.Linq;
using ObiOne.DomainRepositoryPattern.Specialized.RestAPI.DataContext;
using ObiOne.DomainRepositoryPattern.Specialized.RestAPI.Infra;
using ObiOne.DomainRepositoryPattern.Specialized.RestAPI.Model;
using ObiOne.DomainRepositoryPattern.Specialized.Specs.Repository;

namespace ObiOne.DomainRepositoryPattern.Specialized.RestAPI.Repository{
    public abstract class APIRepository<TAPIEntity, TAPIKey> : IRepository<TAPIEntity, TAPIKey> 
        where TAPIEntity : APIEntity<TAPIKey>, new() where TAPIKey : new(){
        private readonly IAPIRequest<TAPIEntity, TAPIKey> mAPIRequest;

        protected APIRepository(APIContext aAPIContext){
            mAPIRequest = aAPIContext.Set<TAPIEntity, TAPIKey>();
        }

        public TAPIEntity Select(TAPIKey aID){
            return mAPIRequest.GET(aID);
        }

        public TAPIEntity Insert(TAPIEntity aEntity){
            var lAPIKey = mAPIRequest.POST(aEntity);
            return Select(lAPIKey);
        }

        public TAPIEntity Update(TAPIEntity aEntity){
            mAPIRequest.PUT(aEntity);
            return Select(aEntity.Id);
        }

        public void Delete(TAPIKey aID){
            mAPIRequest.DELETE(aID);
        }

        public IList<TAPIEntity> Select(){
            return mAPIRequest.GET()?.ToList();
        }
    }
}