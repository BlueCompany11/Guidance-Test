namespace Guidance.FlashCardModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TextAnserw
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FlashCardId { get; set; }

        [Column("TextAnserw")]
        [Required]
        public string Text { get; set; }

        public virtual FlashCard FlashCard { get; set; }
    }
}
