namespace Guidance.DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AllAnserwsFlashCard")]
    public partial class AllAnserwsFlashCard
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string Title { get; set; }

        [Key]
        [Column(Order = 2)]
        public string Tags { get; set; }

        [Key]
        [Column(Order = 3)]
        public byte[] FileAnserw { get; set; }

        [Key]
        [Column(Order = 4)]
        public byte[] Picture { get; set; }

        [Key]
        [Column(Order = 5)]
        public string TextAnserw { get; set; }
    }
}
