using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guidance.FlashCardModel
{
    public partial class FlashCardData
    {
        public FlashCardData()
        {
            CreationDate = DateTime.Now.Date;
            LastOccurrence = DateTime.Now.Date;
            SuccessfulAnserws = 0;
            LastResult = false;
        }
    }
}
