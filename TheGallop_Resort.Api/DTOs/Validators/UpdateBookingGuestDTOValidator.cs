using FluentValidation;

namespace TheGallop_Resort.Api.DTOs.Validators
{
    public class UpdateBookingGuestDTOValidator : AbstractValidator<UpdateBookingGuestDTO>
    {
        public UpdateBookingGuestDTOValidator()
        {
            RuleFor(x => x.guestId)
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage("GuestId can't be negative");

            RuleFor(x => x.bookingId)
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage("BookingId can't be negative");
        }
    }
}
