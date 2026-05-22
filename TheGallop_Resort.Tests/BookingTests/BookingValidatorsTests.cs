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

        var booking = new UpdateBookingGuestDTO(bookingId: 3, guestId: -3);

        var result = validator.TestValidate(booking);

        result.ShouldHaveValidationErrorFor(x => x.guestId);
    }

    [TestMethod]
    public void UpdateGuestOnBooking_NegativeBookingId_ReturnError()
    {
        var validator = new UpdateBookingGuestDTOValidator();

        var booking = new UpdateBookingGuestDTO(bookingId: -1, guestId: 3);

        var result = validator.TestValidate(booking);

        result.ShouldHaveValidationErrorFor(x => x.bookingId);
    }
}
