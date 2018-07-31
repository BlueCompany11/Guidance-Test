namespace Guidance
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class FlashCardsEntities : DbContext
    {
        public FlashCardsEntities()
            : base("name=FlashCardsEntities")
        {
        }

        public virtual DbSet<FileAnserw> FileAnserws { get; set; }
        public virtual DbSet<FlashCard> FlashCards { get; set; }
        public virtual DbSet<PictureAnserw> PictureAnserws { get; set; }
        public virtual DbSet<TextAnserw> TextAnserws { get; set; }
        public virtual DbSet<FlashCardData> FlashCardDatas { get; set; }
        public virtual DbSet<AllAnserwsFlashCard> AllAnserwsFlashCards { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FlashCard>()
                .Property(e => e.Tags)
                .IsUnicode(false);

            modelBuilder.Entity<FlashCard>()
                .HasOptional(e => e.FileAnserw)
                .WithRequired(e => e.FlashCard);

            modelBuilder.Entity<FlashCard>()
                .HasMany(e => e.FlashCardDatas)
                .WithRequired(e => e.FlashCard)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FlashCard>()
                .HasOptional(e => e.PictureAnserw)
                .WithRequired(e => e.FlashCard);

            modelBuilder.Entity<FlashCard>()
                .HasOptional(e => e.TextAnserw)
                .WithRequired(e => e.FlashCard);

            modelBuilder.Entity<AllAnserwsFlashCard>()
                .Property(e => e.Tags)
                .IsUnicode(false);
        }
    }
}
