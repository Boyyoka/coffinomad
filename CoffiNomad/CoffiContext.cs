namespace CoffiNomad
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class CoffiContext : DbContext
    {
        public CoffiContext()
            : base("name=CoffiContext")
        {
        }

        public virtual DbSet<Caffee> Caffees { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Locatie> Locaties { get; set; }
        public virtual DbSet<Beoordeling> Beoordeling { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Caffee>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Caffee>()
                .Property(e => e.Straat)
                .IsUnicode(false);
            modelBuilder.Entity<Caffee>()
                .HasMany(e => e.Beoordelingen)
                .WithRequired(e => e.Caffee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.Naam)
                .IsUnicode(false);
            modelBuilder.Entity<Category>()
                .HasMany(e => e.Beoordelingen)
                .WithRequired(e => e.Category)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Locatie>()
                .Property(e => e.Stad)
                .IsUnicode(false);

            modelBuilder.Entity<Locatie>()
                .HasMany(e => e.Caffees)
                .WithRequired(e => e.Locatie)
                .WillCascadeOnDelete(false);
        }
    }
}
