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
        FlashCard FlashCard { get; set; }
        List<TextAnserw> TextAnserws { get; set; }
        List<FileAnserw> FileAnserws { get; set; }
        void AddFileAnserw(string fileName, byte[] file, string annotation = null);
        void AddFlashCard(string title, List<string> tags);
        void AddTextAnserw(string text, string annotation = null);
        string Title { get; set; }
        List<string> Tags { get; set; }
        


    }
}
