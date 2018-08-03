using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guidance.FlashCardDb;

namespace Guidance.DataAccessLayer
{
    public class FlashCardDAL
    {
        //private Lazy<IQueryable<FlashCard>> flashCards = new Lazy<IQueryable<FlashCard>>(() => GetFlashCardsData());
        //private static IQueryable<FlashCard> GetFlashCardsData()
        //{
        //    using (var db = new FlashCardsEntities())
        //    {
        //        var query = from b in db.FlashCards
        //                    select b;
        //        return query;
        //    }
        //}
        public IQueryable<FlashCard> FlashCards
        {
            //get
            //{
            //    return flashCards.Value;
            //}
            get;
            private set;
        }
        FlashCardsEntities flashCardsEntities;
        public FlashCardDAL()
        {
            flashCardsEntities = new FlashCardsEntities();
                var query = from b in flashCardsEntities.FlashCards
                            select b;
                FlashCards = query;
        }
    }
}
