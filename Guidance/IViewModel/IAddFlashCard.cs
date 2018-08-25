using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guidance.FlashCardModel;

namespace Guidance.IViewModel
{
    public interface IAddFlashCard
    {
        string Title { get; set; }
        List<string> Tags { get; set; }
        string CurrentTag { get; set; }
        string CurrentTextAnserw { get; set; }
        string TextAnserwAnnotation { get; set; }
        void AddTextAnserw();
        string FileAnnotation { get; set; }
        void AddFile();
        void AddSetOfFiles();
        FlashCard Save();
        void AddTag();
    }
}
