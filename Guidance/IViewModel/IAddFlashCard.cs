using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Guidance.FlashCardModel;

namespace Guidance.IViewModel
{
    public interface IAddFlashCard
    {
        string Title { get; set; }
        ObservableCollection<string> Tags { get; set; }
        string NewTag { get; set; }
        ICommand AddTag { get; }
        ObservableCollection<string> FilesNames { get; }
        ICommand SaveFlashCard { get; }
        string TextAnserw { get; set; }
        string TextAnnotation { get; set; }
        ICommand SaveTextAnserw { get; }
        string FileAnnotation { get; set; }
        ICommand AddFile { get; }
        ICommand SaveFileAnserw { get; }
        ICommand DeleteTag { get; }
        string SelectedTag { get; set; }
        ObservableCollection<string> TextAnserws { get; }
        FlashCard ReturnedFlashCard { get; }
        ICommand MaterializeFlashCardAnserws { get; }
    }
}
