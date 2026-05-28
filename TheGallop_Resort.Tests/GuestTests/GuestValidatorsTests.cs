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
    public void Validate_ValidEmail_ReturnNoError(string validEmail)
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

        result.ShouldNotHaveValidationErrorFor(g => g.Email);
    }

    [DataTestMethod]
    [DataRow("")]
    [DataRow(null)]
    [DataRow("FelNummer")]
    [DataRow("1")]
    [DataRow("0928sujdhen")]
    public void Validate_InvalidPhone_ReturnError(string invalidPhonenumber)
    {
        var validator = new CreateGuestDTOValidator();

        var dto = new CreateGuestDTO()
        {
            FirstName = "Test",
            LastName = "Testsson",
            Email = "Test@testsson.se",
            Phone = invalidPhonenumber
        };

        var result = validator.TestValidate(dto);

        result.ShouldHaveValidationErrorFor(g => g.Phone);
    }

    [DataTestMethod]
    [DataRow("+467283929")]
    [DataRow("+467-2839-29")]
    [DataRow("0340-16500")]
    [DataRow("078726537")]
    public void Validate_ValidPhoneNumber_ReturnNoError(string validPhonenumber)
    {
        var validator = new CreateGuestDTOValidator();

        var dto = new CreateGuestDTO
        {
            FirstName = "Test",
            LastName = "Testsson",
            Email = "test@testsson.com",
            Phone = validPhonenumber
        };

        var result = validator.TestValidate(dto);
        result.ShouldNotHaveValidationErrorFor(g => g.Phone);
    }
    

    [DataTestMethod]
    [DataRow("")]
    [DataRow("     ")]
    [DataRow(" ")]
    [DataRow("E")]
    [DataRow("ThisIsAVeryLongNameSoLoongThatItShouldNotBeAcceptedByEriksTest")]
    [DataRow(null)]
    public void Validate_InvalidFirstName_ReturnError(string invalidFirstName)
    {
        var validator = new CreateGuestDTOValidator();

        var dto = new CreateGuestDTO()
        {
            FirstName = invalidFirstName,
            LastName = "Testsson",
            Email = "Test@testsson.se",
            Phone = "0727435550"
        };

        var result = validator.TestValidate(dto);

        result.ShouldHaveValidationErrorFor(g => g.FirstName);
    }

    [DataTestMethod]
    [DataRow("Erik")]
    [DataRow("My")]
    [DataRow("Konstantinopel")]
    public void Validate_ValidFirstName_ReturnNoError(string validFirstName)
    {
        var validator = new CreateGuestDTOValidator();

        var dto = new CreateGuestDTO
        {
            FirstName = validFirstName,
            LastName = "Lastname",
            Email = "test@testsson.se",
            Phone = "093862528"
        };

        var result = validator.TestValidate(dto);

        result.ShouldNotHaveValidationErrorFor(g => g.FirstName);
    }

    [DataTestMethod]
    [DataRow("")]
    [DataRow("  ")]
    [DataRow(null)]
    [DataRow("T")]
    [DataRow("ThisMightBeALongerNameThenTheOneErikUsedInTheFirstNameTest?")]
    public void Validate_InvalidLastName_ReturnError(string invalidLastName)
    {
        var validator = new CreateGuestDTOValidator();

        var dto = new CreateGuestDTO()
        {
            FirstName = "Test",
            LastName = invalidLastName,
            Email = "Test@testsson.se",
            Phone = "0727435550"
        };

        var result = validator.TestValidate(dto);

        result.ShouldHaveValidationErrorFor(g => g.LastName);
    }

    [DataTestMethod]
    [DataRow("Andersson")]
    [DataRow("Ny")]
    [DataRow("Andersson fredriksson")]
    [DataRow("Olofssón")]
    public void Validate_ValidLastName_ReturnNoError(string validLastName)
    {
        var validator = new CreateGuestDTOValidator();

        var dto = new CreateGuestDTO
        {
            FirstName = "Test",
            LastName = validLastName,
            Email = "test@testsson.se",
            Phone = "092822729"
        };

        var result = validator.TestValidate(dto);

        result.ShouldNotHaveValidationErrorFor(g => g.LastName);

    }

}
