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
            UpdateFlashCardPreview();
        }
        private void UpdateFlashCardPreview()
        {
            FlashCardPreviewFactory flashCardPreviewFactory = new FlashCardPreviewFactory();
            FlashCardPreviews = new ObservableCollection<IFlashCardPreview>(flashCardPreviewFactory.FlashCardPreviews());
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
        void AddFlashCard()
        {   
            FlashCardTagFactory flashCardTagFactory = new FlashCardTagFactory();
            var addFlashCardWindow = new FlashCardDetailsWindow(new FlashCardDetails(new FlashCard(), flashCardTagFactory.GetAllTags()));
            addFlashCardWindow.ShowDialog();
            //if (!string.IsNullOrEmpty(addFlashCardWindow.addFlashCard.Title) && addFlashCardWindow.addFlashCard.Save)
            //{
            //    addFlashCardWindow.addFlashCard.ReturnedFlashCard.FlashCardData = new FlashCardData();
            //    using (var repo = new FlashCardRepository())
            //    {
            //        repo.Add(addFlashCardWindow.addFlashCard.ReturnedFlashCard);
            //    }
            //    UpdateFlashCardPreview();
            //}
            UpdateFlashCardPreview();
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
        void DeleteSelectedFlashCard()
        {
            using(var repo = new FlashCardRepository())
            {
                repo.Delete(selectedFlashCard.Id);
            }
            SelectedFlashCard = null;
            UpdateFlashCardPreview();
        }
        private ICommand editSelectedFlashCard;
        public ICommand EditSelectedFlashCardCommand
        {
            get
            {
                return editSelectedFlashCard ?? (editSelectedFlashCard = new CommandHandler(EditSelectedFlashCard, isFlashCardSelected));
            }
        }

        void EditSelectedFlashCard()
        {
            using (var repo = new FlashCardRepository())
            {
                FlashCardFactory flashCardFactory = new FlashCardFactory();
            FlashCardTagFactory flashCardTagFactory = new FlashCardTagFactory();
            var addFlashCardWindow = new FlashCardDetailsWindow(
                new FlashCardDetails(flashCardFactory.GetFlashCard(selectedFlashCard.Id), flashCardTagFactory.GetAllTags())
                );
            addFlashCardWindow.ShowDialog();

                if (addFlashCardWindow.addFlashCard.Save)
                {
                    repo.Save(addFlashCardWindow.addFlashCard.ReturnedFlashCard);
                }
            }
            UpdateFlashCardPreview();
        }
        ICommand startFlashCardRecall;
        bool canStartFlashCardRecall;
        public ICommand StartFlashCardRecall { get { return startFlashCardRecall ?? (startFlashCardRecall = new CommandHandler(StartFlashCardRecallCommand, canStartFlashCardRecall)); } }
        void StartFlashCardRecallCommand()
        {
            //zablokowac mozliwosc zmiany danych
            //2 przyciski na dole - powtorzone poprawnie lub negatywnie
            var flashCard = new FlashCard();
            List<string> tags = new List<string>();
            using (var repo = new TagRepository())
            {
                tags = repo.GetAll().Select(x => x.Tag1).Distinct().ToList();
            }
            using (var repo = new FlashCardRepository())
            {
                flashCard = repo.GetOne(selectedFlashCard.Id);
                repo.Context.Entry(flashCard).Collection(x => x.Tags).Load();
                repo.Context.Entry(flashCard).Collection(x => x.TextAnserws).Load();
                repo.Context.Entry(flashCard).Collection(x => x.FileAnserws).Load();
                var addFlashCardWindow = new FlashCardDetailsWindow(new FlashCardDetails(flashCard, tags));
                addFlashCardWindow.ShowDialog();
                
            }
        }
    }
}
