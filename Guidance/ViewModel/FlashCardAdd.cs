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
        public string Title { get; set; }
        public List<string> Tags { get; set; }
        public string CurrentTextAnserw { get; set; }
        public string TextAnserwAnnotation { get; set; }
        public string FileAnnotation { get; set; }
        public string CurrentTag { get; set; }

        private List<FileAnserw> fileAnserws = new List<FileAnserw>();
        private List<TextAnserw> textAnserws = new List<TextAnserw>();
        private List<Tag> tags = new List<Tag>();
        //DODAC FUNCKJE DODAJACE TAGI I FILEANSERW NIE DODAOWALO
        public void AddTextAnserw(string textAnserw, string textAnserwAnnotation)
        {
            textAnserws.Add(new TextAnserw { Text = textAnserw, Annotation = textAnserwAnnotation });
        }
        public void AddTag()
        {
            Tags.Add(CurrentTag);
            tags.Add(new Tag { Tag1 = CurrentTag });
        }

        public FlashCard Save()
        {
            using (FlashCardRepository flashCardRepository = new FlashCardRepository())
            {
                var flashCard = new FlashCard
                {
                    Title = Title,
                    FileAnserws = fileAnserws,
                    TextAnserws = textAnserws,
                    Tags = tags,
                    FlashCardData = new FlashCardData()
                };
                return flashCard;
            }
        }

        public void AttachAnnotationToFile(string annotation)
        {
            try
            {
                var currentFileAnserw = fileAnserws.Last();
                currentFileAnserw.Annotation = annotation;
            }
            catch (Exception) { };  //if empty do nothing
        }

        public void AddFile(string filePath)
        {
            var file = File.ReadAllBytes(filePath);
            var fileName = Path.GetFileName(filePath);
            if(fileAnserws.Any(n=>n.FileName == fileName))
            {
                return; // do nothing cuz this file is already there
            }
            fileAnserws.Add(new FileAnserw { File = file, FileName = fileName });
        }

        public void SaveFlashCard()
        {
            //var flashCard1 = new FlashCard
            //{
            //    Title = Title,
            //    FileAnserws = fileAnserws,
            //    TextAnserws = textAnserws,
            //    Tags = tags,
            //    FlashCardData = new FlashCardData()
            //};
            //Console.WriteLine(flashCard1);
            using (FlashCardRepository flashCardRepository = new FlashCardRepository())
            {
                var flashCard = new FlashCard
                {
                    Title = Title,
                    FileAnserws = fileAnserws,
                    TextAnserws = textAnserws,
                    Tags = tags,
                    FlashCardData = new FlashCardData()
                };
                flashCardRepository.Add(flashCard);
            }
        }
    }
}
