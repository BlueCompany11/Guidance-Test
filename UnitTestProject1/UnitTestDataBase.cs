using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Guidance.DataAccessLayer;
using Guidance.FlashCardModel;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity;

namespace UnitTestFlashCards
{
    [TestClass]
    public class UnitTestDataBase
    {
        string serverName = "TestFlashCardsEntities";
        TextAnserw textAnserw1 = new TextAnserw { Annotation = "TestAnnotation", Text = "SampleText1" };
        TextAnserw textAnserw2 = new TextAnserw { Annotation = "TestAnnotation", Text = "SampleText2" };
        FileAnserw fileAnserw1 = new FileAnserw { FileName = "TestFile.jpg", File = new byte[] { 1, 2, 3 }, Annotation = "TestAnnotation" };
        FileAnserw fileAnserw2 = new FileAnserw { FileName = "TestFile.jpg", File = new byte[] { 1, 2, 3 }, Annotation = "TestAnnotation" };
        Tag tag1 = new Tag { Tag1 = "TestTag1" };
        Tag tag2 = new Tag { Tag1 = "TestTag2" };
        FlashCardData flashCardData = new FlashCardData { CreationDate = DateTime.Today };
        string flashCardTitle = "Test1";
        //[TestInitialize]
        //public void TestInit()
        //{
        //    using (FlashCardRepository flashCardRepository = new FlashCardRepository(serverName))
        //    {
        //        var flashCard = new FlashCard
        //        {
        //            Title = flashCardTitle,
        //            FileAnserws = new List<FileAnserw> { fileAnserw1, fileAnserw2 },
        //            TextAnserws = new List<TextAnserw> { textAnserw1, textAnserw2 },
        //            Tags = new List<Tag> { tag1, tag2 },
        //            FlashCardData = flashCardData
        //        };
        //        flashCardRepository.Add(flashCard);
        //    }
        //}

        //private void ClearRepo<T>(T repo) where T: IRepository, new()
        //{
        //    using (T tagRepo = (T)Activator.CreateInstance(typeof(T), serverName))
        //    {
        //        var allTags = tagRepo.GetAll();
        //        foreach (var tag in allTags)
        //        {
        //            tagRepo.Delete(tag);
        //        }
        //    }
        //}
        //private void ClearRepo<T,K>(BaseRepository<T,K> baseRepository)where K:DbContext,new() where T : class, new()
        //{
        //    //Context = (K)Activator.CreateInstance(typeof(K), contextName);
        //    using (T tagRepo = new T(serverName))
        //    {
        //        var allTags = tagRepo.GetAll();
        //        foreach (var tag in allTags)
        //        {
        //            tagRepo.Delete(tag);
        //        }
        //    }
        //}

        //[TestCleanup]
        //public void TestCleanUp()
        //{
        //    using (TagRepository tagRepo = new TagRepository(serverName))
        //    {
        //        var allTags = tagRepo.GetAll();
        //        foreach (var tag in allTags)
        //        {
        //            tagRepo.Delete(tag);
        //        }
        //    }
        //    using (TextAnserwRepository textAnserwRepo = new TextAnserwRepository(serverName))
        //    {
        //        var allTextAnserws = textAnserwRepo.GetAll();
        //        foreach (var text in allTextAnserws)
        //        {
        //            textAnserwRepo.Delete(text);
        //        }
        //    }
        //    using (FlashCardRepository flashCardRepository = new FlashCardRepository(serverName))
        //    {
        //        var allFlashCards = flashCardRepository.GetAll();
        //        foreach (var flashCard in allFlashCards)
        //        {
        //            flashCardRepository.Delete(flashCard);
        //        }
        //    }

        //}

        [TestMethod]
        public void TestFlashCardSetSaveToDB()
        {
            using (FlashCardRepository flashCardRepository = new FlashCardRepository(serverName))
            {
                FlashCard flashCard = new FlashCard()
                {
                    Title = flashCardTitle,
                    FileAnserws = new List<FileAnserw> { fileAnserw1, fileAnserw2 },
                    TextAnserws = new List<TextAnserw> { textAnserw1, textAnserw2 },
                    Tags = new List<Tag> { tag1, tag2 },
                };
                flashCardRepository.Context.FlashCards.Add(flashCard);
                var flashCardsFromDb = flashCardRepository.GetAll();
                var specyficFlashCard = flashCardsFromDb.Find(n => n.Title == flashCardTitle);
                Assert.AreEqual(specyficFlashCard.Title, flashCardTitle);
            }
        }

        [TestMethod]
        public void TestFlashCardDeleteFromDB()
        {
            using (FlashCardRepository flashCardRepository = new FlashCardRepository(serverName))
            {
                var flashCardsFromDb = flashCardRepository.GetAll();
                FlashCard flashCard = flashCardsFromDb.Find(n => n.Title == flashCardTitle);
                using (var textAnserwRepository = new TextAnserwRepository(serverName))
                {
                    foreach (var item in flashCard.TextAnserws)
                    {
                        textAnserwRepository.Delete(item);
                    }
                }
                //using (var fileAnserwRepository = new FileAnserwRepository(serverName))
                //{
                //    foreach (var item in flashCard.FileAnserws)
                //    {
                //        fileAnserwRepository.Delete(item);
                //    }
                //}
                //using (var flashCardDataRepository = new FlashCardDataRepository(serverName))
                //{
                //    if (flashCard.FlashCardData != null)
                //        flashCardDataRepository.Delete(flashCard.FlashCardData);
                //}
                //using (var tagRepository = new TagRepository(serverName))
                //{
                //    foreach (var item in flashCard.Tags)
                //    {
                //        tagRepository.Delete(item);
                //    }
                //}
                Console.WriteLine(flashCard.Id);
                int loadedId = flashCard.Id;
                //flashCardRepository.Delete(flashCard);
                flashCardsFromDb = flashCardRepository.GetAll();
                var specyficFlashCard = flashCardsFromDb.Find(n => n.Id == loadedId);
                Assert.AreEqual(specyficFlashCard, null);
            }
        }
        [TestMethod]
        public void TestFlashCardDeleteFromDB2()
        {
            FlashCard flashCard = new FlashCard { Id = 3013 };
            using(var repo = new FlashCardRepository(serverName))
            {
                repo.Delete(flashCard);
            }
        }
    }
}
