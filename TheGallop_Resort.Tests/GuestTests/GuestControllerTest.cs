using FakeItEasy;
using FluentAssertions;
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

        var fake = A.Fake<IGuestService>();

        var controller = new GuestController(fake);

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

        A.CallTo(() => fake.AddGuestAsync(A<CreateGuestDTO>._))
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

        A.CallTo(() => fake.AddGuestAsync(A<CreateGuestDTO>._))
            .MustHaveHappenedOnceExactly();
    }
}
