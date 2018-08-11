using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guidance.DataAccessLayer;
using Guidance.IViewModel;
using PropertyChanged;

namespace Guidance.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class FlashCardReader:IFlashCardView
    {
        public ObservableCollection<FlashCardPreview> FlashCards { get; private set; } = new ObservableCollection<FlashCardPreview>();
        public FlashCardReader()
        {
            FillFlashCards();
        }
        private void FillFlashCards()
        {
            //FlashCardRepository flashCardRepository = new FlashCardRepository();
            //var allFlashCards = flashCardRepository.GetAll();
            //foreach (var flashCard in allFlashCards)
            //{
            //    FlashCards.Add(new FlashCardPreview(flashCard.Title, flashCard.Tags));
            //}
            throw new NotImplementedException();
        }
    }
}
