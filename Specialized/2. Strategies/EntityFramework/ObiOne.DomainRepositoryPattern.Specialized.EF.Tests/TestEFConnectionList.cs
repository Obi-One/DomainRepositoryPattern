using ObiOne.DomainRepositoryPattern.Specialized.EF.Infra;

namespace ObiOne.DomainRepositoryPattern.Specialized.EF.Tests
{
    public class TestEFConnectionList{
        public EFConnectionInfo TestDbConnection => mTestDbAtGennera;

        private readonly EFConnectionInfo mTestDbAtGennera = new EFConnectionInfo("b1cert", "sa", "b1admin", "TestDb", EnInitializer.DropCreateDatabaseIfModelChanges);
        private readonly EFConnectionInfo mTestDbAtDeathstar = new EFConnectionInfo("DEATHSTAR", "sa", "sa123", "TestDb", EnInitializer.DropCreateDatabaseIfModelChanges);
        private readonly EFConnectionInfo mTestDbAtHercules = new EFConnectionInfo("172.16.1.132", "sa", "Luciin!4", "TestDb", EnInitializer.DropCreateDatabaseIfModelChanges);
    }
}
