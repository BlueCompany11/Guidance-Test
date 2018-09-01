using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guidance.FlashCardModel;
namespace Guidance.DataAccessLayer
{
    public class FileAnserwRepository: BaseRepository<FileAnserw, FlashCardsEntities>, IRepository<FileAnserw>
    {
        public FileAnserwRepository(string contextName = "FlashCardsEntities") :base(contextName)
        {   
            Table = Context.FileAnserws;
        }
        public int Delete(int id)
        {
            Context.Entry(new FileAnserw() { Id = id }).State = EntityState.Deleted;
            return SaveChanges();
        }
        public Task<int> DeleteAsync(int id)
        {
            Context.Entry(new FileAnserw() { Id = id }).State = EntityState.Deleted;
            return SaveChangesAsync();
        }

    }
}
