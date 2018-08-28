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
        public string Title { get => flashCard.Title; set { flashCard.Title = value; Console.WriteLine("tytul"); } }
        public ObservableCollection<string> Tags
        {
            get => new ObservableCollection<string>(flashCard.Tags.Select(x => x.Tag1).ToList());
            set
            {
                flashCard.Tags = value.Select(tag => new Tag() { Tag1 = tag }).ToList();
                Console.WriteLine("asd");
            }
        }
        public string CurrentTextAnserw { get; set; }
        public string TextAnserwAnnotation { get; set; }
        public string FileAnnotation { get; set; }

        private List<FileAnserw> fileAnserws = new List<FileAnserw>();
        private List<TextAnserw> textAnserws = new List<TextAnserw>();
        private List<Tag> tags = new List<Tag>();
        //DODAC FUNCKJE DODAJACE TAGI I FILEANSERW NIE DODAOWALO
        public void AddTextAnserw(string textAnserw, string textAnserwAnnotation)
        {
            textAnserws.Add(new TextAnserw { Text = textAnserw, Annotation = textAnserwAnnotation });
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
            Console.WriteLine(flashCard);
            //using (FlashCardRepository flashCardRepository = new FlashCardRepository())
            //{
            //    var flashCard = new FlashCard
            //    {
            //        Title = Title,
            //        FileAnserws = fileAnserws,
            //        TextAnserws = textAnserws,
            //        Tags = tags,
            //        FlashCardData = new FlashCardData()
            //    };
            //    flashCardRepository.Add(flashCard);
            //}
        }
    }
}
