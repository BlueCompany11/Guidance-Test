using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guidance.ViewModel
{
    public class FlashCardVM
    {
        public string Title { get; set; }
        public string Tags { get; set; }
        public int Id { get; set; }
        List<FileAnserw> FileAnserws { get; set; }
        List<PictureAnserw> PictureAnserws { get; set; }
        List<TextAnserw> TextAnserws { get; set; }

    }
}
