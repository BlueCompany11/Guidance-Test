using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Guidance.DataAccessLayer;
using Guidance.FlashCardModel;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;

namespace UnitTestFlashCards
{
    [TestClass]
    public class UnitTest1
    {
        [TestInitialize]
        public void TestInit()
        {
            //zmienic connection string w flashcardentities
            
        }
        [TestMethod]
        public void TestFlashCardSetSaveToDB()
        {
            using (FlashCardRepository flashCardRepository = new FlashCardRepository("TestFlashCardsEntities"))
            {
                var textAnserw = new TextAnserw { Annotation = "TestAnnotation", Text = "SampleText1" };
                var fileAnserw = new FileAnserw { FileName = "TestFile", File = new byte[] { 1, 2, 3 }, Annotation = "TestAnnotation" };
                var tag = new Tag { Tag1 = "TestTag" };
                var flashCardData = new FlashCardData { CreationDate = DateTime.Today };
                var flashCard = new FlashCard
                {
                    Title = "Test12",
                    FileAnserws = new List<FileAnserw> { fileAnserw },
                    TextAnserws = new List<TextAnserw> { textAnserw },
                    Tags = new List<Tag> { tag },
                    FlashCardData = flashCardData
                };
                flashCardRepository.Add(flashCard);
            }
        }
    }
}
