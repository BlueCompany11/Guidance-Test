﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guidance.FlashCardModel;
using Guidance.IView;
using System.Collections.ObjectModel;

namespace Guidance.FlashCardModel
{
    public class FlashCardPreview:IFlashCardPreview
    {
        public string Title { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastOccurance { get; set; }
        public int Id { get; }

        public int RecallVal { get; set; }

        public FlashCardPreview(int id)
        {
            Id = id;
        }
    }
}
