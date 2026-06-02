using FakeItEasy;
using FluentAssertions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGallop_Resort.Api.Controllers;
using TheGallop_Resort.Api.DTOs;
using TheGallop_Resort.Api.Services;
using TheGallop_Resort.Models.Models;

namespace TheGallop_Resort.Tests.RoomReservationTests
{
    [TestClass]
    public class RoomReservationControllerTest
    {
        private IRoomReservationService _fakeRoomReservationService;
        private IValidator<CreateRoomReservationDTO> _createRoomReservationDTO;

        [TestInitialize]
        public void Setup()
        {
            _fakeRoomReservationService = A.Fake<IRoomReservationService>();
            _createRoomReservationDTO = A.Fake<IValidator<CreateRoomReservationDTO>>();
        }

        [TestMethod]
        public async Task CreateRoomReservation_AddValidRoomReservation_ReturnOk()
        {

            var controller = new RoomReservationController(_fakeRoomReservationService, _createRoomReservationDTO);

            var testRoomReservation = new CreateRoomReservationDTO(bookingId: 1, CheckIn: new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddMonths(1).AddDays(5), CheckOut: new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddMonths(1).AddDays(10), Children: 1, Adults: 2, Type: RoomType.Suite);

            var fakeResponse = new GetFullBookingResponsDTO { Id = 99, GuestId = 1, Status = Status.Confirmed, TotalPrice = 5000, CreatedAt = DateTime.Now, RoomReservations = new List<GetFullRoomReservationResponse>() };

            A.CallTo(() => _createRoomReservationDTO.ValidateAsync(testRoomReservation, default))
                .Returns(Task.FromResult(new FluentValidation.Results.ValidationResult()));

            A.CallTo(() => _fakeRoomReservationService.CreateRoomReservationAsync(testRoomReservation))
                .Returns(ServiceResult<GetFullBookingResponsDTO>.Ok(fakeResponse));

            var result = await controller.CreateRoomReservation(testRoomReservation);

            var okResult = result.Result
                .Should()
                .BeAssignableTo<OkObjectResult>()
                .Subject;

            var serviceResult = okResult.Value
                .Should()
                .BeAssignableTo<GetFullBookingResponsDTO>()
                .Subject;

            serviceResult.Should().NotBeNull();
        }
    }
}
