using Guidance.DataAccessLayer;
using Guidance.FlashCardModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guidance.FlashCardModel
{
    public class FlashCardPreviewFactory
    {
        //dodac opcjonalny arguemnt do filtrowania
        public List<FlashCardPreview> FlashCardPreviews()
        {
            var flashCardPreview = new List<FlashCardPreview>();
            using (var flashCardRepository = new FlashCardRepository())
            {
                var allFlashCards = flashCardRepository.GetAll();
                //usunac try 
                try
                {
                    for (int i = 0; i < allFlashCards.Count; i++)
                    {
                        //zabezpieczyc sie na wypadek braku danych
                        flashCardPreview.Add(new FlashCardPreview(allFlashCards[i].Id));
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
                        var flashCardMemorizer = new FlashCardMemorizer();
                        flashCardRepository.Context.Entry(allFlashCards[i]).Reference(x => x.FlashCardData).Load();
                        int recallVal = flashCardMemorizer.GetRecallValue(allFlashCards[i].FlashCardData);
                        flashCardPreview.Last().RecallVal = recallVal;
                    }
                }
                catch (Exception) { }
            }
            return flashCardPreview;
        }
    }
}
