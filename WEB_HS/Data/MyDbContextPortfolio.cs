using Microsoft.EntityFrameworkCore;
using WEB_HS.Entities;

namespace WEB_HS.Data
{
    public class MyDbContextPortfolio : DbContext
    {
        public MyDbContextPortfolio(DbContextOptions<MyDbContextPortfolio> options)
            : base(options)
        {
        }

        public DbSet<Personne> Personnes { get; set; }
        public DbSet<Competence> Competences { get; set; }
        public DbSet<Projet> Projets { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Entreprise> Entreprise { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //  Personne  Competence
            modelBuilder.Entity<Personne>()
                .HasMany(p => p.Competences)
                .WithOne(c => c.Personne)
                .HasForeignKey(c => c.PersonneId)
                .OnDelete(DeleteBehavior.Cascade);

            //  Personne  Projet
            modelBuilder.Entity<Personne>()
                .HasMany(p => p.Projets)
                .WithOne(p => p.Personne)
                .HasForeignKey(p => p.PersonneId)
                .OnDelete(DeleteBehavior.Cascade);

            //  Personne  Message
            modelBuilder.Entity<Personne>()
                .HasMany(p => p.MessagesRecus)
                .WithOne(m => m.Personne)
                .HasForeignKey(m => m.PersonneId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}