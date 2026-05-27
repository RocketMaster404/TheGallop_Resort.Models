using FakeItEasy;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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
    public async Task GetGuestInfoById_CheckGuestInfo_ReturnOk()
    {
        var fake = A.Fake<IGuestService>();
        var validator = A.Fake<IValidator<CreateGuestDTO>>();
        var validatorUpdateGuest = A.Fake<IValidator<UpdateGuestInfoDTO>>();
        var controller = new GuestController(fake,validator,validatorUpdateGuest);

        var guest = new GuestInfoWithBookingDTO(
            "Test",
            "Testsson",
            "test@testsson.se",
            "076238723",
            new List<BookingDetailsDTO>()
            );
        



        A.CallTo(() => fake.GetGuestInfoByIdAsync(1))
            .Returns(ServiceResult<GuestInfoWithBookingDTO>.Ok(guest));

        ActionResult<GuestInfoWithBookingDTO> result = await controller.GetGuestInfoById(1);

        var resultOk = result.Should()
            .BeAssignableTo<OkObjectResult>()
            .Subject;

        var returnedGuest = resultOk.Value.Should()
        .BeAssignableTo<GuestInfoWithBookingDTO>()
        .Subject;

        returnedGuest.FirstName.Should().Be(guest.FirstName);
        returnedGuest.LastName.Should().Be(guest.LastName);
        returnedGuest.Email.Should().Be(guest.Email);
        returnedGuest.Phone.Should().Be(guest.Phone);

       


    }


    [TestMethod]
    public async Task AddGuest_AddValidGuest_Return200()
    {
        var fake = A.Fake<IGuestService>();
        var fakeValidator = A.Fake<IValidator<CreateGuestDTO>>();
        var validatorUpdateGuest = A.Fake<IValidator<UpdateGuestInfoDTO>>();

        var controller = new GuestController(
            fake,
            fakeValidator,validatorUpdateGuest);

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

        
    }

    [TestMethod]
    public async Task UpdateGuest_CheckUpdatedGuestInfo_ReturnUpdatedObject()
    {
        var fake = A.Fake<IGuestService>();
        var validatorCreateGuest = A.Fake<IValidator<CreateGuestDTO>>();
        var validatorUpdateGuest = A.Fake < IValidator< UpdateGuestInfoDTO>>();

        var controller = new GuestController(fake,validatorCreateGuest,validatorUpdateGuest);

       

        var updatedGuestInfo = new UpdateGuestInfoDTO
        {
            FirstName = "Ove",
           LastName = "Sundberg",
            Email = "Ove@Sundberg.com",
            Phone ="078981232"
        };
        UpdateGuestInfoDTO? capturedDto = null;

            A.CallTo(() => fake.UpdateGuestInfoAsync(1, A<UpdateGuestInfoDTO>._))
            .Invokes((int id, UpdateGuestInfoDTO dto) =>
            {
                capturedDto = dto;
            }).Returns(ServiceResult.Ok());


        IActionResult result = await controller.UpdateGuestInfo(1, updatedGuestInfo);

        result.Should().BeAssignableTo<NoContentResult>();

        capturedDto.Should().NotBeNull();
        capturedDto!.FirstName.Should().Be(updatedGuestInfo.FirstName);
        capturedDto.LastName.Should().Be(updatedGuestInfo.LastName);
        capturedDto.Email.Should().Be(updatedGuestInfo.Email);
        capturedDto.Phone.Should().Be(updatedGuestInfo.Phone);

    }

    [TestMethod]
    public async Task DeleteGuest_Return_NoContent()
    {

        var fake = A.Fake<IGuestService>();
        var validator = A.Fake<IValidator<CreateGuestDTO>>();
        var validatorUpdateGuest = A.Fake<IValidator<UpdateGuestInfoDTO>>();

        A.CallTo(() => fake.DeleteGuestAsync(1))
            .Returns(ServiceResult.Ok());

        var controller = new GuestController(fake,validator,validatorUpdateGuest);

        
        IActionResult result = await controller.DeleteGuest(1);
        
        result.Should().BeAssignableTo<NoContentResult>();


        

    }



}