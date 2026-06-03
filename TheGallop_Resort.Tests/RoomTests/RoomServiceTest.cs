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

namespace TheGallop_Resort.Tests.RoomTests
{
    [TestClass]
    public class RoomServiceTest
    {
        private GaloppDbContext _ctx;
        private RoomService _roomService;


        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<GaloppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _ctx = new GaloppDbContext(options);

            _roomService = new RoomService(_ctx);
        }

        [TestMethod]
        public async Task GetAllRoomsAsync_GetAllRooms_ReturnListOfRooms()
        {
            var category = new RoomCategory
            {
                Id = 1,
                Type = RoomType.SingleBed,
                CategoryPrice = 500
            };

            _ctx.RoomCategories.Add(category);

            var room1 = new Room
            {
                Id = 1,
                RoomNr = 1004,
                RoomCategoryId = category.Id
            };

            var room2 = new Room
            {
                Id = 2,
                RoomNr = 1006,
                RoomCategoryId = category.Id
            };

            _ctx.Rooms.AddRange(room1, room2);

            await _ctx.SaveChangesAsync();

            var result = await _roomService.GetAllRoomsAsync();

            result.SuccessfulResult.Should().BeTrue();

            result.Data.Should().HaveCount(2);
        }
    }
}
