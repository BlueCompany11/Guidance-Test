using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guidance.FlashCardModel;
namespace Guidance.DataAccessLayer
{
    public class TextAnserwRepository: BaseRepository<TextAnserw>, IRepository<TextAnserw>
    {
        public TextAnserwRepository()
        {
            Table = Context.TextAnserws;
        }
    }
}
