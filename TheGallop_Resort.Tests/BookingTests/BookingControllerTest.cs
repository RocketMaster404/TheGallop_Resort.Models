using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using TheGallop_Resort.Api.Controllers;
using TheGallop_Resort.Api.DTOs;
using TheGallop_Resort.Api.Services;
using TheGallop_Resort.Models.Models;

namespace TheGallop_Resort.Tests;

[TestClass]
public class BookingControllerTest
{
    [TestMethod]
    public async Task GetBookingById_CheckIfIdExist_ReturnSpecificBooking()
    {
        var fake = A.Fake<IBookingService>();

        var controller = new BookingController(fake);

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

        A.CallTo(() => fake.GetBookingByIdAsync(testId))
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
        var fake = A.Fake<IBookingService>();

        var controller = new BookingController(fake);

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


        A.CallTo(() => fake.GetAllBookingsAsync())
            .Returns(ServiceResult<IEnumerable<GetBookingResponseDTO>>.Ok(bookings));

        var result = await controller.GetAllBookings();

        var okResult = result.Result
            .Should()
            .BeAssignableTo<OkObjectResult>()
            .Subject;

        var serviceResult = okResult.Value
            .Should()
            .BeAssignableTo<ServiceResult<IEnumerable<GetBookingResponseDTO>>>()
            .Subject;

        serviceResult.Should().NotBeNull();
        serviceResult.Data.Should().HaveCount(1);
    }

    [TestMethod]
    public async Task AddBooking_AddValidBooking_ReturnOk()
    {
        var fake = A.Fake<IBookingService>();

        var controller = new BookingController(fake);

        var testGuestId = 1;

        var testBooking = new Booking { Id = 1, GuestId = testGuestId };

        A.CallTo(() => fake.AddBookingAsync(testGuestId))
            .Returns(ServiceResult<Booking>.Ok(testBooking));

        var result = await controller.AddBooking(testGuestId);

        var okResult = result.Result
            .Should()
            .BeAssignableTo<OkObjectResult>()
            .Subject;

        var serviceResult = okResult.Value
            .Should()
            .BeAssignableTo<ServiceResult<Booking>>()
            .Subject;

        serviceResult.Should().NotBeNull();
        serviceResult.Data.GuestId.Should().Be(testGuestId);
    }

    [TestMethod]
    public async Task UpdateGuestOnBookingAsync_UpdateValidGuestOnBooking_ReturnOk()
    {
        var fake = A.Fake<IBookingService>();

        var controller = new BookingController(fake);

        var updatedDTO = new UpdateBookingGuestDTO(bookingId: 1, guestId: 2);

        A.CallTo(() => fake.UpdateGuestOnBookingAsync(updatedDTO))
            .Returns(ServiceResult.Ok());

        var result = await controller.UpdateGuestOnBooking(updatedDTO);

        var okResult = result
            .Should()
            .BeAssignableTo<OkObjectResult>()
            .Subject;

        var serviceResult = okResult.Value
            .Should()
            .BeAssignableTo<ServiceResult>()
            .Subject;

        serviceResult.Should().NotBeNull();
        serviceResult.SuccessfulResult.Should().BeTrue();
    }
}
