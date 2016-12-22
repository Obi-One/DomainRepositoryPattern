using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObiOne.DomainRepositoryPattern.Specialized.EF.Tests.DataContext;
using ObiOne.DomainRepositoryPattern.Specialized.EF.Tests.Model;

namespace ObiOne.DomainRepositoryPattern.Specialized.EF.Tests.Tests {
    [TestClass]
    public class AllTypesExampleTest {        
        [TestMethod]
        public void TestDataCRUD(){
            try{
                using (var lDomainEFContext = new TestEFContext(new TestEFConnectionList().TestDbConnection))
                {
                    //ARRANGE
                    var lRepository = lDomainEFContext.GetRepository<AllTypesExample, int>();
                    var l128String = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam pharetra pulvinar mi sed ullamcorper. Suspendisse eu cras amet.";
                    var lTestEntityEmpty = new AllTypesExample();
                    var lTestEntityMin = new AllTypesExample(DateTime.MinValue, -9999999999999999.99m, int.MinValue, string.Empty); //new decimal(-9999999999999.99)
                    var lTestEntityMax = new AllTypesExample(DateTime.MaxValue,  9999999999999999.99m, int.MaxValue, l128String);
                    var lInitialRecordsCount = lRepository.Select().Count();

                    #region INSERT

                    //ACT
                    lRepository.Insert(lTestEntityEmpty);
                    lDomainEFContext.SaveChanges();
                    lRepository.Insert(lTestEntityMin);
                    lDomainEFContext.SaveChanges();
                    lRepository.Insert(lTestEntityMax);
                    lDomainEFContext.SaveChanges();

                    //ASSERT
                    var lFoundTestEntityEmpty = lRepository.Select(lTestEntityEmpty.Id);
                    Assert.IsNotNull(lFoundTestEntityEmpty);
                    Assert.AreEqual(lTestEntityEmpty.DateTime, lFoundTestEntityEmpty.DateTime);
                    Assert.AreEqual(lTestEntityEmpty.DecimalProp, lFoundTestEntityEmpty.DecimalProp);
                    Assert.AreEqual(lTestEntityEmpty.IntProp, lFoundTestEntityEmpty.IntProp);
                    Assert.AreEqual(lTestEntityEmpty.StringProp, lFoundTestEntityEmpty.StringProp);

                    var lFoundTestEntityMin = lRepository.Select(lTestEntityMin.Id);
                    Assert.IsNotNull(lFoundTestEntityMin);
                    Assert.AreEqual(lTestEntityMin.DateTime, lFoundTestEntityMin.DateTime);
                    Assert.AreEqual(lTestEntityMin.DecimalProp, lFoundTestEntityMin.DecimalProp);
                    Assert.AreEqual(lTestEntityMin.IntProp, lFoundTestEntityMin.IntProp);
                    Assert.AreEqual(lTestEntityMin.StringProp, lFoundTestEntityMin.StringProp);

                    var lFoundTestEntityMax = lRepository.Select(lTestEntityMax.Id);
                    Assert.IsNotNull(lFoundTestEntityMax);
                    Assert.AreEqual(lTestEntityMax.DateTime, lFoundTestEntityMax.DateTime);
                    Assert.AreEqual(lTestEntityMax.DecimalProp, lFoundTestEntityMax.DecimalProp);
                    Assert.AreEqual(lTestEntityMax.IntProp, lFoundTestEntityMax.IntProp);
                    Assert.AreEqual(lTestEntityMax.StringProp, lFoundTestEntityMax.StringProp);

                    #endregion

                    #region UPDATE

                    //ACT
                    lFoundTestEntityEmpty.DateTime = DateTime.Now;
                    lFoundTestEntityEmpty.DecimalProp = decimal.One;
                    lFoundTestEntityEmpty.IntProp = 1;
                    lFoundTestEntityEmpty.StringProp = l128String;

                    lRepository.Update(lFoundTestEntityMin);
                    lDomainEFContext.SaveChanges();

                    var lFoundTestEntityChanged = lRepository.Select(lFoundTestEntityEmpty.Id);

                    Assert.IsNotNull(lFoundTestEntityChanged);
                    Assert.AreEqual(lFoundTestEntityEmpty.DateTime, lFoundTestEntityChanged.DateTime);
                    Assert.AreEqual(lFoundTestEntityEmpty.DecimalProp, lFoundTestEntityChanged.DecimalProp);
                    Assert.AreEqual(lFoundTestEntityEmpty.IntProp, lFoundTestEntityChanged.IntProp);
                    Assert.AreEqual(lFoundTestEntityEmpty.StringProp, lFoundTestEntityChanged.StringProp);

                    #endregion

                    #region SELECT ALL AFTER INSERT

                    var lRecordsCountAfterInsert = lRepository.Select().Count();
                    Assert.AreEqual(lInitialRecordsCount + 3, lRecordsCountAfterInsert);

                    #endregion

                    #region DELETE

                    lRepository.Delete(lFoundTestEntityEmpty.Id);
                    lRepository.Delete(lFoundTestEntityMin.Id);
                    lRepository.Delete(lFoundTestEntityMax.Id);

                    lDomainEFContext.SaveChanges();

                    var lFoundTestEntityEmptyDeleted = lRepository.Select(lFoundTestEntityEmpty.Id);
                    Assert.IsNull(lFoundTestEntityEmptyDeleted);
                    var lFoundTestEntityMinDeleted = lRepository.Select(lFoundTestEntityMin.Id);
                    Assert.IsNull(lFoundTestEntityMinDeleted);
                    var lFoundTestEntityMaxDeleted = lRepository.Select(lFoundTestEntityMax.Id);
                    Assert.IsNull(lFoundTestEntityMaxDeleted);

                    #endregion

                    #region SELECT ALL AFTER DELETE

                    var lRecordsCountAfterDelete = lRepository.Select().Count();
                    Assert.AreEqual(lInitialRecordsCount, lRecordsCountAfterDelete);

                    #endregion
                }
            } catch (Exception lException){
                Assert.IsNull(lException, lException.ToString());
            }
        }
    }
}
