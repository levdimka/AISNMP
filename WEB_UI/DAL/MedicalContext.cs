using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using WEB_UI.Models;

namespace WEB_UI.DAL
{
    public partial class MedicalContext : DbContext
    {
        public MedicalContext()
            : base("name=Medical")
        {
        }

        public virtual DbSet<Card> Card { get; set; }
        public virtual DbSet<Card_information> Card_information { get; set; }
        public virtual DbSet<Doctor> Doctor { get; set; }
        public virtual DbSet<Home> Home { get; set; }
        public virtual DbSet<Pacient> Pacient { get; set; }
        public virtual DbSet<Queue> Queue { get; set; }
        public virtual DbSet<Recipe> Recipe { get; set; }
        public virtual DbSet<Specialization> Specialization { get; set; }
        public virtual DbSet<Visit> Visit { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Card>()
                .HasMany(e => e.Card_information)
                .WithRequired(e => e.Card)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Card>()
                .HasMany(e => e.Pacient)
                .WithRequired(e => e.Card)
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Card_information>()
            //    .HasMany(e => e.Recipe)
            //    .WithRequired(e => e.Card_information)
            //    .HasForeignKey(e => e.idCard_information)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<Doctor>()
                .HasMany(e => e.Card_information)
                .WithRequired(e => e.Doctor)
                .HasForeignKey(e => e.idDoctor)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Doctor>()
                .HasMany(e => e.Queue)
                .WithRequired(e => e.Doctor)
                .HasForeignKey(e => e.idDoctor)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Home>()
                .HasMany(e => e.Queue)
                .WithOptional(e => e.Home)
                .HasForeignKey(e => e.idHome);

            modelBuilder.Entity<Pacient>()
                .HasMany(e => e.Queue)
                .WithRequired(e => e.Pacient)
                .HasForeignKey(e => e.idPacient)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Specialization>()
                .HasMany(e => e.Doctor)
                .WithRequired(e => e.Specialization)
                .HasForeignKey(e => e.IdSpecialization)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Visit>()
                .HasMany(e => e.Queue)
                .WithOptional(e => e.Visit)
                .HasForeignKey(e => e.idVisit);
        }
    }
}
