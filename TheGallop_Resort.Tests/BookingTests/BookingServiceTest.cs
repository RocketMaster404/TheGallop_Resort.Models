using FakeItEasy;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGallop_Resort.Api.Data;
using TheGallop_Resort.Api.DTOs;
using TheGallop_Resort.Api.Services;
using TheGallop_Resort.Models.Models;

namespace TheGallop_Resort.Tests.BookingTests
{
    [TestClass]
    public class BookingServiceTest
    {
        private GaloppDbContext _ctx;
        private BookingService _bookingService;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<GaloppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _ctx = new GaloppDbContext(options);



            _bookingService = new BookingService(_ctx);

        }

        [TestMethod]
        public async Task GetAllBookingsAsync_CheckIfBookingsExist_ReturnBookings()
        {
            var guest = new Guest
            {
                Id = 1,
                FirstName = "Test",
                LastName = "Testsson",
                Email = "test@test.com",
                PhoneNumber = "0765975412"
            };

            var booking1 = new Booking
            {
                Id = 1,
                Guest = guest,
                TotalPrice = 1200,
                Status = Status.Confirmed,
                CreatedAt = DateTime.Now
            };

            var booking2 = new Booking
            {
                Id = 2,
                Guest = guest,
                TotalPrice = 1300,
                Status = Status.Preliminary,
                CreatedAt = DateTime.Now
            };

            _ctx.Bookings.AddRange(booking1, booking2);

            await _ctx.SaveChangesAsync();

            var result = await _bookingService.GetAllBookingsAsync();

            result.SuccessfulResult.Should().BeTrue();

            result.Data.Should().HaveCount(2);
        }

        [TestMethod]
        public async Task GetBookingById_CheckIfIdExist_ReturnBooking()
        {
            var guest = new Guest
            {
                Id = 1,
                FirstName = "Test",
                LastName = "Testsson",
                Email = "test@test.com",
                PhoneNumber = "0765975412"
            };

            var booking = new Booking
            {
                Id = 3,
                Guest = guest,
                TotalPrice = 1200,
                Status = Status.Confirmed,
                CreatedAt = DateTime.Now
            };

            await _ctx.Bookings.AddAsync(booking);

            await _ctx.SaveChangesAsync();

            var result = await _bookingService.GetBookingByIdAsync(booking.Id);

            result.SuccessfulResult.Should().BeTrue();

            result.Data.Id.Should().Be(3);
        }

        [TestMethod]
        public async Task CreateBookingAsync_AddBookingToExistingGuest_ReturnOK()
        {

            var fakeRoomCategory = new RoomCategory
            {
                Id = 1,
                Type = RoomType.Suite,
                CategoryPrice = 1500
            };

            var fakeRoom = new Room
            {
                Id = 10,
                RoomCategoryId = 1,
                RoomCategory = fakeRoomCategory
            };

            await _ctx.Rooms.AddAsync(fakeRoom);
            await _ctx.RoomCategories.AddAsync(fakeRoomCategory);
            await _ctx.SaveChangesAsync();

            var inputDTO = new GetInputFromUserCreateDTO { GuestId = 1, CheckIn = new DateOnly(2026, 06, 28), CheckOut = new DateOnly(2026, 06, 29), Children = 1, Adults = 2, Type = RoomType.Suite };

            var result = await _bookingService.CreateBookingAsync(inputDTO);


            result.SuccessfulResult.Should().BeTrue();

            result.Data.TotalPrice.Should().Be(2500);

            result.Data.GuestId.Should().Be(inputDTO.GuestId);

            var count = await _ctx.Bookings.CountAsync();
            count.Should().Be(1);
        }

        [TestMethod]
        public async Task UpdateGuestOnBookingAsync_UpdatingGuestOnExistingBooking_NewGuestOnBooking()
        {
            var guest1 = new Guest
            {
                Id = 1,
                FirstName = "Test",
                LastName = "Testsson",
                Email = "test@test.com",
                PhoneNumber = "0765975412"
            };

            var guest2 = new Guest
            {
                Id = 2,
                FirstName = "NyTest",
                LastName = "NyTestsson",
                Email = "test@extra.com",
                PhoneNumber = "0789123419"
            };

            var booking = new Booking
            {
                Id = 1,
                Guest = guest1,
                TotalPrice = 1200,
                Status = Status.Confirmed,
                CreatedAt = DateTime.Now
            };

            await _ctx.Bookings.AddAsync(booking);
            await _ctx.Guests.AddRangeAsync(guest1, guest2);

            await _ctx.SaveChangesAsync();

            var updatedDto = new UpdateBookingGuestDTO(booking.Id, guest2.Id);

            var result = await _bookingService.UpdateGuestOnBookingAsync(updatedDto);

            result.SuccessfulResult.Should().BeTrue();

            var checkGuest = await _ctx.Bookings.FirstOrDefaultAsync();
            checkGuest.Guest.FirstName.Should().Be("NyTest");
        }

        [TestMethod]
        public async Task UpdateBookingStatusAsync_UpdateValidBooking_ReturnTrue()
        {
            var guest = new Guest
            {
                Id = 1,
                FirstName = "Test",
                LastName = "Testsson",
                Email = "test@test.com",
                PhoneNumber = "0765975412"
            };

            var booking = new Booking
            {
                Id = 3,
                Guest = guest,
                TotalPrice = 1200,
                Status = Status.Confirmed,
                CreatedAt = DateTime.Now
            };

            await _ctx.Bookings.AddAsync(booking);
            await _ctx.Guests.AddAsync(guest);

            await _ctx.SaveChangesAsync();

            var updatedDto = new UpdateBookingStatusDTO(booking.Id, Status.Cancelled);

            var result = await _bookingService.UpdateBookingStatusAsync(updatedDto);

            result.SuccessfulResult.Should().BeTrue();

            var checkStatus = await _ctx.Bookings.FirstOrDefaultAsync();
            checkStatus.Status.Should().Be(Status.Cancelled);
        }

        [TestMethod]
        public async Task GetBookingsForNextMonthAsync_FilterCorrectDates_ReturnOnlyNextMonth()
        {
            var today = DateTime.Now;
            var guest = new Guest
            {
                Id = 1,
                FirstName = "Test",
                LastName = "Testsson",
                Email = "test@test.com",
                PhoneNumber = "0765975412"
            };

            var correctBooking = new Booking
            {
                Id = 3,
                Guest = guest,
                TotalPrice = 1200,
                Status = Status.Confirmed,
                CreatedAt = today,
                RoomReservations = new List<RoomReservation> {
                    new RoomReservation
                    {
                        Id = 10,
                        CheckIn = new DateTime(today.Year, today.Month, 1).AddMonths(1).AddDays(5),
                        CheckOut = new DateTime(today.Year, today.Month, 1).AddMonths(1).AddDays(10),
                        RoomStatus = RoomStatus.Confirmed
                    }
                }
            };

            var wrongBooking = new Booking
            {
                Id = 11,
                Guest = guest,
                TotalPrice = 1300,
                Status = Status.Confirmed,
                CreatedAt = today,
                RoomReservations = new List<RoomReservation> {
                    new RoomReservation
                    {
                        Id = 12,
                        CheckIn = new DateTime(today.Year, today.Month, 1).AddMonths(3).AddDays(5),
                        CheckOut = new DateTime(today.Year, today.Month, 1).AddMonths(3).AddDays(10),
                        RoomStatus = RoomStatus.Confirmed
                    }
                }
            };

            await _ctx.Guests.AddAsync(guest);
            await _ctx.Bookings.AddRangeAsync(correctBooking, wrongBooking);
            await _ctx.SaveChangesAsync();

            var result = await _bookingService.GetBookingsForNextMonthAsync();

            result.SuccessfulResult.Should().BeTrue();
            result.Data.Should().HaveCount(1);
            result.Data.First().Id.Should().Be(3);
        }
    }
}
