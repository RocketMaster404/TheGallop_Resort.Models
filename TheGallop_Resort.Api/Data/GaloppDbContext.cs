using Microsoft.EntityFrameworkCore;
using System;
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
                },
                new RoomDetail
                {
                    Id = 4,
                    Beds = 1,
                    MaxAdults = 2,
                    MaxChildren = 1
                },
                new RoomDetail
                {
                    Id = 5,
                    Beds = 2,
                    MaxAdults = 3,
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
                },
                new RoomCategory
                {
                    Id = 4,
                    Type = RoomType.SingleBed,
                    CategoryPrice = 1299,
                    RoomDetailId = 4
                },
                new RoomCategory
                {
                    Id = 5,
                    Type = RoomType.DoubleBed,
                    CategoryPrice = 2199,
                    RoomDetailId = 5
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
                },
                new Room
                {
                    Id = 6,
                    RoomNr = 1202,
                    RoomCategoryId = 3
                },
                new Room
                {
                    Id = 7,
                    RoomNr = 1301,
                    RoomCategoryId = 4
                },
                new Room
                {
                    Id = 8,
                    RoomNr = 1302,
                    RoomCategoryId = 4
                },
                new Room
                {
                    Id = 9,
                    RoomNr = 1401,
                    RoomCategoryId = 5
                },
                new Room
                {
                    Id = 10,
                    RoomNr = 1402,
                    RoomCategoryId = 5
                }
            );



            modelBuilder.Entity<Guest>().HasData(
                new Guest { Id = 1, FirstName = "Erik", LastName = "Johansson", Email = "erik.johansson@email.com", PhoneNumber = "0701234567" },
                new Guest { Id = 2, FirstName = "Anna", LastName = "Svensson", Email = "anna.svensson@email.com", PhoneNumber = "0709876543" },
                new Guest { Id = 3, FirstName = "Johan", LastName = "Karlsson", Email = "johan.karlsson@email.com", PhoneNumber = "0701112233" },
                new Guest { Id = 4, FirstName = "Emma", LastName = "Nilsson", Email = "emma.nilsson@email.com", PhoneNumber = "0702223344" },
                new Guest { Id = 5, FirstName = "Lucas", LastName = "Andersson", Email = "lucas.andersson@email.com", PhoneNumber = "0703334455" },
                new Guest { Id = 6, FirstName = "Maja", LastName = "Lindberg", Email = "maja.lindberg@email.com", PhoneNumber = "0704445566" },
                new Guest { Id = 7, FirstName = "Oscar", LastName = "Berg", Email = "oscar.berg@email.com", PhoneNumber = "0705556677" },
                new Guest { Id = 8, FirstName = "Sofia", LastName = "Holm", Email = "sofia.holm@email.com", PhoneNumber = "0706667788" },
                new Guest { Id = 9, FirstName = "William", LastName = "Ekström", Email = "william.ekstrom@email.com", PhoneNumber = "0707778899" },
                new Guest { Id = 10, FirstName = "Ella", LastName = "Fors", Email = "ella.fors@email.com", PhoneNumber = "0708889900" }
            );

            modelBuilder.Entity<Booking>().HasData(
                new Booking { Id = 1, GuestId = 1, CreatedAt = new DateTime(2025, 1, 10), Status = Status.Confirmed, TotalPrice = 3598 },
                new Booking { Id = 2, GuestId = 2, CreatedAt = new DateTime(2025, 3, 14), Status = Status.Confirmed, TotalPrice = 6998 },
                new Booking { Id = 3, GuestId = 3, CreatedAt = new DateTime(2025, 5, 2), Status = Status.Preliminary, TotalPrice = 1799 },
                new Booking { Id = 4, GuestId = 4, CreatedAt = new DateTime(2025, 7, 20), Status = Status.Cancelled, TotalPrice = 3499 },
                new Booking { Id = 5, GuestId = 5, CreatedAt = new DateTime(2025, 9, 11), Status = Status.Confirmed, TotalPrice = 999 },
                new Booking { Id = 6, GuestId = 6, CreatedAt = new DateTime(2025, 11, 1), Status = Status.Confirmed, TotalPrice = 5397 },
                new Booking { Id = 7, GuestId = 7, CreatedAt = new DateTime(2026, 1, 15), Status = Status.Preliminary, TotalPrice = 1799 },
                new Booking { Id = 8, GuestId = 8, CreatedAt = new DateTime(2026, 2, 10), Status = Status.Confirmed, TotalPrice = 3499 },
                new Booking { Id = 9, GuestId = 9, CreatedAt = new DateTime(2026, 4, 5), Status = Status.Confirmed, TotalPrice = 6998 },
                new Booking { Id = 10, GuestId = 10, CreatedAt = new DateTime(2026, 6, 1), Status = Status.Preliminary, TotalPrice = 999 }
            );

            modelBuilder.Entity<RoomReservation>().HasData(
                new RoomReservation
                {
                    Id = 1,
                    BookingId = 1,
                    RoomId = 3,
                    CheckIn = new DateTime(2025, 1, 15),
                    CheckOut = new DateTime(2025, 1, 17),
                    Adults = 2,
                    Children = 1,
                    PricePerNight = 1799,
                    RoomStatus = RoomStatus.Confirmed
                },
                new RoomReservation
                {
                    Id = 2,
                    BookingId = 2,
                    RoomId = 5,
                    CheckIn = new DateTime(2025, 3, 20),
                    CheckOut = new DateTime(2025, 3, 22),
                    Adults = 2,
                    Children = 0,
                    PricePerNight = 3499,
                    RoomStatus = RoomStatus.Confirmed
                },
                new RoomReservation
                {
                    Id = 3,
                    BookingId = 3,
                    RoomId = 1,
                    CheckIn = new DateTime(2025, 5, 10),
                    CheckOut = new DateTime(2025, 5, 11),
                    Adults = 1,
                    Children = 0,
                    PricePerNight = 999,
                    RoomStatus = RoomStatus.Cancelled
                },
                new RoomReservation
                {
                    Id = 4,
                    BookingId = 4,
                    RoomId = 5,
                    CheckIn = new DateTime(2025, 8, 1),
                    CheckOut = new DateTime(2025, 8, 2),
                    Adults = 2,
                    Children = 2,
                    PricePerNight = 3499,
                    RoomStatus = RoomStatus.Cancelled
                },
                new RoomReservation
                {
                    Id = 5,
                    BookingId = 5,
                    RoomId = 2,
                    CheckIn = new DateTime(2025, 10, 12),
                    CheckOut = new DateTime(2025, 10, 13),
                    Adults = 1,
                    Children = 0,
                    PricePerNight = 999,
                    RoomStatus = RoomStatus.Confirmed
                },
                new RoomReservation
                {
                    Id = 6,
                    BookingId = 6,
                    RoomId = 4,
                    CheckIn = new DateTime(2025, 12, 20),
                    CheckOut = new DateTime(2025, 12, 23),
                    Adults = 2,
                    Children = 1,
                    PricePerNight = 1799,
                    RoomStatus = RoomStatus.Confirmed
                },
                new RoomReservation
                {
                    Id = 7,
                    BookingId = 7,
                    RoomId = 3,
                    CheckIn = new DateTime(2026, 6, 10),
                    CheckOut = new DateTime(2026, 6, 12),
                    Adults = 2,
                    Children = 0,
                    PricePerNight = 1799,
                    RoomStatus = RoomStatus.Preliminary
                },
                new RoomReservation
                {
                    Id = 8,
                    BookingId = 8,
                    RoomId = 5,
                    CheckIn = new DateTime(2026, 7, 15),
                    CheckOut = new DateTime(2026, 7, 17),
                    Adults = 2,
                    Children = 2,
                    PricePerNight = 3499,
                    RoomStatus = RoomStatus.Confirmed
                },
                new RoomReservation
                {
                    Id = 9,
                    BookingId = 9,
                    RoomId = 5,
                    CheckIn = new DateTime(2026, 12, 23),
                    CheckOut = new DateTime(2026, 12, 27),
                    Adults = 2,
                    Children = 1,
                    PricePerNight = 3499,
                    RoomStatus = RoomStatus.Confirmed
                },
                new RoomReservation
                {
                    Id = 10,
                    BookingId = 10,
                    RoomId = 1,
                    CheckIn = new DateTime(2027, 2, 14),
                    CheckOut = new DateTime(2027, 2, 15),
                    Adults = 1,
                    Children = 0,
                    PricePerNight = 999,
                    RoomStatus = RoomStatus.Preliminary
                }
            );




        }
    }
}
