namespace Guidance
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PictureAnserw
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FlashCardId { get; set; }

        [Required]
        public byte[] Picture { get; set; }

        public virtual FlashCard FlashCard { get; set; }
    }
}
