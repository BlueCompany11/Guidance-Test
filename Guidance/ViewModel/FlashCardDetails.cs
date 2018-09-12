using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guidance.DataAccessLayer;
using Guidance.IView;
using Guidance.FlashCardModel;
using System.IO;
using System.ComponentModel;
using System.Windows.Input;

namespace Guidance.ViewModel
{
    public class FlashCardDetails : IFlashCardDetails, INotifyPropertyChanged
    {
        FlashCardFactory flashCardFactory;
        #region ctors
        /// <summary>
        /// Can be used when need to create new flash card. No help providers will appear.
        /// </summary>
        public FlashCardDetails()
        {
            AllTags = new ObservableCollection<string>();
            flashCardFactory = new FlashCardFactory();
            ReturnedFlashCard = flashCardFactory.GetFlashCard();
            canSaveFlashCard = true;
            canMaterializeFlashCardAnserws = true;
        }
        /// <summary>
        /// Can be used when need to create new flash card. 
        /// </summary>
        /// <param name="tags">Provides list of tags which will appear during tag insertion. </param>
        public FlashCardDetails(List<string> tags):this()
        {
            AllTags = new ObservableCollection<string>(tags);
        }
        /// <summary>
        /// Can be used when class has to load flash card on it's own
        /// </summary>
        /// <param name="flashCardId"></param>
        public FlashCardDetails(int flashCardId):this()
        {
            this.ReturnedFlashCard = flashCardFactory.GetFlashCard(flashCardId);
        }
        /// <summary>
        /// Use only when all data is loaded.
        /// </summary>
        /// <param name="flashCard"></param>
        public FlashCardDetails(FlashCard flashCard):this()
        {
            this.ReturnedFlashCard = flashCard;
        }
        /// <summary>
        /// Use when all data from flash card is loaded. 
        /// </summary>
        /// <param name="flashCard"></param>
        /// <param name="tags"></param>
        public FlashCardDetails(FlashCard flashCard, List<string> tags) : this()
        {
            AllTags = new ObservableCollection<string>(tags);
            ReturnedFlashCard = flashCard;
        }
        #endregion
        public FlashCard ReturnedFlashCard { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public string Title
        {
            get => ReturnedFlashCard.Title;
            set
            {
                ReturnedFlashCard.Title = value;
                OnPropertyChanged("Title");
                if (value != "")
                {
                    canSaveFlashCard = true;
                }
                else
                {
                    canSaveFlashCard = false;
                }
                saveFlashCard.CanExecute(canSaveFlashCard);
            }
        }
        public ObservableCollection<string> Tags
        {
            get => new ObservableCollection<string>(ReturnedFlashCard.Tags.Select(x => x.Tag1).ToList());
            set
            {
                ReturnedFlashCard.Tags = value.Select(tag => new Tag() { Tag1 = tag }).ToList();    //czy to dobrze?
                OnPropertyChanged(nameof(Tags));
            }
        }
        public ObservableCollection<string> FilesNames
        {
            get => new ObservableCollection<string>(ReturnedFlashCard.FileAnserws.Select(x => x.FileName).ToList());
        }
        public ObservableCollection<string> TextAnserws
        {
            get => new ObservableCollection<string>(ReturnedFlashCard.TextAnserws.Select(x => x.Annotation).ToList());
        }
        ICommand addTag;
        bool canAddTagCommand;
        public ICommand AddTag { get { return addTag ?? (addTag = new CommandHandler(AddTagCommand, canAddTagCommand)); } }
        void AddTagCommand()
        {
            //Tags.Add(NewTag);
            ReturnedFlashCard.Tags.Add(new Tag { Tag1 = NewTag });
            NewTag = "";
            OnPropertyChanged("Tags");
        }
        ICommand saveFlashCard;
        bool canSaveFlashCard;
        public ICommand SaveFlashCard { get { return saveFlashCard ?? (saveFlashCard = new CommandHandler(SaveCommand, canSaveFlashCard)); } }
        void SaveCommand()
        {
            Save = true;
            ReturnedFlashCard.FlashCardData = new FlashCardData();
            flashCardFactory.AddFlashCard(ReturnedFlashCard);
        }
        string textAnserw;
        public string TextAnserw
        {
            get { return textAnserw; }
            set
            {
                textAnserw = value;
                OnPropertyChanged("TextAnserw");
                if (value != "")
                {
                    canSaveTextAnserw = true;
                }
                else
                {
                    canSaveTextAnserw = false;
                }
                SaveTextAnserw.CanExecute(canSaveTextAnserw);
            }
        }
        string textAnnotation;
        public string TextAnnotation { get { return textAnnotation; } set { textAnnotation = value; OnPropertyChanged("TextAnnotation"); } }
        ICommand saveTextAnserw;
        bool canSaveTextAnserw;
        public ICommand SaveTextAnserw { get { return saveTextAnserw ?? (saveTextAnserw = new CommandHandler(SaveTextAnserwCommand, canSaveTextAnserw)); } }
        void SaveTextAnserwCommand()
        {
            ReturnedFlashCard.TextAnserws.Add(new TextAnserw { Text = textAnnotation, Annotation = textAnnotation });
            TextAnserws.Add(TextAnnotation);
            TextAnnotation = "";
            TextAnserw = "";

        }
        string fileAnnotation;
        public string FileAnnotation { get { return fileAnnotation; } set { fileAnnotation = value; OnPropertyChanged("FileAnnotation"); } }
        ICommand addFile;
        public ICommand AddFile
        {
            get { return addFile ?? (addFile = new CommandHandler(AddFileCommand, true)); }
        }
        void AddFileCommand()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            bool? result = dlg.ShowDialog();
            if (result == true)
            {
                string filePath = dlg.FileName;
                currentFileAnserw.File = File.ReadAllBytes(filePath);
                currentFileAnserw.FileName = Path.GetFileName(filePath);
                canSaveFileAnserw = true;
                saveFileAnserw.CanExecute(canSaveFileAnserw);
            }
        }
        FileAnserw currentFileAnserw = new FileAnserw();
        ICommand saveFileAnserw;
        bool canSaveFileAnserw;
        public ICommand SaveFileAnserw { get { return saveFileAnserw ?? (saveFileAnserw = new CommandHandler(SaveFileAnserwCommand, canSaveFileAnserw)); } }
        void SaveFileAnserwCommand()
        {
            //string filePath = flashCard.FileAnserws.Last().FileName;
            //var file = File.ReadAllBytes(filePath);
            //var fileName = Path.GetFileName(filePath);
            //if (flashCard.FileAnserws.Any(n => n.FileName == fileName))   //zamienic to na boola we wlasciwosci chyba jednak nie ma potrzeby skoro jest podglad
            //{
            //    return; // do nothing cuz this file is already there
            //}
            currentFileAnserw.Annotation = FileAnnotation;
            ReturnedFlashCard.FileAnserws.Add(currentFileAnserw);
            canSaveFileAnserw = false;
            saveFileAnserw.CanExecute(canSaveFileAnserw);
            FileAnnotation = "";
            OnPropertyChanged(nameof(FilesNames));
        }
        string newTag;
        public string NewTag
        {
            get { return newTag; }
            set {
                newTag = value;
                OnPropertyChanged("NewTag");
                if (value != "")
                {
                    canAddTagCommand = true;
                }
                else
                    canAddTagCommand = false;
                addTag.CanExecute(canAddTagCommand);
            }
        }
        ICommand deleteTag;
        bool canDeleteTag;
        public ICommand DeleteTag
        {
            get
            {
                return deleteTag ?? (deleteTag = new CommandHandler(DeleteTagCommand, canDeleteTag));
            }
        }
        void DeleteTagCommand()
        {
            Tags = new ObservableCollection<string>(Tags.Where(tag => tag != SelectedTag).ToList());
            OnPropertyChanged("Tags");
            SelectedTag = null;
        }
        string selectedTag;
        public string SelectedTag
        {
            get
            {
                return selectedTag;
            }
            set
            {
                selectedTag = value;
                OnPropertyChanged("SelectedTag");
                if(selectedTag != "" && selectedTag != null)
                {
                    canDeleteTag = true;
                }
                else
                {
                    canDeleteTag = false;
                }
                DeleteTag.CanExecute(canDeleteTag);
            }
        }
        ICommand materializeFlashCardAnserws;
        bool canMaterializeFlashCardAnserws;
        public ICommand MaterializeFlashCardAnserws
        {
            get
            {
                return materializeFlashCardAnserws ?? (materializeFlashCardAnserws = new CommandHandler(MaterializeFlashCardAnserwsCommand, canMaterializeFlashCardAnserws));
            }
        }

        public bool Save { get; private set; }

        public ObservableCollection<string> AllTags { get; private set; }

        void MaterializeFlashCardAnserwsCommand()
        {
            //utworz folder o nazwie flash card i sciezce domyslnej na pulpit z folderem Guidance
            string folderName = ReturnedFlashCard.Title;
            string mainFolderName = "//Guidance//";
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + mainFolderName;
            if (!Directory.Exists(path))
            {
                DirectoryInfo di2 = Directory.CreateDirectory(path);
            }    
            path += folderName;
            DirectoryInfo di = Directory.CreateDirectory(path);
            foreach (var item in ReturnedFlashCard.FileAnserws)
            {
                //sprawdzic czy annotation jest puste
                File.WriteAllBytes(path + "//" + item.FileName, item.File);
            }
        }
    }
}
