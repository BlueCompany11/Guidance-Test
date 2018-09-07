using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Guidance.FlashCardModel;
namespace Guidance.DataAccessLayer
{
    public class FlashCardRepository: BaseRepository<FlashCard, FlashCardsEntities> , IRepository<FlashCard>
    {
        public FlashCardRepository(string contextName = "FlashCardsEntities"):base(contextName)
        {
            Table = Context.FlashCards;
        }

        public int Delete(int id)
        {
            //zamienic to w transakcje i przeslonic metode przyjmujaca FlashCard
            ManualCascade(id);
            Context.Entry(new FlashCard() { Id = id }).State = EntityState.Deleted;
            return SaveChanges();
        }
        public Task<int> DeleteAsync(int id)
        {
            Context.Entry(new FlashCard() { Id = id }).State = EntityState.Deleted;
            return SaveChangesAsync();
        }
        private void ManualCascade(int id)
        {
            using (var repo = new FileAnserwRepository())
            {
                var loop = repo.Context.FileAnserws.Where(n => n.FlashCard.Id == id).ToList();
                foreach (var item in loop)
                {
                    repo.Delete(item);
                }
                repo.SaveChanges();
            }
            using (var repo = new TextAnserwRepository())
            {
                var loop = repo.Context.TextAnserws.Where(n => n.FlashCard.Id == id).ToList();
                foreach (var item in loop)
                {
                    repo.Delete(item);
                }
                repo.SaveChanges();
            }
            using (var repo = new FlashCardDataRepository())
            {
                var loop = repo.Context.FlashCardDatas.Where(n => n.FlashCard.Id == id).ToList();
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
                FlashCard flashCard2 = flashCardsFromDb.Find(n => n.Id == id);
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
            //using (var repo = new FlashCardRepository())
            //{
            //    var flashCardsFromDb = repo.GetAll();
            //    FlashCard flashCard2 = flashCardsFromDb.Find(n => n.Id == id);
            //    repo.Delete(flashCard2);
            //}
        }
    }
}
