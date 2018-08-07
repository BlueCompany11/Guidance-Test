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
        public string Title { get; private set; }

        public string Tag { get; private set; }
        public FlashCardPreview(string title, string tag)
        {
            Title = title;
            Tag = tag;
        }

    }
}
