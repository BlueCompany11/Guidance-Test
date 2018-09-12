using Guidance.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guidance.FlashCardModel
{
    public class FlashCardTagFactory
    {
        public List<string> GetAllTags()
        {
            List<string> allTags = new List<string>();
            using (var repo = new TagRepository())
            {
                allTags = repo.GetAll().Select(x => x.Tag1).Distinct().ToList();
            }
            return allTags;
        }
    }
}
