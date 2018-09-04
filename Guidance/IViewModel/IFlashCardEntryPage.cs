using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Guidance.FlashCardModel;
namespace Guidance.IViewModel
{
    public interface IFlashCardEntryPage
    {
        ObservableCollection<IFlashCardPreview> FlashCardPreviews { get; set; }
        IFlashCardPreview SelectedFlashCard { get; set; }
        void AddFlashCard();
        void EditSelectedFlashCard();
        void DeleteSelectedFlashCard();
    }
}
