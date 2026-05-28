using FluentValidation.TestHelper;
using TheGallop_Resort.Api.DTOs;
using TheGallop_Resort.Api.DTOs.Validators;

namespace TheGallop_Resort.Tests;

[TestClass]
public class GuestValidatorsTests
{
    [DataTestMethod]
    [DataRow("test")]
    [DataRow("testtestsson.se")]
    [DataRow("@test.se")]
    [DataRow("")]
    [DataRow(null)]
    public void Validate_InvalidEmail_ReturnError(string invalidEmail)
    {
        var validator = new CreateGuestDTOValidator();
       

        var dto = new CreateGuestDTO()
        {
            FirstName = "Test",
            LastName = "Testsson",
            Email = invalidEmail,
            Phone = "0727435550"
        };

        var result = validator.TestValidate(dto);

        result.ShouldHaveValidationErrorFor(g => g.Email);
    }

    [DataTestMethod]
    [DataRow("erik.nyy@gmail.com")]
    [DataRow("erik@gmail.com")]
    [DataRow("erik.nyy@gmail.se")]
    [DataRow("erik.123@hotmail.com")]
    [DataRow("erik.123@hotmail.com")]

    public void Validate_ValidEmail_ReturnOk(string validEmail)
    {
        var validator = new CreateGuestDTOValidator();

        var dto = new CreateGuestDTO
        {
            FirstName = "Test",
            LastName = "Testsson",
            Email = validEmail,
            Phone = "07262534261"
        };

        var result = validator.TestValidate(dto);

        result.ShouldNotHaveAnyValidationErrors();
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
