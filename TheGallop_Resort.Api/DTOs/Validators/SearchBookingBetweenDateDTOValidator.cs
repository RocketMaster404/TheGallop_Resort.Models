using FluentValidation;

namespace TheGallop_Resort.Api.DTOs.Validators
{
    public class SearchBookingBetweenDateDTOValidator : AbstractValidator<SearchBookingBetweenDateDTO>
    {
        public SearchBookingBetweenDateDTOValidator()
        {
            RuleFor(x => x.StartDate)
                .NotEmpty()
                .WithMessage("Start date must be provided");

            RuleFor(x => x.EndDate)
                .GreaterThan(x => x.StartDate)
                .NotEmpty()
                .WithMessage("End date cannot be before the start date.");
        }
    }
}
