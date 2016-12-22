using ObiOne.DomainRepositoryPattern.Specialized.RestAPI.DataContext;
using ObiOne.DomainRepositoryPattern.Specialized.RestAPI.Model;
using ObiOne.DomainRepositoryPattern.Specialized.RestAPI.Repository;

namespace ObiOne.DomainRepositoryPattern.Specialized.RestAPI.Tests.Repository{
    public class TestAPIRepository<TTestAPIEntity, TTestAPIKey> : APIRepository<TTestAPIEntity, TTestAPIKey> where TTestAPIEntity : APIEntity<TTestAPIKey>, new() where TTestAPIKey : new(){
        public TestAPIRepository(APIContext aAPIContext) : base(aAPIContext)
        {
        }
    }
}
