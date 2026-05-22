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

        var booking = new UpdateBookingGuestDTO(-1,  3);

        var result = validator.TestValidate(booking);

        result.ShouldHaveValidationErrorFor(x => x.bookingId);
    }

    [TestMethod]
    public void UpdateBookingStatus_AnotherStatusThanEnum_ReturnError()
    {
        var validator = new UpdateBookingStatusDTOValidator();
        var booking = new UpdateBookingStatusDTO(BookingId : 1, Status: (Status)99);

        var result = validator.TestValidate(booking);

        result.ShouldHaveValidationErrorFor(x => x.Status);
    }


}
