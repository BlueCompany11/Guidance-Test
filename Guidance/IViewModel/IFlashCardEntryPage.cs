using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Guidance.FlashCardModel;
using System.Windows.Input;

namespace Guidance.IViewModel
{
    public interface IFlashCardEntryPage
    {
        ObservableCollection<IFlashCardPreview> FlashCardPreviews { get; set; }
        IFlashCardPreview SelectedFlashCard { get; set; }
        ICommand AddFlashCardCommand { get; }
        ICommand EditSelectedFlashCardCommand { get; }
        ICommand DeleteSelectedFlashCardCommand { get; }
    }
}
