using FluentValidation;

namespace TheGallop_Resort.Api.DTOs.Validators
{
    public class CreateBookingDTOValidator : AbstractValidator<GetInputFromUserCreateDTO>
    {
        public CreateBookingDTOValidator()
        {
            RuleFor(x => x.CheckOut)
                .GreaterThan(x => x.CheckIn)
                .WithMessage("Check-out date must be after the check-in date.");

            RuleFor(x => x.CheckIn)
                .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now))
                .WithMessage("Check-in date can not be in the past.");
            
        }
    }
}
