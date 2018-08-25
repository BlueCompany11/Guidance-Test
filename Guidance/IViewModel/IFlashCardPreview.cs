using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guidance.IViewModel
{
    public interface IFlashCardPreview
    {
        string Title { get; set; }
        DateTime CreationDate { get; set; }
        List<string> Tags { get; set; }
        DateTime LastOccurance { get; set; }
    }
}
