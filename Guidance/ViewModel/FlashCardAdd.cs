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
        private List<FileAnserw> currentFileAnserws = new List<FileAnserw>();
        private List<TextAnserw> textAnserws = new List<TextAnserw>();
        private List<Tag> tags = new List<Tag>();

        public void AddFile()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            bool? result = dlg.ShowDialog();
            if (result == true)
            {
                string filePath = dlg.FileName;
                var file = File.ReadAllBytes(filePath);
                var fileName = Path.GetFileName(filePath);
                currentFileAnserws.Add(new FileAnserw { File = file, FileName = fileName });
            }
        }

        public void AddSetOfFiles()
        {
            fileAnserws.AddRange(currentFileAnserws);
            fileAnserws.ForEach(fileAnserw => fileAnserw.Annotation = FileAnnotation);
            currentFileAnserws.Clear();
        }

        public void AddTextAnserw()
        {
            textAnserws.Add(new TextAnserw { Text = CurrentTextAnserw, Annotation = TextAnserwAnnotation });
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
    }
}
