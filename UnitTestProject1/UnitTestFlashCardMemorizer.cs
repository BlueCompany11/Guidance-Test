using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Guidance.FlashCardModel;

namespace UnitTestProject1
{
    /// <summary>
    /// Summary description for UnitTestFlashCardMemorizer
    /// </summary>
    [TestClass]
    public class UnitTestFlashCardMemorizer
    {
        public UnitTestFlashCardMemorizer()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion
        [TestMethod]
        public void GetRecallValueCheckIfFailedAreFirst()
        {
            FlashCardData fcdFail = new FlashCardData();
            fcdFail.LastOccurrence = DateTime.Today.AddDays(-1);
            FlashCardData fcdSuccess = new FlashCardData();
            fcdSuccess.LastOccurrence = DateTime.Today.AddDays(-1);
            fcdSuccess.LastResult = true;
            var flashCardMemorzer = new FlashCardMemorizer();
            int valFail = flashCardMemorzer.GetRecallValue(fcdFail);
            int valSuccess = flashCardMemorzer.GetRecallValue(fcdSuccess);
            Assert.IsTrue(valFail <= valSuccess);

            //even if there is some very old pending 
            fcdSuccess.LastOccurrence = DateTime.Today.AddDays(-100);
            fcdSuccess.CreationDate = DateTime.Today.AddDays(-101);
            valSuccess = flashCardMemorzer.GetRecallValue(fcdSuccess);
            Assert.IsTrue(valFail <= valSuccess);
        }
        [TestMethod]
        public void GetRecallValueCheckIfYesterdayIsBeforeToday()
        {
            FlashCardData fcdToday = new FlashCardData();
            FlashCardData fcdYesterday = new FlashCardData();
            fcdYesterday.CreationDate = DateTime.Today.AddDays(-1);
            fcdYesterday.LastOccurrence = DateTime.Today.AddDays(-1);
            var flashCardMemorizer = new FlashCardMemorizer();
            int valToday = flashCardMemorizer.GetRecallValue(fcdToday);
            int valYesterday = flashCardMemorizer.GetRecallValue(fcdYesterday);
            Assert.IsTrue(valYesterday <= valToday);
        }
        [TestMethod]
        public void GetRecallValueCompareOrdianry()
        {
            FlashCardData fcd1 = new FlashCardData();
            fcd1.CreationDate = DateTime.Today.AddDays(-10);
            fcd1.LastOccurrence = DateTime.Today.AddDays(-2);
            fcd1.SuccessfulAnserws = 3;
            fcd1.LastResult = true;
            FlashCardData fcd2 = new FlashCardData();
            fcd2.CreationDate = DateTime.Today.AddDays(-10);
            fcd2.LastOccurrence = DateTime.Today.AddDays(-1);
            fcd2.SuccessfulAnserws = 3;
            fcd2.LastResult = true;
            var flashCardMemorizer = new FlashCardMemorizer();
            int val1 = flashCardMemorizer.GetRecallValue(fcd1);
            int val2 = flashCardMemorizer.GetRecallValue(fcd2);
            Assert.IsTrue(val1 <= val2);
            fcd2.SuccessfulAnserws = 1;
            val2 = flashCardMemorizer.GetRecallValue(fcd2);
            Assert.IsTrue(val1 >= val2);

            //fcd1 should be first beacuse it was memorized less times
            fcd1 = new FlashCardData();
            fcd1.CreationDate = DateTime.Today.AddDays(-20);
            fcd1.LastOccurrence = DateTime.Today.AddDays(-4);
            fcd1.SuccessfulAnserws = 5;
            fcd1.LastResult = true;
            fcd2 = new FlashCardData();
            fcd2.CreationDate = DateTime.Today.AddDays(-30);
            fcd2.LastOccurrence = DateTime.Today.AddDays(-10);
            fcd2.SuccessfulAnserws = 7;
            fcd2.LastResult = true;
            val1 = flashCardMemorizer.GetRecallValue(fcd1);
            val2 = flashCardMemorizer.GetRecallValue(fcd2);
            Assert.IsTrue(val1<=val2);
        }
        [TestMethod]
        public void GetRecallValueCheckIfReturnedValuesAreNotSame()
        {
            FlashCardData fcd1 = new FlashCardData();
            fcd1.CreationDate = DateTime.Today.AddDays(-20);
            fcd1.LastOccurrence = DateTime.Today.AddDays(-10);
            fcd1.SuccessfulAnserws = 1;
            FlashCardData fcd2 = new FlashCardData();
            var flashCardMemorizer = new FlashCardMemorizer();
            int val1 = flashCardMemorizer.GetRecallValue(fcd1);
            int val2 = flashCardMemorizer.GetRecallValue(fcd2);
            Assert.IsTrue(val1 < val2);
        }

            //[TestMethod]
            //public void GetRecallValueCheckIf
            //[TestMethod]
            //public void TestMethodSort()
            //{
            //    FlashCardMemorizer flashCardMemorizer = new FlashCardMemorizer();
            //    //CreationDate = DateTime.Now.Date;
            //    //LastOccurrence = DateTime.Now.Date;
            //    //SuccessfulAnserws = 0;
            //    //LastResult = false;
            //    List<FlashCardData> flashCardDatas = new List<FlashCardData>
            //    {
            //        new FlashCardData   //przed chwila dodany
            //        {
            //            CreationDate = DateTime.Today.Date,
            //            LastOccurrence = DateTime.Now.Date,
            //            LastResult = false,
            //            SuccessfulAnserws = 0,
            //            FlashCardId = 1
            //        },
            //        new FlashCardData   //wczorajszy - jeszcze nie przetestowany
            //        {
            //            CreationDate = DateTime.Today.AddDays(-1).Date,
            //            LastOccurrence = DateTime.Today.AddDays(-1).Date,
            //            LastResult = false,
            //            SuccessfulAnserws = 0,
            //            FlashCardId = 2
            //        },
            //        new FlashCardData   //wczorajszy - przetestowany nastepnego dnia pozytywnie
            //        {
            //            CreationDate = DateTime.Today.AddDays(-1).Date,
            //            LastOccurrence = DateTime.Today.AddDays(0).Date,
            //            LastResult = true,
            //            SuccessfulAnserws = 1,
            //            FlashCardId = 3
            //        },
            //        new FlashCardData   //wczorajszy - przetestowany nastepnego dnia negatywnie
            //        {
            //            CreationDate = DateTime.Today.AddDays(-1).Date,
            //            LastOccurrence = DateTime.Today.AddDays(0).Date,
            //            LastResult = false,
            //            SuccessfulAnserws = 1,
            //            FlashCardId = 4
            //        },
            //        new FlashCardData   // 2^SA - (CD - Today) = 0
            //        {
            //            CreationDate = DateTime.Today.AddDays(0).Date,
            //            LastOccurrence = DateTime.Today.AddDays(2^5).Date,
            //            LastResult = true,
            //            SuccessfulAnserws = 5,
            //            FlashCardId = 5
            //        },
            //        new FlashCardData   // 2^SA - (CD - Today) = 0
            //        {
            //            CreationDate = DateTime.Today.AddDays(0).Date,
            //            LastOccurrence = DateTime.Today.AddDays(2^5).Date,
            //            LastResult = false,
            //            SuccessfulAnserws = 5,
            //            FlashCardId = 6
            //        }
            //    };
            //    var x = flashCardMemorizer.Sort(flashCardDatas);
            //    Assert.AreEqual(new List<int> { 2,6,4,3,5,1 }, x);
            //}
        }
}
