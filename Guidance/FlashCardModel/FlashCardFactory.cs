using Guidance.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guidance.FlashCardModel
{
    public class FlashCardFactory
    {
        public FlashCard GetFlashCard(int id)
        {
            var flashCard = new FlashCard();
            using (var repo = new FlashCardRepository())
            {
                flashCard = repo.GetOne(id);
                repo.Context.Entry(flashCard).Collection(x => x.Tags).Load();
                repo.Context.Entry(flashCard).Collection(x => x.TextAnserws).Load();
                repo.Context.Entry(flashCard).Collection(x => x.FileAnserws).Load();
                repo.Context.Entry(flashCard).Reference(x => x.FlashCardData).Load();
            }
            return flashCard;
        }
        public FlashCard GetFlashCard()
        {
            return new FlashCard();
        }
        public bool AddFlashCard(FlashCard flashCard)
        {
            bool success = false;
            using(var repo = new FlashCardRepository())
            {
                repo.Add(flashCard);
                success = true;
            }
            return success;
        }
    }
}
