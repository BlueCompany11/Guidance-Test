using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guidance.DataAccessLayer;
using Guidance.IViewModel;
using Guidance.FlashCardModel;
using System.IO;
using System.ComponentModel;

namespace Guidance.ViewModel
{
    public class FlashCardAdd : IAddFlashCard, INotifyPropertyChanged
    {
        public FlashCardAdd()
        {
            this.flashCard = new FlashCard();
        }
        public FlashCardAdd(FlashCard flashCard):this()
        {
            this.flashCard = flashCard;
        }
        FlashCard flashCard;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public string Title
        {
            get => flashCard.Title;
            set
            {
                flashCard.Title = value;
                OnPropertyChanged("Title");
            }
        }
        public ObservableCollection<string> Tags
        {
            get => new ObservableCollection<string>(flashCard.Tags.Select(x => x.Tag1).ToList());
            set
            {
                flashCard.Tags = value.Select(tag => new Tag() { Tag1 = tag }).ToList();
            }
        }

        public ObservableCollection<string> FilesNames
        {
            get => new ObservableCollection<string>(flashCard.FileAnserws.Select(x => x.FileName).ToList());
            //set
            //{
                //otrzymuje liste nazw plikow - patrze ktorego nie ma i kasuje z flashCard
                //znajduje brakujace elementy
                //var fileNamesToDelete = flashCard.FileAnserws.Select(x => x.FileName).Except(value).ToList();
                //usuwam je z listy flash card i zatwierdzam nowa liste
                //flashCard.FileAnserws = flashCard.FileAnserws.Where(x => !fileNamesToDelete.Any(s => s.Contains(x.FileName))).ToList();

            //}
        }
        //public ObservableCollection<string> TextAnserws { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ObservableCollection<string> TextAnserws { get; set; }
        public void AddTextAnserw(string textAnserw, string textAnserwAnnotation)
        {
            flashCard.TextAnserws.Add(new TextAnserw { Text = textAnserw, Annotation = textAnserwAnnotation });
        }

        public void AttachAnnotationToFile(string annotation)
        {
            try
            {
                var currentFileAnserw = flashCard.FileAnserws.Last();
                currentFileAnserw.Annotation = annotation;
                //nowe file zostlao zatwierdzone
                OnPropertyChanged("FilesNames");
            }
            catch (Exception) { };  //if empty do nothing
        }

        public void AddFile(string filePath)
        {
            var file = File.ReadAllBytes(filePath);
            var fileName = Path.GetFileName(filePath);
            if(flashCard.FileAnserws.Any(n=>n.FileName == fileName))
            {
                return; // do nothing cuz this file is already there
            }
            flashCard.FileAnserws.Add(new FileAnserw { File = file, FileName = fileName });
        }

        public void Save()
        {
            using (FlashCardRepository flashCardRepository = new FlashCardRepository())
            {
                flashCard.FlashCardData = new FlashCardData();
                flashCardRepository.Add(flashCard);
            }
        }
        public void PrintFlashCard()
        {
            Console.WriteLine(flashCard);
        }

        public void DeleteFile(string fileName)
        {
            flashCard.FileAnserws = flashCard.FileAnserws.Where(x => x.FileName != fileName).ToList();
            OnPropertyChanged("FilesNames");
        }
    }
}
