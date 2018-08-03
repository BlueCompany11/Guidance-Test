using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guidance.DataAccessLayer;
using Guidance.FlashCardDb; //to trzeba wywalic
namespace Guidance.ViewModel
{
    public class FlashCardVM
    {
        public string Title { get; set; }
        public List<string> Tags { get; set; }
        public List<byte[]> Files { get; set; }
        public List<byte[]> Pictures { get; set; }
        public List<string> TextAnserws { get; set; }
        private string tags;
        private Lazy<IQueryable<FlashCard>> flashCards = new Lazy<IQueryable<FlashCard>>(() => GetFlashCardsData());

        public IQueryable<FlashCard> FlashCards
        {
            get
            {
                return flashCards.Value;
            }
        }

        private static IQueryable<FlashCard> GetFlashCardsData()
        {
            using (var db = new FlashCardsEntities())
            {
                var query = from b in db.FlashCards
                            select b;
                return query;
            }
        }

        public FlashCardVM()
        {
            Tags = new List<string>();
            Files = new List<byte[]>();
            Pictures = new List<byte[]>();
            TextAnserws = new List<string>();
        }

        private void ConvertTags()
        {
            if (tags == null)
                tags = "";
            for (int i = 0; i < Tags.Count; i++)
            {
                Tags[i] += "#";
                tags += Tags[i];
            }
        }

        public void SaveToDB()
        {
            using (var db = new FlashCardsEntities())
            {
                ConvertTags();
                var flashCard = new FlashCard { Title = Title, Tags = tags };
                db.FlashCards.Add(flashCard);
                foreach (var picture in Pictures)
                {
                    var pictureAnserw = new PictureAnserw { FlashCard = flashCard, Picture = picture };
                    db.PictureAnserws.Add(pictureAnserw);
                }
                //foreach (var file in Files)
                //{
                //    var fileAnserw = new FileAnserw { FlashCard = flashCard, File = file };
                //    db.FileAnserws.Add(fileAnserw);
                //}
                foreach (var text in TextAnserws)
                {
                    var textAnserw = new TextAnserw { FlashCard = flashCard, Text = text };
                    db.TextAnserws.Add(textAnserw);
                }
                var flashCardData = new FlashCardData {FlashCard = flashCard, CreationDate = DateTime.Today };
                db.FlashCardDatas.Add(flashCardData);
                db.SaveChanges();
            }
        }
    }
}
