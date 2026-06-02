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

namespace TheGallop_Resort.Tests.RoomReservationTests
{
    [TestClass]
    public class RoomReservationServiceTest
    {
        private GaloppDbContext _ctx;
        private RoomReservationService _roomReservationService;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<GaloppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _ctx = new GaloppDbContext(options);

            _roomReservationService = new RoomReservationService(_ctx);
        }

        [TestMethod]
        public async Task CreateRoomReservationAsync_AddRoomReservationToExistingBooking_ReturnOK()
        {
            var roomCategory = new RoomCategory
            {
                Id = 1,
                Type = RoomType.Suite,
                CategoryPrice = 1500
            };

            var room = new Room
            {
                Id = 10,
                RoomNr = 101,
                RoomCategoryId = roomCategory.Id,
                RoomCategory = roomCategory,
                RoomReservations = new List<RoomReservation>()
            };

            var booking = new Booking
            {
                Id = 1,
                GuestId = 5,
                TotalPrice = 0,
                Status = Status.Confirmed,
                CreatedAt = DateTime.Now,
                RoomReservations = new List<RoomReservation>()
            };

            await _ctx.RoomCategories.AddAsync(roomCategory);
            await _ctx.Rooms.AddAsync(room);
            await _ctx.Bookings.AddAsync(booking);
            await _ctx.SaveChangesAsync();

            var inputDTO = new CreateRoomReservationDTO
                (bookingId: 1,
                CheckIn: new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddMonths(1).AddDays(5),
                CheckOut: new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddMonths(1).AddDays(10),
                Children: 1,
                Adults: 2,
                Type: RoomType.Suite
                );

            var result = await _roomReservationService.CreateRoomReservationAsync(inputDTO);

            result.SuccessfulResult.Should().BeTrue();
            result.Data.Id.Should().Be(booking.Id);

            var savedReservation = await _ctx.RoomReservations.FirstOrDefaultAsync(rr => rr.BookingId == booking.Id);
            savedReservation.Should().NotBeNull();
            savedReservation!.RoomId.Should().Be(room.Id);
            savedReservation.Adults.Should().Be(inputDTO.Adults);

        }

    }
}
