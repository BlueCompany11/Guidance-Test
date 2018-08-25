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
        public void TestMethodSort()
        {
            FlashCardMemorizer flashCardMemorizer = new FlashCardMemorizer();
            //CreationDate = DateTime.Now.Date;
            //LastOccurrence = DateTime.Now.Date;
            //SuccessfulAnserws = 0;
            //LastResult = false;
            List<FlashCardData> flashCardDatas = new List<FlashCardData>
            {
                new FlashCardData   //przed chwila dodany
                {
                    CreationDate = DateTime.Today.Date,
                    LastOccurrence = DateTime.Now.Date,
                    LastResult = false,
                    SuccessfulAnserws = 0,
                    FlashCardId = 1
                },
                new FlashCardData   //wczorajszy - jeszcze nie przetestowany
                {
                    CreationDate = DateTime.Today.AddDays(-1).Date,
                    LastOccurrence = DateTime.Today.AddDays(-1).Date,
                    LastResult = false,
                    SuccessfulAnserws = 0,
                    FlashCardId = 2
                },
                new FlashCardData   //wczorajszy - przetestowany nastepnego dnia pozytywnie
                {
                    CreationDate = DateTime.Today.AddDays(-1).Date,
                    LastOccurrence = DateTime.Today.AddDays(0).Date,
                    LastResult = true,
                    SuccessfulAnserws = 1,
                    FlashCardId = 3
                },
                new FlashCardData   //wczorajszy - przetestowany nastepnego dnia negatywnie
                {
                    CreationDate = DateTime.Today.AddDays(-1).Date,
                    LastOccurrence = DateTime.Today.AddDays(0).Date,
                    LastResult = false,
                    SuccessfulAnserws = 1,
                    FlashCardId = 4
                },
                new FlashCardData   // 2^SA - (CD - Today) = 0
                {
                    CreationDate = DateTime.Today.AddDays(0).Date,
                    LastOccurrence = DateTime.Today.AddDays(2^5).Date,
                    LastResult = true,
                    SuccessfulAnserws = 5,
                    FlashCardId = 5
                },
                new FlashCardData   // 2^SA - (CD - Today) = 0
                {
                    CreationDate = DateTime.Today.AddDays(0).Date,
                    LastOccurrence = DateTime.Today.AddDays(2^5).Date,
                    LastResult = false,
                    SuccessfulAnserws = 5,
                    FlashCardId = 6
                }
            };
            var x = flashCardMemorizer.Sort(flashCardDatas);
            Assert.AreEqual(new List<int> { 2,6,4,3,5,1 }, x);
        }
    }
}
