using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Guidance.DataAccessLayer;
using Guidance.FlashCardDb; //do usuniecia
namespace Guidance.ViewModel
{
    
    public class FlashCardReader
    {
        //FlashCardDAL flashCardDAL;
        //public List<FlashCardPreview> FlashCards { get; private set; }
        public ObservableCollection<FlashCardPreview> FlashCards { get; private set; }
        public FlashCardReader()
        {
            FlashCards = new ObservableCollection<FlashCardPreview>();
            //flashCardDAL = new FlashCardDAL();
            using (var db = new FlashCardsEntities())
            {
                var query = from b in db.FlashCards
                            select b;
                foreach (var item in query)
                {
                    FlashCards.Add(new FlashCardPreview(item.Title, item.Tags));
                }
            }
        }
    }
}
