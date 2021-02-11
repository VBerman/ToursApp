using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ToursApp.Database
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
            : base("name=DatabaseContext")
        {
        }

        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Hotel> Hotels { get; set; }
        public virtual DbSet<HotelComment> HotelComments { get; set; }
        public virtual DbSet<HotelImage> HotelImages { get; set; }
        public virtual DbSet<Tour> Tours { get; set; }
        public virtual DbSet<Type> Types { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>()
                .Property(e => e.Code)
                .IsFixedLength();

            modelBuilder.Entity<Country>()
                .HasMany(e => e.Hotels)
                .WithRequired(e => e.Country)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Hotel>()
                .Property(e => e.CountryCode)
                .IsFixedLength();

            modelBuilder.Entity<Hotel>()
                .HasMany(e => e.HotelComments)
                .WithRequired(e => e.Hotel)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Hotel>()
                .HasMany(e => e.HotelImages)
                .WithRequired(e => e.Hotel)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Hotel>()
                .HasMany(e => e.Tours)
                .WithMany(e => e.Hotels)
                .Map(m => m.ToTable("HotelOfTour").MapLeftKey("HotelId").MapRightKey("TourId"));

            modelBuilder.Entity<Tour>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Tour>()
                .HasMany(e => e.Types)
                .WithMany(e => e.Tours)
                .Map(m => m.ToTable("TypeOfTour").MapLeftKey("TourId").MapRightKey("TypeId"));
        }
    }
}
