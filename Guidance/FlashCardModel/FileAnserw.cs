namespace Guidance.FlashCardModel
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
        public byte[] File { get; set; }

        [Required]
        [StringLength(50)]
        public string FileName { get; set; }

        public virtual FlashCard FlashCard { get; set; }
    }
}
