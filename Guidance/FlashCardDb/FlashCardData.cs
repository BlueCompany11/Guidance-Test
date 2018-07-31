namespace Guidance
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FlashCardData")]
    public partial class FlashCardData
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FlashCardId { get; set; }

        public bool? LastResult { get; set; }

        [Column(TypeName = "date")]
        public DateTime? LastOccurrence { get; set; }

        public int? SuccessfulAnserws { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "date")]
        public DateTime CreationDate { get; set; }

        public virtual FlashCard FlashCard { get; set; }
    }
}
