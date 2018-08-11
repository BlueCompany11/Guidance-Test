using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guidance.FlashCardModel;

namespace Guidance.DataAccessLayer
{
    public class TagRepository : BaseRepository<Tag, FlashCardsEntities>, IRepository<Tag>
    {
        public TagRepository(string contextName = "FlashCardsEntities") : base(contextName)
        {
            Table = Context.Tags;
        }
    }
}
