using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Guidance.FlashCardModel;
namespace Guidance.DataAccessLayer
{
    public class FlashCardRepository: BaseRepository<FlashCard> , IRepository<FlashCard>
    {
        public FlashCardRepository()
        {
            Table = Context.FlashCards;
        }

        public int Delete(int id)
        {
            Context.Entry(new FlashCard() { Id = id }).State = EntityState.Deleted;
            return SaveChanges();
        }
        public Task<int> DeleteAsync(int id)
        {
            Context.Entry(new FlashCard() { Id = id }).State = EntityState.Deleted;
            return SaveChangesAsync();
        }
        
    }
}
