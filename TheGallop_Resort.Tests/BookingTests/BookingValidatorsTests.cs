using FluentValidation.TestHelper;
using TheGallop_Resort.Api.DTOs;
using TheGallop_Resort.Api.DTOs.Validators;
using TheGallop_Resort.Models.Models;

namespace TheGallop_Resort.Tests;

[TestClass]
public class BookingValidatorsTests
{
    [TestMethod]
    public void UpdateGuestOnBooking_NegativeGuestId_ReturnError()
    {
        var validator = new UpdateBookingGuestDTOValidator();

        var booking = new UpdateBookingGuestDTO(3, -3);

        var result = validator.TestValidate(booking);

        result.ShouldHaveValidationErrorFor(x => x.guestId);
    }

    [TestMethod]
    public void UpdateGuestOnBooking_NegativeBookingId_ReturnError()
    {
        var validator = new UpdateBookingGuestDTOValidator();

        var booking = new UpdateBookingGuestDTO(-1, 3);

        var result = validator.TestValidate(booking);

        result.ShouldHaveValidationErrorFor(x => x.bookingId);
    }

    [TestMethod]
    public void UpdateBookingStatus_AnotherStatusThanEnum_ReturnError()
    {
        var validator = new UpdateBookingStatusDTOValidator();
        var booking = new UpdateBookingStatusDTO(BookingId: 1, Status: (Status)99);

        var result = validator.TestValidate(booking);

        result.ShouldHaveValidationErrorFor(x => x.Status);
    }

    [TestMethod]
    public void UpdateBookingStatus_NegativeBookingId_ReturnError()
    {
        var validator = new UpdateBookingStatusDTOValidator();
        var booking = new UpdateBookingStatusDTO(-1, Status.Confirmed);

        var result = validator.TestValidate(booking);

        result.ShouldHaveValidationErrorFor(x => x.BookingId);
    }

    [TestMethod]
    public void CreateBooking_CheckinCanNotBeBeforeCheckOut_ReturnError()
    {
        var validator = new CreateBookingDTOValidator();
        var booking = new GetInputFromUserCreateDTO
        {
            GuestId = 1,
            CheckIn = new DateOnly(2026, 07, 10),
            CheckOut = new DateOnly(2026, 07, 05),
            Adults = 2,
            Children = 0,
            Type = RoomType.Suite
        };

        var result = validator.TestValidate(booking);
        result.ShouldHaveValidationErrorFor(x => x.CheckOut);
    }

    [TestMethod]
    public void CreateBooking_CheckInDateInPast_RetornError()
    {
        var validator = new CreateBookingDTOValidator();
        var booking = new GetInputFromUserCreateDTO
        {
            GuestId = 1,
            CheckIn = new DateOnly(2026, 04, 10),
            CheckOut = new DateOnly(2026, 07, 05),
            Adults = 2,
            Children = 0,
            Type = RoomType.Suite
        };

        var result = validator.TestValidate(booking);
        result.ShouldHaveValidationErrorFor(x => x.CheckIn);
    }

    [TestMethod]
    public void CreateBooking_MinimalOneAdult_RetornError()
    {
        var validator = new CreateBookingDTOValidator();
        var booking = new GetInputFromUserCreateDTO
        {
            GuestId = 1,
            CheckIn = new DateOnly(2026, 08, 01),
            CheckOut = new DateOnly(2026, 08, 05),
            Adults = 0,
            Children = 1,
            Type = RoomType.SingleBed
        };

        var result = validator.TestValidate(booking);
        result.ShouldHaveValidationErrorFor(x => x.Adults);
    }

    [TestMethod]
    public void CreateBooking_NegativeNumberOfChildren_ReturnError()
    {
        var validator = new CreateBookingDTOValidator();
        var booking = new GetInputFromUserCreateDTO
        {
            GuestId = 1,
            CheckIn = new DateOnly(2026, 08, 01),
            CheckOut = new DateOnly(2026, 08, 05),
            Adults = 1,
            Children = -3,
            Type = RoomType.SingleBed
        };

        var result = validator.TestValidate(booking);
        result.ShouldHaveValidationErrorFor(x => x.Children);
    }
}
