using Guidance.DataAccessLayer;
using Guidance.IViewModel;
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
            selectedFlashCard = new FlashCardPreview();
            canDeleteSelectedFlashCard = false;
            var flashCardPreview = new List<FlashCardPreview>();
            using (var flashCardRepository = new FlashCardRepository())
            {
                var allFlashCards = flashCardRepository.GetAll();
                for (int i = 0; i < allFlashCards.Count; i++)
                {
                    //zabezpieczyc sie na wypadek braku danych
                    flashCardPreview.Add(new FlashCardPreview());
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
                    //var allTagsObj = allFlashCards[i].Tags.OfType<Tag>().ToList();
                    //var allTagsStrings = allTagsObj.Select(n => n.Tag1).ToList();
                    //if (allTagsStrings != null)
                    //{
                    //    flashCardPreview.Last().Tags = new List<string>();
                    //    flashCardPreview.Last().Tags.AddRange(allTagsStrings);
                    //}
                }
            }
            FlashCardPreviews = new ObservableCollection<IFlashCardPreview>(flashCardPreview);
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
                canDeleteSelectedFlashCard = true;
                deleteSelectedFlashCard.CanExecute(true);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public void AddFlashCard()
        {
            throw new NotImplementedException();
        }
        private ICommand deleteSelectedFlashCard;
        private bool canDeleteSelectedFlashCard;
        public ICommand DeleteSelectedFlashCardCommand
        {
            get
            {
                return deleteSelectedFlashCard ?? (deleteSelectedFlashCard = new CommandHandler(DeleteSelectedFlashCard, canDeleteSelectedFlashCard));
            }
        }
        public void DeleteSelectedFlashCard()
        {
            //using (var repo = new FileAnserwRepository())
            //{
            //    var loop = repo.Context.FileAnserws.Where(n => n.FlashCard.Id == 13007).ToList();
            //    foreach (var item in loop)
            //    {
            //        repo.Delete(item);
            //    }
            //    repo.SaveChanges();
            //}
            //using (var repo = new TextAnserwRepository())
            //{
            //    var loop = repo.Context.TextAnserws.Where(n => n.FlashCard.Id == 13007).ToList();
            //    foreach (var item in loop)
            //    {
            //        repo.Delete(item);
            //    }
            //    repo.SaveChanges();
            //}
            //using (var repo = new FlashCardDataRepository())
            //{
            //    var loop = repo.Context.FlashCardDatas.Where(n => n.FlashCard.Id == 13007).ToList();
            //    foreach (var item in loop)
            //    {
            //        repo.Delete(item);
            //    }
            //    repo.SaveChanges();
            //}
            //List<Tag> temp1 = new List<Tag>();
            //using (var repo = new FlashCardRepository())
            //{
            //    //var flashCardsFromDb = repo.GetAll();
            //    //FlashCard flashCard2 = flashCardsFromDb.Find(n => n.Id == 14007); //new FlashCard { Id = 13006 };
            //    //var loop = repo.Context.FlashCards.Where(n => n.Id == 14007).ToList();
            //    //var loop2 = loop.
            //    var flashCardsFromDb = repo.GetAll();
            //    FlashCard flashCard2 = flashCardsFromDb.Find(n => n.Id == 13007);
            //    temp1 = flashCard2.Tags.ToList();
            //}
            //using (var repo = new TagRepository())
            //{
            //    foreach (var item in temp1)
            //    {
            //        repo.Delete(item);
            //    }
            //    repo.SaveChanges();
            //}
            //using (var repo = new FlashCardRepository())
            //{
            //    var flashCardsFromDb = repo.GetAll();
            //    FlashCard flashCard2 = flashCardsFromDb.Find(n => n.Id == 13007);
            //    repo.Delete(flashCard2);
            //}
            Console.WriteLine(SelectedFlashCard.Title);
        }

        public void EditSelectedFlashCard()
        {
            throw new NotImplementedException();
        }
    }
}
