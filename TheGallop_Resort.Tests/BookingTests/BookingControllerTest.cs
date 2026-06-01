using Microsoft.AspNetCore.Mvc;
using Moq;
using TheGallop_Resort.Api.Controllers;
using TheGallop_Resort.Api.DTOs;
using TheGallop_Resort.Api.DTOs.Validators;
using TheGallop_Resort.Api.Services;
using TheGallop_Resort.Models.Models;
using Xunit;

namespace TheGallop_Resort.Tests
{
    public class RoomCategoryControllerTests
    {
        [Fact]
        public async Task AddRoomCategory_ReturnsOk_WhenServiceSucceeds()
        {
            // Arrange
            var dto = new RoomCategoryDTO
            {
                Type = RoomType.DoubleBed,
                CategoryPrice = 1800,
                RoomDetailId = 1
            };

            var createdRoomCategory = new RoomCategory
            {
                Id = 1,
                Type = RoomType.DoubleBed,
                CategoryPrice = 1800,
                RoomDetailId = 1
            };

            var serviceMock = new Mock<IRoomCategoryService>();

            serviceMock
                .Setup(s => s.AddRoomCategoryAsync(dto))
                .ReturnsAsync(ServiceResult<RoomCategory>.Ok(createdRoomCategory));

            var controller = new RoomCategoryController(serviceMock.Object);

            // Act
            var result = await controller.AddRoomCategory(dto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedRoomCategory = Assert.IsType<RoomCategory>(okResult.Value);

        okResult.Should().NotBeNull();
    }

    [TestMethod]
    public async Task UpdateBookingStatus_UpdateValidBookingStatus_ReturnOk()
    {
        var controller = new BookingController(_fakeBookingService, _updateStatusValidator, _updateGuestValidator, _getInputFromUserCreateDTO, _searchBookingBetweenDateDTO);

        var updatedDTO = new UpdateBookingStatusDTO(BookingId: 1, Status: Status.Cancelled);

        A.CallTo(() => _updateStatusValidator.ValidateAsync(updatedDTO, default))
            .Returns(new ValidationResult());

        A.CallTo(() => _fakeBookingService.UpdateBookingStatusAsync(updatedDTO))
            .Returns(ServiceResult.Ok());

        var result = await controller.UpdateBookingStatus(updatedDTO);

        var noContentResult = result
            .Should()
            .BeAssignableTo<NoContentResult>()
            .Subject;

        noContentResult.Should().NotBeNull();
    }

    [TestMethod]
    public async Task GetBookingsForNextMonth_GetCorrectBookings_ReturnBookings()
    {

        var controller = new BookingController(_fakeBookingService, _updateStatusValidator, _updateGuestValidator, _getInputFromUserCreateDTO, _searchBookingBetweenDateDTO);

        var bookings = new List<GetBookingResponseDTO>();

        var booking = new GetBookingResponseDTO
        {
            Id = 1,
            TotalPrice = 2000,
            Status = Status.Confirmed,
            Guest = new GuestInfoDTO("Test", "Testsson", "test@mail.com", "0700000000"),
            RoomReservation = new List<GetRoomReservationResponseDTO>()
        };

        bookings.Add(booking);


        A.CallTo(() => _fakeBookingService.GetBookingsForNextMonthAsync())
            .Returns(ServiceResult<IEnumerable<GetBookingResponseDTO>>.Ok(bookings));

        var result = await controller.GetBookingsForNextMonth();

        var okResult = result.Result
            .Should()
            .BeAssignableTo<OkObjectResult>()
            .Subject;

        var serviceResult = okResult.Value
            .Should()
            .BeAssignableTo<IEnumerable<GetBookingResponseDTO>>()
            .Subject;

        serviceResult.Should().NotBeNull();
        serviceResult.Should().HaveCount(1);
    }

    [TestMethod]
    public async Task GetBookingsForSpecifikDate_GetCorrectBookings_ReturnBookings()
    {

        var controller = new BookingController(_fakeBookingService, _updateStatusValidator, _updateGuestValidator, _getInputFromUserCreateDTO, _searchBookingBetweenDateDTO);

        var bookings = new List<GetBookingResponseDTO>();

        var booking = new GetBookingResponseDTO
        {
            Id = 1,
            TotalPrice = 2000,
            Status = Status.Confirmed,
            Guest = new GuestInfoDTO("Test", "Testsson", "test@mail.com", "0700000000"),
            RoomReservation = new List<GetRoomReservationResponseDTO>()
        };

        bookings.Add(booking);

        DateOnly date = new DateOnly(2026, 09, 20);

        A.CallTo(() => _fakeBookingService.GetBookingsForSpecifikDateAsync(date))
            .Returns(ServiceResult<IEnumerable<GetBookingResponseDTO>>.Ok(bookings));

        var result = await controller.GetBookingsForSpecifikDate(date);

        var okResult = result.Result
            .Should()
            .BeAssignableTo<OkObjectResult>()
            .Subject;

        var serviceResult = okResult.Value
            .Should()
            .BeAssignableTo<IEnumerable<GetBookingResponseDTO>>()
            .Subject;

        serviceResult.Should().NotBeNull();
        serviceResult.Should().HaveCount(1);
    }

    [TestMethod]
    public async Task GetBookingsBetweenDates_ValidDate_ReturnOk()
    {
        var controller = new BookingController(_fakeBookingService, _updateStatusValidator, _updateGuestValidator, _getInputFromUserCreateDTO, _searchBookingBetweenDateDTO);

        var startDate = new DateOnly(2026, 09, 20);
        var endDate = new DateOnly(2026, 09, 29);

        var updatedDTO = new SearchBookingBetweenDateDTO(startDate, endDate);

        var bookings = new List<GetBookingResponseDTO>();

        var booking = new GetBookingResponseDTO
        {
            Id = 1,
            TotalPrice = 2000,
            Status = Status.Confirmed,
            Guest = new GuestInfoDTO("Test", "Testsson", "test@mail.com", "0700000000"),
            RoomReservation = new List<GetRoomReservationResponseDTO>()
        };

        bookings.Add(booking);

        A.CallTo(() => _searchBookingBetweenDateDTO.ValidateAsync(updatedDTO, default))
            .Returns(new ValidationResult());

        A.CallTo(() => _fakeBookingService.GetBookingsBetweenDatesAsync(updatedDTO))
            .Returns(ServiceResult<IEnumerable<GetBookingResponseDTO>>.Ok(bookings));

        var result = await controller.GetBookingsBetweenDates(updatedDTO);

        var okResult = result.Result
             .Should()
             .BeAssignableTo<OkObjectResult>()
             .Subject;

        var serviceResult = okResult.Value
            .Should()
            .BeAssignableTo<IEnumerable<GetBookingResponseDTO>>()
            .Subject;

        serviceResult.Should().NotBeNull();
        serviceResult.Should().HaveCount(1);
    }

    [TestMethod]
    public async Task DeleteBookingById_ValidId_ReturnNoContent()
    {
        var controller = new BookingController(_fakeBookingService, _updateStatusValidator, _updateGuestValidator, _getInputFromUserCreateDTO, _searchBookingBetweenDateDTO);

        var bookingId = 1;

        A.CallTo(() => _fakeBookingService.DeleteBookingByIdAsync(bookingId))
            .Returns(ServiceResult.Ok());

        var result = await controller.DeleteBookingById(bookingId);

        var noContentResult = result
            .Should()
            .BeAssignableTo<NoContentResult>()
            .Subject;

        noContentResult.Should().NotBeNull();
    }

    [TestMethod]
    public async Task GetBookingsBetweenDates_InvalidDate_ShouldReturnBadRequest()
    {
        var controller = new BookingController(_fakeBookingService, _updateStatusValidator, _updateGuestValidator, _getInputFromUserCreateDTO, _searchBookingBetweenDateDTO);
        var invalidDTO = new SearchBookingBetweenDateDTO(new DateOnly(2026, 09, 29), new DateOnly(2026, 09, 20));

        var validator = new SearchBookingBetweenDateDTOValidator();
        var failedResult = validator.Validate(invalidDTO);

        A.CallTo(() => _searchBookingBetweenDateDTO.ValidateAsync(invalidDTO, default))
             .Returns(failedResult);

        var result = await controller.GetBookingsBetweenDates(invalidDTO);

        result.Result.Should().BeAssignableTo<BadRequestObjectResult>();

        A.CallTo(() => _fakeBookingService.GetBookingsBetweenDatesAsync(invalidDTO))
            .MustNotHaveHappened();
    }
}
