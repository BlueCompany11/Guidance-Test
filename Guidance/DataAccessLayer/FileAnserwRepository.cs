using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guidance.FlashCardModel;
namespace Guidance.DataAccessLayer
{
    public class FileAnserwRepository: BaseRepository<FileAnserw>, IRepository<FileAnserw>
    {
        public FileAnserwRepository()
        {
            Table = Context.FileAnserws;
        }

    }
}
