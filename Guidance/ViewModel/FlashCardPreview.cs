using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guidance.IViewModel;

namespace Guidance.ViewModel
{
    public class FlashCardPreview:IFlashCardPreview
    {
        public string Title { get; set; }

        public DateTime CreationDate { get; set; }

        public List<string> Tags { get; set; }

        public DateTime LastOccurance { get; set; }

    }
}
