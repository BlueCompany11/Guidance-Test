using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guidance.FlashCardModel
{
    public partial class FlashCard
    {
        public override string ToString()
        {
            return string.Format("Id: {0} \nTitle: {1}\nTags: {2}\nFiles anserws: {3}\nText anserws: {4}",
                Id, 
                Title, 
                Tags.Count,
                FileAnserws.Count,
                TextAnserws.Count
                );
        }
    }
}
