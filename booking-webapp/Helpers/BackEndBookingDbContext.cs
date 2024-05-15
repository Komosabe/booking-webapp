using BackednBooking.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BackednBooking.Helpers
{
    public class BookingDbContext : DbContext
    {
        public DbSet<Concert> Concerts { get; set; }
        public DbSet<Hall> Halls { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<User> Users { get; set; }

        public BookingDbContext(DbContextOptions<BookingDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Concert>()
                .HasOne(c => c.Hall)
                .WithMany(h => h.Concerts)
                .HasForeignKey(c => c.HallId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Concert)
                .WithMany(c => c.Reservations)
                .HasForeignKey(r => r.ConcertId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reservations)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}
