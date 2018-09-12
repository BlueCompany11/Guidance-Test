using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guidance.IView
{
    public interface IFlashCardPreview
    {
        string Title { get; }
        DateTime CreationDate { get; }
        DateTime LastOccurance { get; }
        int Id { get; }
        int RecallVal { get; }
    }
}
