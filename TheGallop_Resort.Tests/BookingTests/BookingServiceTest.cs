using FakeItEasy;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGallop_Resort.Api.Data;
using TheGallop_Resort.Api.Services;
using TheGallop_Resort.Models.Models;

namespace TheGallop_Resort.Tests.BookingTests
{
    [TestClass]
    public class BookingServiceTest
    {
        private GaloppDbContext _ctx;
        private BookingService _bookingService;
        private IGuestService _iGuestService;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<GaloppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _ctx = new GaloppDbContext(options);
            _iGuestService = A.Fake<IGuestService>();


            _bookingService = new BookingService(_ctx, _iGuestService);

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

            var booking = new Booking
            {
                Id = 1,
                Guest = guest,
                TotalPrice = 1200,
                Status = Status.Confirmed,
                CreatedAt = DateTime.Now
            };

            _ctx.Bookings.Add(booking);

            await _ctx.SaveChangesAsync();

            var result = await _bookingService.GetAllBookingsAsync();

            result.SuccessfulResult.Should().BeTrue();

            result.Status.Should().Be(ServiceResultStatus.Success);

            result.Data.Should().HaveCount(1);
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

            _ctx.Bookings.Add(booking);

            await _ctx.SaveChangesAsync();

            var result = await _bookingService.GetBookingByIdAsync(booking.Id);

            result.SuccessfulResult.Should().BeTrue();

            result.Status.Should().Be(ServiceResultStatus.Success);

            result.Data.Id.Should().Be(3);
        }
    }
}
