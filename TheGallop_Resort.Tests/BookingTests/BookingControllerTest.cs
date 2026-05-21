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
                ("Anna",
                "Andersson",
                "anna@mail.com", 
                "0701234567"),
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
            Guest = new GuestInfoDTO("Olle", "Persson", "olle@mail.com", "0709998877"),
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
}
