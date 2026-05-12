using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public DbSet<Amenity> Amenities { get; set; }

        public GaloppDbContext(DbContextOptions<GaloppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ── Amenity ──────────────────────────────────────────────────────────────
            modelBuilder.Entity<Amenity>().HasData(
                new Amenity { Id = 1, Title = "Free WiFi" },
                new Amenity { Id = 2, Title = "Air Conditioning" },
                new Amenity { Id = 3, Title = "Mini Bar" },
                new Amenity { Id = 4, Title = "Flat-screen TV" },
                new Amenity { Id = 5, Title = "Coffee Maker" },
                new Amenity { Id = 6, Title = "Safe Box" },
                new Amenity { Id = 7, Title = "Bathtub" },
                new Amenity { Id = 8, Title = "Sea View" },
                new Amenity { Id = 9, Title = "Balcony" },
                new Amenity { Id = 10, Title = "Room Service" }
            );

            // ── RoomDetail ───────────────────────────────────────────────────────────
            modelBuilder.Entity<RoomDetail>().HasData(
                new RoomDetail { Id = 1, Beds = 1, MaxAdults = 1, MaxChildren = 0 },
                new RoomDetail { Id = 2, Beds = 1, MaxAdults = 2, MaxChildren = 1 },
                new RoomDetail { Id = 3, Beds = 2, MaxAdults = 2, MaxChildren = 2 },
                new RoomDetail { Id = 4, Beds = 2, MaxAdults = 3, MaxChildren = 2 },
                new RoomDetail { Id = 5, Beds = 3, MaxAdults = 4, MaxChildren = 3 }
            );

            // ── RoomDetail <-> Amenity (many-to-many junction) ────────────────────────
            modelBuilder.Entity<RoomDetail>()
                .HasMany(rd => rd.Amenities)
                .WithMany()
                .UsingEntity(j => j.HasData(
                    new { RoomDetailsId = 1, AmenitiesId = 1 },
                    new { RoomDetailsId = 1, AmenitiesId = 2 },
                    new { RoomDetailsId = 1, AmenitiesId = 4 },

                    new { RoomDetailsId = 2, AmenitiesId = 1 },
                    new { RoomDetailsId = 2, AmenitiesId = 2 },
                    new { RoomDetailsId = 2, AmenitiesId = 4 },
                    new { RoomDetailsId = 2, AmenitiesId = 5 },

                    new { RoomDetailsId = 3, AmenitiesId = 1 },
                    new { RoomDetailsId = 3, AmenitiesId = 2 },
                    new { RoomDetailsId = 3, AmenitiesId = 3 },
                    new { RoomDetailsId = 3, AmenitiesId = 4 },
                    new { RoomDetailsId = 3, AmenitiesId = 5 },
                    new { RoomDetailsId = 3, AmenitiesId = 6 },

                    new { RoomDetailsId = 4, AmenitiesId = 1 },
                    new { RoomDetailsId = 4, AmenitiesId = 2 },
                    new { RoomDetailsId = 4, AmenitiesId = 3 },
                    new { RoomDetailsId = 4, AmenitiesId = 4 },
                    new { RoomDetailsId = 4, AmenitiesId = 7 },
                    new { RoomDetailsId = 4, AmenitiesId = 9 },

                    new { RoomDetailsId = 5, AmenitiesId = 1 },
                    new { RoomDetailsId = 5, AmenitiesId = 2 },
                    new { RoomDetailsId = 5, AmenitiesId = 3 },
                    new { RoomDetailsId = 5, AmenitiesId = 4 },
                    new { RoomDetailsId = 5, AmenitiesId = 6 },
                    new { RoomDetailsId = 5, AmenitiesId = 7 },
                    new { RoomDetailsId = 5, AmenitiesId = 8 },
                    new { RoomDetailsId = 5, AmenitiesId = 9 },
                    new { RoomDetailsId = 5, AmenitiesId = 10 }
                ));

            // ── RoomCategory ─────────────────────────────────────────────────────────
            modelBuilder.Entity<RoomCategory>().HasData(
                new RoomCategory { Id = 1, Type = RoomType.SingleBed, CategoryPrice = 89.00m, RoomDetailId = 1 },
                new RoomCategory { Id = 2, Type = RoomType.SingleBed, CategoryPrice = 109.00m, RoomDetailId = 2 },
                new RoomCategory { Id = 3, Type = RoomType.DoubleBed, CategoryPrice = 149.00m, RoomDetailId = 3 },
                new RoomCategory { Id = 4, Type = RoomType.DoubleBed, CategoryPrice = 179.00m, RoomDetailId = 4 },
                new RoomCategory { Id = 5, Type = RoomType.Suite, CategoryPrice = 349.00m, RoomDetailId = 5 }
            );

            // ── Room ─────────────────────────────────────────────────────────────────
            modelBuilder.Entity<Room>().HasData(
                new Room { Id = 1, RoomNr = 101, RoomCategoryId = 1 },
                new Room { Id = 2, RoomNr = 102, RoomCategoryId = 1 },
                new Room { Id = 3, RoomNr = 103, RoomCategoryId = 2 },
                new Room { Id = 4, RoomNr = 104, RoomCategoryId = 2 },
                new Room { Id = 5, RoomNr = 201, RoomCategoryId = 3 },
                new Room { Id = 6, RoomNr = 202, RoomCategoryId = 3 },
                new Room { Id = 7, RoomNr = 203, RoomCategoryId = 4 },
                new Room { Id = 8, RoomNr = 204, RoomCategoryId = 4 },
                new Room { Id = 9, RoomNr = 301, RoomCategoryId = 5 },
                new Room { Id = 10, RoomNr = 302, RoomCategoryId = 5 }
            );

            // ── Guest ─────────────────────────────────────────────────────────────────
            modelBuilder.Entity<Guest>().HasData(
                new Guest { Id = 1, FirstName = "James", LastName = "Anderson", Email = "james.anderson@email.com", PhoneNumber = "+1-555-0101" },
                new Guest { Id = 2, FirstName = "Sofia", LastName = "Martinez", Email = "sofia.martinez@email.com", PhoneNumber = "+1-555-0102" },
                new Guest { Id = 3, FirstName = "Liam", LastName = "Johnson", Email = "liam.johnson@email.com", PhoneNumber = "+44-7911-100103" },
                new Guest { Id = 4, FirstName = "Emma", LastName = "Williams", Email = "emma.williams@email.com", PhoneNumber = "+44-7911-100104" },
                new Guest { Id = 5, FirstName = "Noah", LastName = "Brown", Email = "noah.brown@email.com", PhoneNumber = "+1-555-0105" },
                new Guest { Id = 6, FirstName = "Olivia", LastName = "Davis", Email = "olivia.davis@email.com", PhoneNumber = "+1-555-0106" },
                new Guest { Id = 7, FirstName = "Elijah", LastName = "Wilson", Email = "elijah.wilson@email.com", PhoneNumber = "+46-70-1000107" },
                new Guest { Id = 8, FirstName = "Ava", LastName = "Taylor", Email = "ava.taylor@email.com", PhoneNumber = "+46-70-1000108" },
                new Guest { Id = 9, FirstName = "Lucas", LastName = "Thomas", Email = "lucas.thomas@email.com", PhoneNumber = "+49-151-10000109" },
                new Guest { Id = 10, FirstName = "Isabella", LastName = "Moore", Email = "isabella.moore@email.com", PhoneNumber = "+49-151-10000110" }
            );

            // ── Booking ───────────────────────────────────────────────────────────────
            modelBuilder.Entity<Booking>().HasData(
                new Booking { Id = 1, CreatedAt = new DateTime(2025, 10, 1, 9, 15, 0), Status = Status.Confirmed, TotalPrice = 267.00m, GuestId = 1 },
                new Booking { Id = 2, CreatedAt = new DateTime(2025, 10, 3, 11, 30, 0), Status = Status.Confirmed, TotalPrice = 447.00m, GuestId = 2 },
                new Booking { Id = 3, CreatedAt = new DateTime(2025, 10, 5, 14, 0, 0), Status = Status.Cancelled, TotalPrice = 298.00m, GuestId = 3 },
                new Booking { Id = 4, CreatedAt = new DateTime(2025, 10, 7, 8, 45, 0), Status = Status.Confirmed, TotalPrice = 716.00m, GuestId = 4 },
                new Booking { Id = 5, CreatedAt = new DateTime(2025, 10, 10, 16, 20, 0), Status = Status.Preliminary, TotalPrice = 218.00m, GuestId = 5 },
                new Booking { Id = 6, CreatedAt = new DateTime(2025, 10, 12, 10, 0, 0), Status = Status.Confirmed, TotalPrice = 2443.00m, GuestId = 6 },
                new Booking { Id = 7, CreatedAt = new DateTime(2025, 10, 14, 13, 35, 0), Status = Status.Confirmed, TotalPrice = 447.00m, GuestId = 7 },
                new Booking { Id = 8, CreatedAt = new DateTime(2025, 10, 16, 9, 0, 0), Status = Status.Preliminary, TotalPrice = 298.00m, GuestId = 8 },
                new Booking { Id = 9, CreatedAt = new DateTime(2025, 10, 18, 15, 10, 0), Status = Status.Confirmed, TotalPrice = 1794.00m, GuestId = 9 },
                new Booking { Id = 10, CreatedAt = new DateTime(2025, 10, 20, 11, 55, 0), Status = Status.Confirmed, TotalPrice = 698.00m, GuestId = 10 }
            );

            // ── RoomReservation ───────────────────────────────────────────────────────
            modelBuilder.Entity<RoomReservation>().HasData(
                new RoomReservation { Id = 1, RoomId = 1, BookingId = 1, CheckIn = new DateTime(2025, 11, 10, 14, 0, 0), CheckOut = new DateTime(2025, 11, 13, 11, 0, 0), RoomStatus = RoomStatus.Confirmed, Adults = 1, Children = 0, PricePerNight = 89.00m },
                new RoomReservation { Id = 2, RoomId = 5, BookingId = 2, CheckIn = new DateTime(2025, 11, 15, 14, 0, 0), CheckOut = new DateTime(2025, 11, 18, 11, 0, 0), RoomStatus = RoomStatus.Confirmed, Adults = 2, Children = 1, PricePerNight = 149.00m },
                new RoomReservation { Id = 3, RoomId = 3, BookingId = 3, CheckIn = new DateTime(2025, 11, 20, 14, 0, 0), CheckOut = new DateTime(2025, 11, 22, 11, 0, 0), RoomStatus = RoomStatus.Cancelled, Adults = 2, Children = 0, PricePerNight = 149.00m },
                new RoomReservation { Id = 4, RoomId = 7, BookingId = 4, CheckIn = new DateTime(2025, 12, 1, 14, 0, 0), CheckOut = new DateTime(2025, 12, 5, 11, 0, 0), RoomStatus = RoomStatus.Confirmed, Adults = 2, Children = 2, PricePerNight = 179.00m },
                new RoomReservation { Id = 5, RoomId = 2, BookingId = 5, CheckIn = new DateTime(2025, 12, 10, 14, 0, 0), CheckOut = new DateTime(2025, 12, 12, 11, 0, 0), RoomStatus = RoomStatus.Preliminary, Adults = 1, Children = 0, PricePerNight = 109.00m },
                new RoomReservation { Id = 6, RoomId = 9, BookingId = 6, CheckIn = new DateTime(2025, 12, 20, 14, 0, 0), CheckOut = new DateTime(2025, 12, 27, 11, 0, 0), RoomStatus = RoomStatus.Confirmed, Adults = 2, Children = 1, PricePerNight = 349.00m },
                new RoomReservation { Id = 7, RoomId = 4, BookingId = 7, CheckIn = new DateTime(2026, 1, 5, 14, 0, 0), CheckOut = new DateTime(2026, 1, 8, 11, 0, 0), RoomStatus = RoomStatus.Confirmed, Adults = 2, Children = 0, PricePerNight = 149.00m },
                new RoomReservation { Id = 8, RoomId = 6, BookingId = 8, CheckIn = new DateTime(2026, 1, 15, 14, 0, 0), CheckOut = new DateTime(2026, 1, 17, 11, 0, 0), RoomStatus = RoomStatus.Preliminary, Adults = 2, Children = 1, PricePerNight = 149.00m },
                new RoomReservation { Id = 9, RoomId = 10, BookingId = 9, CheckIn = new DateTime(2026, 2, 1, 14, 0, 0), CheckOut = new DateTime(2026, 2, 7, 11, 0, 0), RoomStatus = RoomStatus.Confirmed, Adults = 2, Children = 2, PricePerNight = 299.00m },
                new RoomReservation { Id = 10, RoomId = 8, BookingId = 10, CheckIn = new DateTime(2026, 2, 14, 14, 0, 0), CheckOut = new DateTime(2026, 2, 16, 11, 0, 0), RoomStatus = RoomStatus.Confirmed, Adults = 2, Children = 0, PricePerNight = 349.00m }
            );
        }
    }
}
