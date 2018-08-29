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

namespace Guidance.ViewModel
{
    public class FlashCardAdd : IAddFlashCard
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
        public string Title { get => flashCard.Title; set => flashCard.Title = value; }
        public ObservableCollection<string> Tags
        {
            get => new ObservableCollection<string>(flashCard.Tags.Select(x => x.Tag1).ToList());
            set
            {
                flashCard.Tags = value.Select(tag => new Tag() { Tag1 = tag }).ToList();
            }
        }
        
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
            flashCard.FlashCardData = new FlashCardData();
            using (FlashCardRepository flashCardRepository = new FlashCardRepository())
            {
                flashCardRepository.Add(flashCard);
            }
        }
        public void PrintFlashCard()
        {
            Console.WriteLine(flashCard);
        }
    }
}
