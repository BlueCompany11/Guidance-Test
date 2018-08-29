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
            using (FlashCardRepository flashCardRepository = new FlashCardRepository()) { 
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
                    var allTagsStrings = allTagsObj.Select(n => n.Tag1).ToList();
                    if (allTagsStrings != null)
                    {
                        flashCardPreview.Last().Tags = new List<string>();
                        flashCardPreview.Last().Tags.AddRange(allTagsStrings);
                    }
                }
            }
            FlashCards = flashCardPreview;
        }

        public FlashCard FindFlashCard(string flashCardTitle)
        {
            using (FlashCardRepository flashCardRepository = new FlashCardRepository())
            {
                flashCardRepository.Context.FlashCards.Add(new FlashCard());
                var flashCardsFromDb = flashCardRepository.GetAll();
                return flashCardsFromDb.Find(n => n.Title == flashCardTitle);
            }
        }

        public void DeleteFlashCard(FlashCard flashCard)
        {
            using (FlashCardRepository flashCardRepository = new FlashCardRepository())
            {
                var allFlashCards = flashCardRepository.GetAll();
                flashCardRepository.Delete(flashCard);
            }
        }
    }
}
