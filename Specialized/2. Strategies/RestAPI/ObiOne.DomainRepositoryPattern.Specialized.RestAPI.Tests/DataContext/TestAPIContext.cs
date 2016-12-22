using ObiOne.DomainRepositoryPattern.Specialized.RestAPI.DataContext;
using ObiOne.DomainRepositoryPattern.Specialized.RestAPI.Infra;
using ObiOne.DomainRepositoryPattern.Specialized.RestAPI.Model;
using ObiOne.DomainRepositoryPattern.Specialized.RestAPI.Tests.Model;
using ObiOne.DomainRepositoryPattern.Specialized.RestAPI.Tests.Repository;

namespace ObiOne.DomainRepositoryPattern.Specialized.RestAPI.Tests.DataContext {
    public class TestAPIContext : APIContext {
        public TestAPIContext(APIConnectionInfo aAPIConnectionInfo) : base(aAPIConnectionInfo)
        {
        }

        #region Overrides of APIContext

        protected override void OnModelCreating(APIRequestMapping aAPIRequestMapping){
            base.OnModelCreating(aAPIRequestMapping);
            aAPIRequestMapping.Map<Posts, APIRequest<Posts, int>>("posts");
        }

        #endregion

        public TestAPIRepository<TTestAPIEntity, TTestAPIKey> GetRepository<TTestAPIEntity, TTestAPIKey>() where TTestAPIEntity : APIEntity<TTestAPIKey>, new() where TTestAPIKey : new(){
            return new TestAPIRepository<TTestAPIEntity, TTestAPIKey>(this);
        }
    }
}
