using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guidance.FlashCardModel;
namespace Guidance.DataAccessLayer
{
    public class TextAnserwRepository: BaseRepository<TextAnserw, FlashCardsEntities>, IRepository<TextAnserw>
    {
        public TextAnserwRepository(string contextName = "FlashCardsEntities") : base(contextName)
        {
            Table = Context.TextAnserws;
        }
    }
}
