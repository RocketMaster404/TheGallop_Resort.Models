using FakeItEasy;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using TheGallop_Resort.Api.Controllers;
using TheGallop_Resort.Api.DTOs;
using TheGallop_Resort.Api.Services;
using TheGallop_Resort.Models.Models;

namespace TheGallop_Resort.Tests;

[TestClass]
public class GuestControllerTest
{
    [TestMethod]
    public async Task AddGuest_AddValidGuest_Return200()
    {
        var fakeService = A.Fake<IGuestService>();
        var fakeValidator = A.Fake<IValidator<CreateGuestDTO>>();

        var controller = new GuestController(
            fakeService,
            fakeValidator);

        A.CallTo(() => fakeValidator.ValidateAsync(
                A<CreateGuestDTO>._,
                default))
            .Returns(new ValidationResult());

        var guestDto = new CreateGuestDTO
        {
            FirstName = "Test",
            LastName = "Testsson",
            Email = "test@test.com",
            Phone = "0701234567"
        };

        var guest = new Guest
        {
            FirstName = guestDto.FirstName,
            LastName = guestDto.LastName,
            Email = guestDto.Email,
            PhoneNumber = guestDto.Phone
        };

        A.CallTo(() => fakeService.AddGuestAsync(A<CreateGuestDTO>._))
            .Returns(ServiceResult<Guest>.Ok(guest));

        IActionResult result = await controller.AddGuest(guestDto);

        var okResult = result.Should()
            .BeAssignableTo<OkObjectResult>()
            .Subject;

        var returnedGuest = okResult.Value.Should()
            .BeAssignableTo<Guest>()
            .Subject;

        returnedGuest.FirstName.Should().Be(guestDto.FirstName);
        returnedGuest.LastName.Should().Be(guestDto.LastName);
        returnedGuest.Email.Should().Be(guestDto.Email);
        returnedGuest.PhoneNumber.Should().Be(guestDto.Phone);

        A.CallTo(() => fakeService.AddGuestAsync(A<CreateGuestDTO>._))
            .MustHaveHappenedOnceExactly();
    }
}