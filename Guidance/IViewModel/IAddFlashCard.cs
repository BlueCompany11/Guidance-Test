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
        ObservableCollection<string> Tags { get; set; }
        void AddTextAnserw(string textAnserw, string annotation);
        void AddFile(string path);
        void AttachAnnotationToFile(string annotation);// dodanie annotation do pliku
        void SaveFlashCard();
    }
}
