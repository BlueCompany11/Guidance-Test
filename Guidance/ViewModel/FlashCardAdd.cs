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
        public string Title { get; set; }
        public List<string> Tags { get; set; }
        public string TextAnserw { get; set; }
        public List<byte[]> PictureAnserws { get; set; }


        public FlashCardAdd()
        {
            PictureAnserws = new List<byte[]>();
        }

        public void AddPicture()
        {
            throw new NotImplementedException();
        }

        public void AddTag(string input)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
        }
        private string ConvertTags()
        {
            string tags = "";
            for (int i = 0; i < Tags.Count; i++)
            {
                Tags[i] += "#";
                tags += Tags[i];
            }
            return tags;
        }
    }
}
