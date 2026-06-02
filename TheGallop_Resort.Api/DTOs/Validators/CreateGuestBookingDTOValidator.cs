using FluentValidation;

namespace TheGallop_Resort.Api.DTOs.Validators
{
    public class CreateGuestBookingDTOValidator : AbstractValidator<CreateGuestBookingDTO>
    {
        public CreateGuestBookingDTOValidator(IValidator<CreateGuestDTO> guestValidator)
        {

            

            RuleFor(x => x.GuestInfo).NotNull().SetValidator(guestValidator);

            RuleFor(x => x.Reservation.CheckOut)
                .GreaterThan(x => x.Reservation.CheckIn)
                .WithMessage("Check-out date must be after the check-in date.");

            RuleFor(x => x.Reservation.CheckIn)
                    .GreaterThanOrEqualTo(DateTime.Today)
                    .WithMessage("Check-in date can not be in the past.");

            RuleFor(x => x.Reservation.Adults)
                .GreaterThan(0)
                .WithMessage("There must be at least one adult in the booking.");

            RuleFor(x => x.Reservation.Children)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Children count cannot be negative.");

        }
    }
}
