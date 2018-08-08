using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guidance.DataAccessLayer;
using Guidance.IViewModel;
using Guidance.FlashCardModel;
namespace Guidance.ViewModel
{
    public class FlashCardAdd : IAddFlashCard
    {
        public FlashCard FlashCard { get; set; } = new FlashCard();
        public List<TextAnserw> TextAnserws { get; set; } = new List<TextAnserw>();
        public List<FileAnserw> FileAnserws { get; set; } = new List<FileAnserw>();
        public void AddFileAnserw(string fileName, byte[] file, string annotation = null)
        {
            this.FileAnserws.Add(new FileAnserw { File = file, FileName = fileName, Annotation = annotation});
        }

        public void AddFlashCard(string title, List<string> tags)
        {
            this.FlashCard = new FlashCard { Title = title, Tags = JoinStrings(tags) };
        }

        public void AddTextAnserw(string text, string annotation = null)
        {
            this.TextAnserws.Add(new TextAnserw { Text = text, Annotation = annotation });
        }

        private string JoinStrings(List<string> list, string separator = "#")
        {
            string joinedString = null;
            if (list.Count != 0)
            {
                joinedString = "";
            }
            foreach (var item in list)
            {
                joinedString += "#";
                joinedString += item;
            }
            return joinedString;
        }
    }
}
