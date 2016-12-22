using ObiOne.DomainRepositoryPattern.Specialized.EF.Model;

namespace ObiOne.DomainRepositoryPattern.Specialized.EF.Tests.Model.Base
{
    public class TestEFEntity<TTestEFKey> : EFEntity<TTestEFKey>
    {
        public override TTestEFKey Id { get; set; }
    }
}
