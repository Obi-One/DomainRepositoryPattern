using System.Data.Entity.ModelConfiguration;
using ObiOne.DomainRepositoryPattern.Specialized.EF.Tests.Model;

namespace ObiOne.DomainRepositoryPattern.Specialized.EF.Tests.Map
{
    public class AllTypesExampleMap : EntityTypeConfiguration<AllTypesExample>
    {
        public AllTypesExampleMap(){
            ToTable("AllTypesExample");

            HasKey(aOCRD => aOCRD.Id);
        }
    }
}
