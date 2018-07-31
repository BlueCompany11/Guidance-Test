namespace Guidance
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class FileAnserw
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FlashCardId { get; set; }

        [Column("FileAnserw")]
        [Required]
        public byte[] FileAnserw1 { get; set; }

        public virtual FlashCard FlashCard { get; set; }
    }
}
