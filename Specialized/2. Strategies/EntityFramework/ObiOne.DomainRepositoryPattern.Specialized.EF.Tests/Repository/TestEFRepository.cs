using ObiOne.DomainRepositoryPattern.Specialized.EF.DataContext;
using ObiOne.DomainRepositoryPattern.Specialized.EF.Model;
using ObiOne.DomainRepositoryPattern.Specialized.EF.Repository;

namespace ObiOne.DomainRepositoryPattern.Specialized.EF.Tests.Repository{
    public class TestEFRepository<TTestEFEntity, TTestEFKey> : EFRepository<TTestEFEntity, TTestEFKey> where TTestEFEntity : EFEntity<TTestEFKey>{
        public TestEFRepository(EFContext aEFContext) : base(aEFContext){
        }
    }
}
