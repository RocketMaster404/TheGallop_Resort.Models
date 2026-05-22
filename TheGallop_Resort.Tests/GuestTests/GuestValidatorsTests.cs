using FluentValidation.TestHelper;
using TheGallop_Resort.Api.DTOs;
using TheGallop_Resort.Api.DTOs.Validators;

namespace TheGallop_Resort.Tests;

[TestClass]
public class GuestValidatorsTests
{
    [TestMethod]
    public void Validate_InvalidEmail_ReturnError()
    {
        var validator = new CreateGuestDTOValidator();

        var dto = new CreateGuestDTO()
        {
            FirstName = "Test",
            LastName = "Testsson",
            Email = "Testtestsson.se",
            Phone = "0727435550"
        };

        var result = validator.TestValidate(dto);

        result.ShouldHaveValidationErrorFor(g => g.Email);
    }

    [TestMethod]
    public void Validate_InvalidPhone_ReturnError()
    {
        var validator = new CreateGuestDTOValidator();

        var dto = new CreateGuestDTO()
        {
            FirstName = "Test",
            LastName = "Testsson",
            Email = "Test@testsson.se",
            Phone = "FElaktigt nummer"
        };

        var result = validator.TestValidate(dto);

        result.ShouldHaveValidationErrorFor(g => g.Phone);
    }

    [TestMethod]
    public void Validate_InvalidFirstName_ReturnError()
    {
        var validator = new CreateGuestDTOValidator();

        var dto = new CreateGuestDTO()
        {
            FirstName = "E",
            LastName = "Testsson",
            Email = "Test@testsson.se",
            Phone = "0727435550"
        };

        var result = validator.TestValidate(dto);

        result.ShouldHaveValidationErrorFor(g => g.FirstName);
    }

    [TestMethod]
    public void Validate_InvalidLastName_ReturnError()
    {
        var validator = new CreateGuestDTOValidator();

        var dto = new CreateGuestDTO()
        {
            FirstName = "Test",
            LastName = "T",
            Email = "Test@testsson.se",
            Phone = "0727435550"
        };

        var result = validator.TestValidate(dto);

        result.ShouldHaveValidationErrorFor(g => g.LastName);
    }
}
