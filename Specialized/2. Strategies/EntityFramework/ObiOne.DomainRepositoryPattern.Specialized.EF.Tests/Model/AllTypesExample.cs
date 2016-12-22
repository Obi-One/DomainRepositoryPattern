using System;
using ObiOne.DomainRepositoryPattern.Specialized.EF.Tests.Model.Base;

namespace ObiOne.DomainRepositoryPattern.Specialized.EF.Tests.Model{
    public class AllTypesExample : TestEFEntity<int> {
        public AllTypesExample(){
        }

        public AllTypesExample(DateTime aDateTime, decimal aDecimalProp, int aIntProp, string aStringProp) : this(){
            DateTime = aDateTime;
            DecimalProp = aDecimalProp;
            IntProp = aIntProp;
            StringProp = aStringProp;
        }

        public int IntProp { get; set; }
        public string StringProp { get; set; }
        public decimal DecimalProp { get; set; }
        public DateTime DateTime { get; set; }
    }
}