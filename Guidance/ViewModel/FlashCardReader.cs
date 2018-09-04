using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guidance.DataAccessLayer;
using Guidance.IViewModel;
using Guidance.FlashCardModel;

namespace Guidance.ViewModel
{
    public class FlashCardReader:IFlashCardView
    {
        public List<FlashCardPreview> FlashCards { get; private set; } = new List<FlashCardPreview>();
        public FlashCardReader()
        {
            FillFlashCards();
            //Console.WriteLine(FlashCards[0].Tags[0].OfType<Tag>().ToList()[0].Tag1);
        }
        private void FillFlashCards()
        {
            var flashCardPreview = new List<FlashCardPreview>();
            using (FlashCardRepository flashCardRepository = new FlashCardRepository())
            {
                var allFlashCards = flashCardRepository.GetAll();
                for (int i = 0; i < allFlashCards.Count; i++)
                {
                    flashCardPreview.Add(new FlashCardPreview());
                    flashCardPreview.Last().Title = allFlashCards[i].Title;
                    flashCardPreview.Last().CreationDate = allFlashCards[i].FlashCardData.CreationDate;
                    if (allFlashCards[i].FlashCardData.LastOccurrence != null)
                    {
                        flashCardPreview.Last().LastOccurance = (DateTime)allFlashCards[i].FlashCardData.LastOccurrence;
                    }
                    else
                    {
                        flashCardPreview.Last().LastOccurance = default(DateTime);
                    }
                    var allTagsObj = allFlashCards[i].Tags.OfType<Tag>().ToList();
                    //var allTagsStrings = allTagsObj.Select(n => n.Tag1).ToList();
                    //if (allTagsStrings != null)
                    //{
                    //    flashCardPreview.Last().Tags = new List<string>();
                    //    flashCardPreview.Last().Tags.AddRange(allTagsStrings);
                    //}
                }
            }
            FlashCards = flashCardPreview;
        }

        public FlashCard FindFlashCard(string flashCardTitle)
        {
            using (FlashCardRepository flashCardRepository = new FlashCardRepository())
            {
                flashCardRepository.Context.Configuration.LazyLoadingEnabled = false;
                flashCardRepository.Context.FlashCards.Add(new FlashCard());
                var flashCardsFromDb = flashCardRepository.GetAll();
                FlashCard flashCard = flashCardsFromDb.Find(n => n.Title == flashCardTitle);
                //flashCardRepository.Context.Entry(flashCard).Collection(x => x.FileAnserws).Load();
                //flashCardRepository.Context.Entry(flashCard).Reference(x => x.FlashCardData).Load();
                //flashCardRepository.Context.Entry(flashCard).Collection(x => x.Tags).Load();
                //flashCardRepository.Context.Entry(flashCard).Collection(x => x.TextAnserws).Load();
                //flashCardRepository.Context.Entry(flashCard).Reference(x => x.Title).Load();
                return flashCard;
            }
        }

        public void DeleteFlashCard2()
        {
            using(FlashCardRepository flashCardRepository = new FlashCardRepository())
            {
                //var x = flashCardRepository.Context.Tags.Where(b=>EF.Property<int>())
            }
        }

        public void DeleteFlashCard(FlashCard flashCard)
        {
            //using (var flashCardRepository = new FlashCardRepository())
            //{
            //    var flashCardsFromDb = flashCardRepository.GetAll();
            //    FlashCard flashCard2 = flashCardsFromDb.Find(n => n.Id == 14007); //new FlashCard { Id = 13006 };
                
            //    //flashCardRepository.Delete(flashCard2);
            //}
            using (var repo = new FileAnserwRepository ())
            {
                var loop = repo.Context.FileAnserws.Where(n => n.FlashCard.Id == 13007).ToList();
                foreach (var item in loop)
                {
                    repo.Delete(item);
                }
                repo.SaveChanges();
            }
            using (var repo = new TextAnserwRepository())
            {
                var loop = repo.Context.TextAnserws.Where(n => n.FlashCard.Id == 13007).ToList();
                foreach (var item in loop)
                {
                    repo.Delete(item);
                }
                repo.SaveChanges();
            }
            using (var repo = new FlashCardDataRepository())
            {
                var loop = repo.Context.FlashCardDatas.Where(n => n.FlashCard.Id == 13007).ToList();
                foreach (var item in loop)
                {
                    repo.Delete(item);
                }
                repo.SaveChanges();
            }
            List<Tag> temp1 = new List<Tag>();
            using (var repo = new FlashCardRepository())
            {
                //var flashCardsFromDb = repo.GetAll();
                //FlashCard flashCard2 = flashCardsFromDb.Find(n => n.Id == 14007); //new FlashCard { Id = 13006 };
                //var loop = repo.Context.FlashCards.Where(n => n.Id == 14007).ToList();
                //var loop2 = loop.
                var flashCardsFromDb = repo.GetAll();
                FlashCard flashCard2 = flashCardsFromDb.Find(n => n.Id == 13007);
                temp1 = flashCard2.Tags.ToList();
            }
            using (var repo = new TagRepository())
            {
                foreach (var item in temp1)
                {
                    repo.Delete(item);
                }
                repo.SaveChanges();
            }
            using(var repo = new FlashCardRepository())
            {
                var flashCardsFromDb = repo.GetAll();
                FlashCard flashCard2 = flashCardsFromDb.Find(n => n.Id == 13007);
                repo.Delete(flashCard2);
            }
            //using (FlashCardRepository flashCardRepository = new FlashCardRepository())
            //{
            //flashCardRepository.Context.Configuration.LazyLoadingEnabled = false;
            //flashCardRepository.Context.FlashCards.Add(new FlashCard());
            //var flashCardsFromDb = flashCardRepository.GetAll();
            //FlashCard flashCard = flashCardsFromDb.Where(x => (string)x.Title == "asd").Select(p => p).First();
            //flashCardRepository.Context.Entry(flashCard).Collection(x => x.FileAnserws).Load();
            //flashCardRepository.Context.Entry(flashCard).Reference(x => x.FlashCardData).Load();
            //flashCardRepository.Context.Entry(flashCard).Collection(x => x.Tags).Load();
            //flashCardRepository.Context.Entry(flashCard).Collection(x => x.TextAnserws).Load();
            //flashCardRepository.Context.Entry(flashCard).Reference(x => x.Title).Load();
            //using (var textAnserwRepository = new TextAnserwRepository())
            //{
            //    foreach (var item in flashCard.TextAnserws)
            //    {
            //        textAnserwRepository.Delete(item);
            //        textAnserwRepository.SaveChanges();
            //    }
            //}
            //using (var fileAnserwRepository = new FileAnserwRepository())
            //{
            //    foreach (var item in flashCard.FileAnserws)
            //    {
            //        fileAnserwRepository.Delete(item);
            //        fileAnserwRepository.SaveChanges();
            //    }
            //}
            //using (var flashCardDataRepository = new FlashCardDataRepository())
            //{
            //    if (flashCard.FlashCardData != null)
            //        flashCardDataRepository.Delete(flashCard.FlashCardData);
            //    flashCardDataRepository.SaveChanges();
            //}
            //using (var tagRepository = new TagRepository())
            //{
            //    foreach (var item in flashCard.Tags)
            //    {
            //        tagRepository.Delete(item);
            //    }
            //}
            //flashCardRepository.Delete(flashCard);
            //}
        }
    }
}
