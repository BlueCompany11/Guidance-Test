using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guidance.IViewModel
{
    public interface IAddFlashCard
    {
        string Title { get; set; }
        List<string> Tags { get; set; }
        string TextAnserw { get; set; }
        List<byte[]> PictureAnserws { get; set; }
        void AddTag(string input);
        void AddPicture();
        void Save();
    }
}
