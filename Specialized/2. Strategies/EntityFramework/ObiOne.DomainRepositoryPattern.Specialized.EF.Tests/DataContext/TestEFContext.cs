using System.Data.Entity;
using System.Reflection;
using ObiOne.DomainRepositoryPattern.Specialized.EF.DataContext;
using ObiOne.DomainRepositoryPattern.Specialized.EF.Infra;
using ObiOne.DomainRepositoryPattern.Specialized.EF.Model;
using ObiOne.DomainRepositoryPattern.Specialized.EF.Tests.Model;
using ObiOne.DomainRepositoryPattern.Specialized.EF.Tests.Repository;

namespace ObiOne.DomainRepositoryPattern.Specialized.EF.Tests.DataContext {
    public class TestEFContext : EFContext {
        public TestEFContext(EFConnectionInfo aEFConnectionInfo) : base(aEFConnectionInfo)
        {
            MyDbContext.ModelConfigurationsAssembly = Assembly.GetAssembly(GetType());
        }

        public DbSet<AllTypesExample> AllTypesExampleSet { get; set; }

        public TestEFRepository<TTestEFEntity, TTestEFKey> GetRepository<TTestEFEntity, TTestEFKey>() where TTestEFEntity : EFEntity<TTestEFKey>{
            return new TestEFRepository<TTestEFEntity, TTestEFKey>(this);
        }
    }
}
