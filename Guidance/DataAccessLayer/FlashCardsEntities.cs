namespace Guidance.DataAccessLayer
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Guidance.FlashCardModel;

    public partial class FlashCardsEntities : DbContext
    {
        public FlashCardsEntities()
            : base("name=FlashCardsEntities1")
        {
        }

        public virtual DbSet<FileAnserw> FileAnserws { get; set; }
        public virtual DbSet<FlashCardData> FlashCardDatas { get; set; }
        public virtual DbSet<FlashCard> FlashCards { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<TextAnserw> TextAnserws { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FlashCard>()
                .HasMany(e => e.FileAnserws)
                .WithRequired(e => e.FlashCard)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FlashCard>()
                .HasOptional(e => e.FlashCardData)
                .WithRequired(e => e.FlashCard);

            modelBuilder.Entity<FlashCard>()
                .HasMany(e => e.TextAnserws)
                .WithRequired(e => e.FlashCard)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FlashCard>()
                .HasMany(e => e.Tags)
                .WithMany(e => e.FlashCards)
                .Map(m => m.ToTable("FlashCardTag").MapLeftKey("IdFlashCard").MapRightKey("IdTag"));
        }
    }
}
