using FluentValidation;

namespace TheGallop_Resort.Api.DTOs.Validators
{
    public class CreateGuestDTOValidator: AbstractValidator<CreateGuestDTO>
    {

        public CreateGuestDTOValidator()
        {
            RuleFor(g => g.FirstName)
                .NotEmpty().MinimumLength(2)
                .MaximumLength(50)
                .WithMessage("Invalid numver of Characters");


            RuleFor(g => g.LastName).NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50)
                .WithMessage("Invalid numbers of characers");

            RuleFor(g => g.Email).NotEmpty().EmailAddress();

            RuleFor(g => g.Phone)
            .NotEmpty()
            .Matches(@"^\+?[\d\s\-\(\)]{7,20}$")
            .WithMessage("Invalid phone number format");



        }


    }
}
