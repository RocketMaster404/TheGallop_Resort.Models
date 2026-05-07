using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGallop_Resort.Models.Models;

namespace TheGallop_Resort.Models
{
    public class GaloppDbContext : DbContext
    {
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<RoomDetail> RoomDetails { get; set; }
        public DbSet<RoomCategory> RoomCategories { get; set; }
        public DbSet<ReservationRoom> ReservationRooms { get; set; }

        public GaloppDbContext(DbContextOptions<GaloppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ReservationRoom>().HasKey(r => new { r.ReservationId, r.RoomId });
        }
    }
}
