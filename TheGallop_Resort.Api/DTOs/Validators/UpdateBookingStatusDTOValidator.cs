using FluentValidation;

namespace TheGallop_Resort.Api.DTOs.Validators
{
    public class UpdateBookingStatusDTOValidator : AbstractValidator<UpdateBookingStatusDTO>
    {
        public UpdateBookingStatusDTOValidator()
        {
            RuleFor(b => b.BookingId)
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage("BookingId can't be negative");

            RuleFor(b => b.Status)
                .IsInEnum()
                .WithMessage("Invalid booking status");
        }
    }
}
