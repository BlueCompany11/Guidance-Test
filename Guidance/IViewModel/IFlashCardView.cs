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
        ObservableCollection<FlashCardPreview> FlashCards { get;}
    }
}
