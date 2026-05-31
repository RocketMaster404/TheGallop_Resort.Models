using FakeItEasy;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using TheGallop_Resort.Api.Controllers;
using TheGallop_Resort.Api.DTOs;
using TheGallop_Resort.Api.Services;
using TheGallop_Resort.Models.Models;

namespace TheGallop_Resort.Tests;

[TestClass]
public class BookingControllerTest
{
    private IBookingService _fakeBookingService;
    private IValidator<UpdateBookingStatusDTO> _updateStatusValidator;
    private IValidator<UpdateBookingGuestDTO> _updateGuestValidator;
    private IValidator<GetInputFromUserCreateDTO> _getInputFromUserCreateDTO;

    [TestInitialize]
    public void Setup()
    {
        _fakeBookingService = A.Fake<IBookingService>();
        _updateStatusValidator = A.Fake<IValidator<UpdateBookingStatusDTO>>();
        _updateGuestValidator = A.Fake<IValidator<UpdateBookingGuestDTO>>();
        _getInputFromUserCreateDTO = A.Fake<IValidator<GetInputFromUserCreateDTO>>();
    }

    [TestMethod]
    public async Task GetBookingById_CheckIfIdExist_ReturnSpecificBooking()
    {

        var controller = new BookingController(_fakeBookingService, _updateStatusValidator, _updateGuestValidator, _getInputFromUserCreateDTO);

        var testId = 1;

        var booking = new GetBookingResponseDTO
        {
            Id = testId,
            CreatedAt = DateTime.Now,
            TotalPrice = 1000,
            Status = Status.Confirmed,
            Guest = new GuestInfoDTO
                ("Test",
                "Testssson",
                "test@mail.com",
                "0700000000"),
            RoomReservation = new List<GetRoomReservationResponseDTO>()
        };

        A.CallTo(() => _fakeBookingService.GetBookingByIdAsync(testId))
            .Returns(ServiceResult<GetBookingResponseDTO>.Ok(booking));

        //Act
        var result = await controller.GetBookingById(testId);

        //Assert
        var okResult = result.Result
            .Should()
            .BeAssignableTo<OkObjectResult>()
            .Subject;

        okResult.Should().NotBeNull();
    }

    [TestMethod]
    public async Task GetAllBookings_GetAllBookings_ReturnListOfBookings()
    {
        var controller = new BookingController(_fakeBookingService, _updateStatusValidator, _updateGuestValidator, _getInputFromUserCreateDTO);

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


        A.CallTo(() => _fakeBookingService.GetAllBookingsAsync())
            .Returns(ServiceResult<IEnumerable<GetBookingResponseDTO>>.Ok(bookings));

        var result = await controller.GetAllBookings();

        var okResult = result.Result
            .Should()
            .BeAssignableTo<OkObjectResult>()
            .Subject;

        var serviceResult = okResult.Value
            .Should()
            .BeAssignableTo<IEnumerable<GetBookingResponseDTO>>()
            .Subject;

        serviceResult.Should().NotBeNull();
    }

    [TestMethod]
    public async Task CreateBookingAsync_AddValidBooking_ReturnOk()
    {

        var controller = new BookingController(_fakeBookingService, _updateStatusValidator, _updateGuestValidator, _getInputFromUserCreateDTO);

        var testBooking = new GetInputFromUserCreateDTO { GuestId = 1, CheckIn = new DateOnly(2026, 09, 28), CheckOut = new DateOnly(2026, 09, 29), Children = 1, Adults = 2, Type = RoomType.Suite };

        var fakeResponse = new GetFullBookingResponsDTO { Id = 99, GuestId = 1, Status = Status.Confirmed, TotalPrice = 5000, CreatedAt = DateTime.Now, RoomReservations = new List<GetFullRoomReservationResponse>() };

        A.CallTo(() => _getInputFromUserCreateDTO.ValidateAsync(testBooking, default))
        .Returns(new ValidationResult());

        A.CallTo(() => _fakeBookingService.CreateBookingAsync(testBooking))
            .Returns(ServiceResult<GetFullBookingResponsDTO>.Ok(fakeResponse));

        var result = await controller.CreateBooking(testBooking);

        var okResult = result.Result
            .Should()
            .BeAssignableTo<OkObjectResult>()
            .Subject;

        var serviceResult = okResult.Value
            .Should()
            .BeAssignableTo<GetFullBookingResponsDTO>()
            .Subject;

        serviceResult.Should().NotBeNull();
        serviceResult.GuestId.Should().Be(testBooking.GuestId);
    }

    [TestMethod]
    public async Task UpdateGuestOnBookingAsync_UpdateValidGuestOnBooking_ReturnOk()
    {
        var controller = new BookingController(_fakeBookingService, _updateStatusValidator, _updateGuestValidator, _getInputFromUserCreateDTO);

        var updatedDTO = new UpdateBookingGuestDTO(bookingId: 1, guestId: 2);

        A.CallTo(() => _updateGuestValidator.ValidateAsync(updatedDTO, default))
            .Returns(new ValidationResult());

        A.CallTo(() => _fakeBookingService.UpdateGuestOnBookingAsync(updatedDTO))
            .Returns(ServiceResult.Ok());

        var result = await controller.UpdateGuestOnBooking(updatedDTO);

        var okResult = result
            .Should()
            .BeAssignableTo<NoContentResult>()
            .Subject;


        okResult.Should().NotBeNull();
    }

    [TestMethod]
    public async Task UpdateBookingStatus_UpdateValidBookingStatus_ReturnOk()
    {
        var controller = new BookingController(_fakeBookingService, _updateStatusValidator, _updateGuestValidator, _getInputFromUserCreateDTO);

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

        var controller = new BookingController(_fakeBookingService, _updateStatusValidator, _updateGuestValidator, _getInputFromUserCreateDTO);

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

        var controller = new BookingController(_fakeBookingService, _updateStatusValidator, _updateGuestValidator, _getInputFromUserCreateDTO);

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


}
