using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Guidance.DataAccessLayer;
using Guidance.FlashCardModel;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity;
using System.Linq;

namespace UnitTestFlashCards
{
    [TestClass]
    public class UnitTestDataBase
    {
        private FlashCard GetFlashCard()
        {
            TextAnserw textAnserw1 = new TextAnserw { Annotation = "TestAnnotation", Text = "SampleText1" };
            TextAnserw textAnserw2 = new TextAnserw { Annotation = "TestAnnotation", Text = "SampleText2" };
            FileAnserw fileAnserw1 = new FileAnserw { FileName = "TestFile.jpg", File = new byte[] { 1, 2, 3 }, Annotation = "TestAnnotation" };
            FileAnserw fileAnserw2 = new FileAnserw { FileName = "TestFile.jpg", File = new byte[] { 1, 2, 3 }, Annotation = "TestAnnotation" };
            Tag tag1 = new Tag { Tag1 = "TestTag111" };
            Tag tag2 = new Tag { Tag1 = "TestTag222" };
            FlashCardData flashCardData = new FlashCardData();
            string flashCardTitle = "UnitTest1";
            var flashCard = new FlashCard
            {
                Title = flashCardTitle,
                FileAnserws = new List<FileAnserw> { fileAnserw1, fileAnserw2 },
                TextAnserws = new List<TextAnserw> { textAnserw1, textAnserw2 },
                Tags = new List<Tag> { tag1, tag2 },
                FlashCardData = flashCardData
            };
            return flashCard;
        }

        [TestMethod]
        public void AddExistingTagToAnotherFlashCard()
        {
            //FlashCard flashCard = new FlashCard { Title = "a" };
            //using (var repo = new FlashCardRepository())
            //{
            //    repo.Add(flashCard);
            //}
            var flashCard = new FlashCard { Id = 25012, Title = "a" };
            using (var repo = new TagRepository())
            {
                Tag tag = repo.GetOne("asd"); //new Tag { Tag1 = "asd" };
                //var flashCard = repo.Context.FlashCards.Find(25012);
                repo.Context.Entry(flashCard).State = EntityState.Modified;
                tag.FlashCards.Add(flashCard);
                repo.Context.SaveChanges();
            }
        }
        //dziala dobrze - poprawnie inicjalzuje tagi
        [TestMethod]
        public void AddDetachedFlashCard1()
        {
            var flashCard = GetFlashCard();
            using (var repo = new FlashCardRepository())
            {
                repo.Context.Entry(flashCard).State = EntityState.Added;
                //var existingTags = repo.GetAll().ToList().Select(x => x.Tags).ToList();
                //var existingTagsStrings = existingTags.Except(flashCard.Tags);
                List<string> tagsInDb = new List<string>();
                using (var repoTag = new TagRepository())
                {
                    tagsInDb = repoTag.GetAll().Select(x => x.Tag1).ToList();
                }
                var pendingTags = new List<string>();
                pendingTags = flashCard.Tags.Select(x => x.Tag1).ToList();
                var notExistingTags = pendingTags.Where(x => !tagsInDb.Any());
                var tagsToBeAdded = new List<Tag>();
                foreach (var item in notExistingTags)
                {
                    tagsToBeAdded.Add(new Tag { Tag1 = item }); //dodac flashcard?
                }
                flashCard.Tags.ToList().ForEach(x => repo.Context.Tags.AddRange(tagsToBeAdded));
                //modyfikujemy istniejace
                var tagsToBeModified = new List<string>();
                tagsToBeModified = pendingTags.Where(x => !tagsToBeAdded.Any()).ToList();
                var tagsToModify = new List<Tag>();
                foreach (var item in tagsToBeModified)
                {
                    tagsToModify.Add(new Tag { Tag1 = item });  //dodac flashcard?
                }
                foreach (var item in flashCard.Tags)
                {
                    repo.Context.Entry(item).State = EntityState.Modified;
                }
                repo.Context.SaveChanges();
            }
        }
        //metoda dzialajaca w kazdej sytuacji
        [TestMethod]
        public void DeleteAttachedFlashCard()
        {
            using (var repo = new FlashCardRepository())
            {
                var flashCard = repo.Context.FlashCards.Find(26006);
                //flashCard.Tags.ToList().ForEach(x => repo.Context.Tags.Remove(x));
                //repo.Context.Tags.RemoveRange(flashCard.Tags);
                //repo.Context.Entry(flashCard).State = EntityState.Deleted;
                if(flashCard.FileAnserws.ToList().Count > 0)
                    flashCard.FileAnserws.ToList().ForEach(x => repo.Context.FileAnserws.Remove(x));
                if(flashCard.TextAnserws.ToList().Count > 0)
                    flashCard.TextAnserws.ToList().ForEach(x => repo.Context.TextAnserws.Remove(x));
                repo.Context.FlashCardDatas.Remove(flashCard.FlashCardData);
                //repo.Context.Tags.ToList().ForEach(x => repo.Context.Tags.Remove(x));
                repo.Context.FlashCards.Remove(flashCard);
                repo.Context.SaveChanges();
            }
        }
        [TestMethod]
        public void DeleteUnAttachedFlashCard()
        {

            var flashCard = GetFlashCard();
            using(var repo = new FlashCardRepository())
            {

            }
        }
        [TestMethod]
        public void Delete()
        {
            var flashCard = GetFlashCard();
            int id;
            using (var repo = new FlashCardRepository())
            {
                var tempFlashCard = repo.GetAll().Find(x => x.Title == flashCard.Title);
                repo.Context.FlashCards.Remove(tempFlashCard);
                var tempTags = tempFlashCard.Tags;
                foreach (var item in tempTags)
                {
                    repo.Context.Tags.Remove(item);
                }
                repo.Context.SaveChanges();
            }
        }
        [TestMethod]
        public void UpdateByDeleteAndAdd()
        {
            var flashCard = GetFlashCard();
            int id;
            using (var repo2 = new FlashCardRepository())
            {
                var tempFlashCard = repo2.GetAll().Find(x => x.Title == flashCard.Title);
                id = tempFlashCard.Id;
                //repo.Delete(tempFlashCard.Id);
                //repo.Context.FlashCards.Remove(tempFlashCard);
                //repo.Context.SaveChanges();

            using (var repo = new FileAnserwRepository())
            {
                var loop = repo.Context.FileAnserws.Where(n => n.FlashCard.Id == id).ToList();
                foreach (var item in loop)
                {
                    repo.Delete(item);
                }
                repo.Context.SaveChanges();
            }
            using (var repo = new TextAnserwRepository())
            {
                var loop = repo.Context.TextAnserws.Where(n => n.FlashCard.Id == id).ToList();
                foreach (var item in loop)
                {
                    repo.Delete(item);
                }
                repo.Context.SaveChanges();
            }
            using (var repo = new FlashCardDataRepository())
            {
                var loop = repo.Context.FlashCardDatas.Where(n => n.FlashCard.Id == id).ToList();
                foreach (var item in loop)
                {
                    repo.Delete(item);
                }
                repo.Context.SaveChanges();
            }
            List<Tag> temp1 = new List<Tag>();
            using (var repo = new FlashCardRepository())
            {
                //var flashCardsFromDb = repo.GetAll();
                //FlashCard flashCard2 = flashCardsFromDb.Find(n => n.Id == 14007); //new FlashCard { Id = 13006 };
                //var loop = repo.Context.FlashCards.Where(n => n.Id == 14007).ToList();
                //var loop2 = loop.
                var flashCardsFromDb = repo.GetAll();
                FlashCard flashCard2 = flashCardsFromDb.Find(n => n.Id == id);
                temp1 = flashCard2.Tags.ToList();
            }
            using (var repo = new TagRepository())
            {
                foreach (var item in temp1)
                {
                    try
                    {
                        repo.Delete(item);
                    }
                    catch (Exception)
                    {

                    }
                }
                //repo.Context.SaveChanges();
            }
                repo2.Context.FlashCards.Remove(tempFlashCard);
                repo2.Context.SaveChanges();
            }
        }
        [TestMethod]
        public void ModifyTextAnserw()
        {
            var flashCard = GetFlashCard();
            flashCard.TextAnserws.Add(new TextAnserw { Text = "ModifyTextAnserw" , Annotation = "a"});
            using (var repo = new FlashCardRepository())
            {
                //repo.Context.FlashCards.Attach(flashCard);
                //var tempFlashCard = repo.GetAll().Find(x => x.Title == flashCard.Title);
                //tempFlashCard = flashCard;
                //repo.Context.Entry(tempFlashCard).State = EntityState.Modified;
                //repo.Save(tempFlashCard);
                repo.Context.Entry(flashCard).State = EntityState.Modified;
                repo.Context.Entry(flashCard.TextAnserws).State = EntityState.Modified;
                repo.Context.SaveChanges();
            }
        }
        //    string serverName = "TestFlashCardsEntities";
        //    TextAnserw textAnserw1 = new TextAnserw { Annotation = "TestAnnotation", Text = "SampleText1" };
        //    TextAnserw textAnserw2 = new TextAnserw { Annotation = "TestAnnotation", Text = "SampleText2" };
        //    FileAnserw fileAnserw1 = new FileAnserw { FileName = "TestFile.jpg", File = new byte[] { 1, 2, 3 }, Annotation = "TestAnnotation" };
        //    FileAnserw fileAnserw2 = new FileAnserw { FileName = "TestFile.jpg", File = new byte[] { 1, 2, 3 }, Annotation = "TestAnnotation" };
        //    Tag tag1 = new Tag { Tag1 = "TestTag1" };
        //    Tag tag2 = new Tag { Tag1 = "TestTag2" };
        //    FlashCardData flashCardData = new FlashCardData { CreationDate = DateTime.Today };
        //    string flashCardTitle = "Test1";
        //    //[TestInitialize]
        //    //public void TestInit()
        //    //{
        //    //    using (FlashCardRepository flashCardRepository = new FlashCardRepository(serverName))
        //    //    {
        //    //        var flashCard = new FlashCard
        //    //        {
        //    //            Title = flashCardTitle,
        //    //            FileAnserws = new List<FileAnserw> { fileAnserw1, fileAnserw2 },
        //    //            TextAnserws = new List<TextAnserw> { textAnserw1, textAnserw2 },
        //    //            Tags = new List<Tag> { tag1, tag2 },
        //    //            FlashCardData = flashCardData
        //    //        };
        //    //        flashCardRepository.Add(flashCard);
        //    //    }
        //    //}

        //    //private void ClearRepo<T>(T repo) where T: IRepository, new()
        //    //{
        //    //    using (T tagRepo = (T)Activator.CreateInstance(typeof(T), serverName))
        //    //    {
        //    //        var allTags = tagRepo.GetAll();
        //    //        foreach (var tag in allTags)
        //    //        {
        //    //            tagRepo.Delete(tag);
        //    //        }
        //    //    }
        //    //}
        //    //private void ClearRepo<T,K>(BaseRepository<T,K> baseRepository)where K:DbContext,new() where T : class, new()
        //    //{
        //    //    //Context = (K)Activator.CreateInstance(typeof(K), contextName);
        //    //    using (T tagRepo = new T(serverName))
        //    //    {
        //    //        var allTags = tagRepo.GetAll();
        //    //        foreach (var tag in allTags)
        //    //        {
        //    //            tagRepo.Delete(tag);
        //    //        }
        //    //    }
        //    //}

        //    //[TestCleanup]
        //    //public void TestCleanUp()
        //    //{
        //    //    using (TagRepository tagRepo = new TagRepository(serverName))
        //    //    {
        //    //        var allTags = tagRepo.GetAll();
        //    //        foreach (var tag in allTags)
        //    //        {
        //    //            tagRepo.Delete(tag);
        //    //        }
        //    //    }
        //    //    using (TextAnserwRepository textAnserwRepo = new TextAnserwRepository(serverName))
        //    //    {
        //    //        var allTextAnserws = textAnserwRepo.GetAll();
        //    //        foreach (var text in allTextAnserws)
        //    //        {
        //    //            textAnserwRepo.Delete(text);
        //    //        }
        //    //    }
        //    //    using (FlashCardRepository flashCardRepository = new FlashCardRepository(serverName))
        //    //    {
        //    //        var allFlashCards = flashCardRepository.GetAll();
        //    //        foreach (var flashCard in allFlashCards)
        //    //        {
        //    //            flashCardRepository.Delete(flashCard);
        //    //        }
        //    //    }

        //    //}

        //    [TestMethod]
        //    public void TestFlashCardSetSaveToDB()
        //    {
        //        using (FlashCardRepository flashCardRepository = new FlashCardRepository(serverName))
        //        {
        //            FlashCard flashCard = new FlashCard()
        //            {
        //                Title = flashCardTitle,
        //                FileAnserws = new List<FileAnserw> { fileAnserw1, fileAnserw2 },
        //                TextAnserws = new List<TextAnserw> { textAnserw1, textAnserw2 },
        //                Tags = new List<Tag> { tag1, tag2 },
        //            };
        //            flashCardRepository.Context.FlashCards.Add(flashCard);
        //            var flashCardsFromDb = flashCardRepository.GetAll();
        //            var specyficFlashCard = flashCardsFromDb.Find(n => n.Title == flashCardTitle);
        //            Assert.AreEqual(specyficFlashCard.Title, flashCardTitle);
        //        }
        //    }

        //    [TestMethod]
        //    public void TestFlashCardDeleteFromDB()
        //    {
        //        using (FlashCardRepository flashCardRepository = new FlashCardRepository(serverName))
        //        {
        //            var flashCardsFromDb = flashCardRepository.GetAll();
        //            FlashCard flashCard = flashCardsFromDb.Find(n => n.Title == flashCardTitle);
        //            using (var textAnserwRepository = new TextAnserwRepository(serverName))
        //            {
        //                foreach (var item in flashCard.TextAnserws)
        //                {
        //                    textAnserwRepository.Delete(item);
        //                }
        //            }
        //            //using (var fileAnserwRepository = new FileAnserwRepository(serverName))
        //            //{
        //            //    foreach (var item in flashCard.FileAnserws)
        //            //    {
        //            //        fileAnserwRepository.Delete(item);
        //            //    }
        //            //}
        //            //using (var flashCardDataRepository = new FlashCardDataRepository(serverName))
        //            //{
        //            //    if (flashCard.FlashCardData != null)
        //            //        flashCardDataRepository.Delete(flashCard.FlashCardData);
        //            //}
        //            //using (var tagRepository = new TagRepository(serverName))
        //            //{
        //            //    foreach (var item in flashCard.Tags)
        //            //    {
        //            //        tagRepository.Delete(item);
        //            //    }
        //            //}
        //            Console.WriteLine(flashCard.Id);
        //            int loadedId = flashCard.Id;
        //            //flashCardRepository.Delete(flashCard);
        //            flashCardsFromDb = flashCardRepository.GetAll();
        //            var specyficFlashCard = flashCardsFromDb.Find(n => n.Id == loadedId);
        //            Assert.AreEqual(specyficFlashCard, null);
        //        }
        //    }
        //    [TestMethod]
        //    public void TestFlashCardDeleteFromDB2()
        //    {
        //        FlashCard flashCard = new FlashCard { Id = 1014 };
        //        using(var repo = new FlashCardRepository(serverName))
        //        {
        //            repo.Delete(flashCard);
        //        }
        //    }
    }
}
