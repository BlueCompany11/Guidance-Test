using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guidance.FlashCardModel;
namespace Guidance.DataAccessLayer
{
    public class FlashCardDataRepository: BaseRepository<FlashCardData>, IRepository<FlashCardData>
    {
        public FlashCardDataRepository()
        {
            Table = Context.FlashCardDatas;
        }
    }
}
