﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guidance.IViewModel
{
    public interface IFlashCardPreview
    {
        string Title { get; }

        string Tag { get; }
    }
}