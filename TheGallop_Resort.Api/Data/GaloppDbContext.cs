using Microsoft.EntityFrameworkCore;
using TheGallop_Resort.Models.Models;

namespace TheGallop_Resort.Api.Data
{
    public class GaloppDbContext : DbContext
    {
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<RoomDetail> RoomDetails { get; set; }
        public DbSet<RoomCategory> RoomCategories { get; set; }
        public DbSet<RoomReservation> RoomReservations { get; set; }

        public GaloppDbContext(DbContextOptions<GaloppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RoomDetail>().HasData(
                new RoomDetail
                {
                    Id = 1,
                    Beds = 1,
                    MaxAdults = 1,
                    MaxChildren = 0
                },
                new RoomDetail
                {
                    Id = 2,
                    Beds = 2,
                    MaxAdults = 2,
                    MaxChildren = 2
                },
                new RoomDetail
                {
                    Id = 3,
                    Beds = 3,
                    MaxAdults = 4,
                    MaxChildren = 2
                }
            );

            modelBuilder.Entity<RoomCategory>().HasData(
                new RoomCategory
                {
                    Id = 1,
                    Type = RoomType.SingleBed,
                    CategoryPrice = 999,
                    RoomDetailId = 1
                },
                new RoomCategory
                {
                    Id = 2,
                    Type = RoomType.DoubleBed,
                    CategoryPrice = 1799,
                    RoomDetailId = 2
                },
                new RoomCategory
                {
                    Id = 3,
                    Type = RoomType.Suite,
                    CategoryPrice = 3499,
                    RoomDetailId = 3
                }
            );

            modelBuilder.Entity<Room>().HasData(
                new Room
                {
                    Id = 1,
                    RoomNr = 1001,
                    RoomCategoryId = 1
                },
                new Room
                {
                    Id = 2,
                    RoomNr = 1002,
                    RoomCategoryId = 1
                },
                new Room
                {
                    Id = 3,
                    RoomNr = 1101,
                    RoomCategoryId = 2
                },
                new Room
                {
                    Id = 4,
                    RoomNr = 1102,
                    RoomCategoryId = 2
                },
                new Room
                {
                    Id = 5,
                    RoomNr = 1201,
                    RoomCategoryId = 3
                }
            );

            modelBuilder.Entity<Guest>().HasData(
                new Guest
                {
                    Id = 1,
                    FirstName = "Erik",
                    LastName = "Johansson",
                    Email = "erik.johansson@email.com",
                    PhoneNumber = "0701234567"
                },
                new Guest
                {
                    Id = 2,
                    FirstName = "Anna",
                    LastName = "Svensson",
                    Email = "anna.svensson@email.com",
                    PhoneNumber = "0709876543"
                }
            );

            modelBuilder.Entity<Booking>().HasData(
                new Booking
                {
                    Id = 1,
                    GuestId = 1,
                    CreatedAt = new DateTime(2026, 5, 20),
                    Status = Status.Confirmed,
                    TotalPrice = 3598
                },
                new Booking
                {
                    Id = 2,
                    GuestId = 2,
                    CreatedAt = new DateTime(2026, 5, 21),
                    Status = Status.Preliminary,
                    TotalPrice = 3699
                }
            );

            modelBuilder.Entity<RoomReservation>().HasData(
                new RoomReservation
                {
                    Id = 1,
                    BookingId = 1,
                    RoomId = 3,
                    CheckIn = new DateTime(2026, 6, 1),
                    CheckOut = new DateTime(2026, 6, 3),
                    Adults = 2,
                    Children = 1,
                    PricePerNight = 1789,
                    RoomStatus = RoomStatus.Confirmed
                },
                new RoomReservation
                {
                    Id = 2,
                    BookingId = 2,
                    RoomId = 5,
                    CheckIn = new DateTime(2026, 7, 9),
                    CheckOut = new DateTime(2026, 7, 11),
                    Adults = 2,
                    Children = 0,
                    PricePerNight = 3499,
                    RoomStatus = RoomStatus.Preliminary
                }
            );
        }
    }
}
