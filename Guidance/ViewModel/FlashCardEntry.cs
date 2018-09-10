using Guidance.DataAccessLayer;
using Guidance.FlashCardModel;
using Guidance.GUI;
using Guidance.IView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Guidance.ViewModel
{
    public class FlashCardEntry : IFlashCardEntryPage, INotifyPropertyChanged
    {
        public FlashCardEntry()
        {
            isFlashCardSelected = false;
            LoadFlashCards();
        }
        private void LoadFlashCards()
        {
            var flashCardPreview = new List<FlashCardPreview>();
            using (var flashCardRepository = new FlashCardRepository())
            {
                var allFlashCards = flashCardRepository.GetAll();
                for (int i = 0; i < allFlashCards.Count; i++)
                {
                    //zabezpieczyc sie na wypadek braku danych
                    flashCardPreview.Add(new FlashCardPreview(allFlashCards[i].Id));
                    flashCardPreview.Last().Title = allFlashCards[i].Title;
                    flashCardPreview.Last().CreationDate = allFlashCards[i].FlashCardData.CreationDate;
                    if (allFlashCards[i].FlashCardData.LastOccurrence != null)
                    {
                        flashCardPreview.Last().LastOccurance = (DateTime)allFlashCards[i].FlashCardData.LastOccurrence;
                    }
                    else
                    {
                        flashCardPreview.Last().LastOccurance = default(DateTime);
                    }
                }
            }
            FlashCardPreviews = new ObservableCollection<IFlashCardPreview>(flashCardPreview);
            OnPropertyChanged(nameof(FlashCardPreviews));
        }
        public ObservableCollection<IFlashCardPreview> FlashCardPreviews { get; set; }

        private IFlashCardPreview selectedFlashCard;
        public IFlashCardPreview SelectedFlashCard
        {
            get
            {
                return selectedFlashCard;
            }
            set
            {
                selectedFlashCard = value;
                OnPropertyChanged("SelectedFlashCard");
                isFlashCardSelected = true;
                deleteSelectedFlashCard.CanExecute(isFlashCardSelected);
                editSelectedFlashCard.CanExecute(isFlashCardSelected);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        private ICommand addFlashCardCommand;
        public ICommand AddFlashCardCommand
        {
            get
            {
                return addFlashCardCommand ?? (addFlashCardCommand = new CommandHandler(AddFlashCard, true));
            }
        }
        public void AddFlashCard()
        {
            var addFlashCardWindow = new FlashCardDetailsWindow(new FlashCardDetails());
            addFlashCardWindow.ShowDialog();
            //dodac do okna wlasciwosc czy zmiany maja zostac zapisane i zmienic sposob definiowania wylaczania ekarnu
            if (!string.IsNullOrEmpty(addFlashCardWindow.addFlashCard.Title) && addFlashCardWindow.addFlashCard.Save)
            {
                addFlashCardWindow.addFlashCard.ReturnedFlashCard.FlashCardData = new FlashCardData();
                using (var repo = new FlashCardRepository())
                {
                    repo.Add(addFlashCardWindow.addFlashCard.ReturnedFlashCard);
                }
                LoadFlashCards();
            }
        }
        private ICommand deleteSelectedFlashCard;
        private bool isFlashCardSelected;
        public ICommand DeleteSelectedFlashCardCommand
        {
            get
            {
                return deleteSelectedFlashCard ?? (deleteSelectedFlashCard = new CommandHandler(DeleteSelectedFlashCard, isFlashCardSelected));
            }
        }
        public void DeleteSelectedFlashCard()
        {
            using(var repo = new FlashCardRepository())
            {
                repo.Delete(selectedFlashCard.Id);
            }
            SelectedFlashCard = null;
            LoadFlashCards();
        }
        private ICommand editSelectedFlashCard;
        public ICommand EditSelectedFlashCardCommand
        {
            get
            {
                return editSelectedFlashCard ?? (editSelectedFlashCard = new CommandHandler(EditSelectedFlashCard, isFlashCardSelected));
            }
        }
        public void EditSelectedFlashCard()
        {
            var flashCard = new FlashCard();
            using(var repo = new FlashCardRepository())
            {
                flashCard = repo.GetOne(selectedFlashCard.Id);
                repo.Context.Entry(flashCard).Collection(x => x.Tags).Load();
                repo.Context.Entry(flashCard).Collection(x => x.TextAnserws).Load();
                repo.Context.Entry(flashCard).Collection(x => x.FileAnserws).Load();
                var addFlashCardWindow = new FlashCardDetailsWindow(new FlashCardDetails(flashCard));
                addFlashCardWindow.ShowDialog();
                Console.WriteLine(addFlashCardWindow.addFlashCard.Save);
                if (addFlashCardWindow.addFlashCard.Save)
                {
                    repo.Save(addFlashCardWindow.addFlashCard.ReturnedFlashCard);
                }
            }
            LoadFlashCards();
        }
    }
}
