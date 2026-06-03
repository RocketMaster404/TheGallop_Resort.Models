using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGallop_Resort.Api.Controllers;
using TheGallop_Resort.Api.DTOs;
using TheGallop_Resort.Api.Services;
using TheGallop_Resort.Models.Models;

namespace TheGallop_Resort.Tests.RoomTests
{
    [TestClass]
    public class RoomControllerTest
    {
        private IRoomService _fakeRoomService;

        [TestInitialize]
        public void Setup()
        {
            _fakeRoomService = A.Fake<IRoomService>();
        }

        [TestMethod]
        public async Task GetAllRooms_GetAllRooms_ReturnListOfRooms()
        {
            var controller = new RoomController(_fakeRoomService);
            var rooms = new List<GetRoomResponseDTO>();

            var room1 = new GetRoomResponseDTO
            (
                1,
                1005,
                RoomType.DoubleBed
            );

            var room2 = new GetRoomResponseDTO
            (
                2,
                1006,
                RoomType.DoubleBed
            );

            rooms.Add(room1);
            rooms.Add(room2);

            A.CallTo(() => _fakeRoomService.GetAllRoomsAsync())
                .Returns(ServiceResult<IEnumerable<GetRoomResponseDTO>>.Ok(rooms));

            var result = await controller.GetAllRooms();

            var okResult = result.Result
                .Should()
                .BeAssignableTo<OkObjectResult>()
                .Subject;

            var serviceResult = okResult.Value
                .Should()
                .BeAssignableTo<IEnumerable<GetRoomResponseDTO>>()
                .Subject;

            serviceResult.Should().NotBeNull();
        }
    }
}
