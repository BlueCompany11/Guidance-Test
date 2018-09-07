﻿using System;
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
using System.Windows.Input;

namespace Guidance.ViewModel
{
    public class FlashCardAdd : IAddFlashCard, INotifyPropertyChanged
    {
        public FlashCardAdd()
        {
            this.ReturnedFlashCard = new FlashCard();
        }
        public FlashCardAdd(FlashCard flashCard) : this()
        {
            this.ReturnedFlashCard = flashCard;
            canSaveFlashCard = true;
        }
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
        public ObservableCollection<string> TextAnserws { get; private set; } = new ObservableCollection<string>();
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
            Console.WriteLine("zamykam");
            //using (TagRepository repo = new TagRepository())
            //{
            //    foreach (var item in flashCard.Tags)
            //    {
            //        repo.Add(item);
            //    }
            //}
            //using (FlashCardRepository flashCardRepository = new FlashCardRepository())
            //{
            //    //dodanie
            //    //flashCard.FlashCardData = new FlashCardData();
            //    //flashCardRepository.Add(flashCard);
            //    //Console.WriteLine(flashCard.Id);
            //    //Console.WriteLine("Powinno sie zamknac okno");
            //    //update
            //    //var tempFlashCard = flashCardRepository.GetOne(flashCard.Id);
            //    //Console.WriteLine(tempFlashCard.Title);
            //    Console.WriteLine(flashCard.Id);
            //    //Console.WriteLine(tempFlashCard.Id);
            //    flashCardRepository.Save(flashCard);
            //}

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
            Console.WriteLine(Tags);
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

        public void PrintFlashCard()
        {
            Console.WriteLine(SelectedTag);
            Console.WriteLine(ReturnedFlashCard);
        }

        //public void DeleteFile(string fileName)
        //{
        //    flashCard.FileAnserws = flashCard.FileAnserws.Where(x => x.FileName != fileName).ToList();
        //    OnPropertyChanged("FilesNames");
        //}
    }
}
