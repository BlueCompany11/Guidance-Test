using Guidance.FlashCardModel;
using Guidance.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guidance.IViewModel
{
    public interface IFlashCardView
    {
        List<FlashCardPreview> FlashCards { get;}
        FlashCard FindFlashCard(string flashCardName);
        void DeleteFlashCard(FlashCard flashCard);
    }
}
