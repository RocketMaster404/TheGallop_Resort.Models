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
    public async Task GetGuestInfoById_CheckGuestInfo_ReturnOk()
    {
        var fake = A.Fake<IGuestService>();
        var validator = A.Fake<IValidator<CreateGuestDTO>>();
        var validatorUpdateGuest = A.Fake<IValidator<UpdateGuestInfoDTO>>();
        var controller = new GuestController(fake, validator, validatorUpdateGuest);

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

        var okResult = result.Result.Should()
            .BeOfType<OkObjectResult>()
            .Subject;

        var returnedGuest = okResult.Value.Should()
       .BeAssignableTo<GuestInfoWithBookingDTO>()
       .Subject;

        returnedGuest.Should().BeEquivalentTo(guest);

    }

    [TestMethod]
    public async Task GetGuestInfoById_CheckGuestInfo_ReturnNotFound()
    {
        var fake = A.Fake<IGuestService>();
        var validator = A.Fake<IValidator<CreateGuestDTO>>();
        var validatorUpdate = A.Fake<IValidator<UpdateGuestInfoDTO>>();

        var controller = new GuestController(fake, validator, validatorUpdate);

        A.CallTo(() => fake.GetGuestInfoByIdAsync(1))
            .Returns(ServiceResult<GuestInfoWithBookingDTO>.NotFound("Guest not found"));

        ActionResult<GuestInfoWithBookingDTO> result = await controller.GetGuestInfoById(1);

        result.Result.Should().BeOfType<NotFoundObjectResult>();

    }


    [TestMethod]
    public async Task AddGuest_AddValidGuest_Return200()
    {
        var fake = A.Fake<IGuestService>();
        var fakeValidator = A.Fake<IValidator<CreateGuestDTO>>();
        var validatorUpdateGuest = A.Fake<IValidator<UpdateGuestInfoDTO>>();

        var controller = new GuestController(
            fake,
            fakeValidator, validatorUpdateGuest);

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



        returnedGuest.Should().BeEquivalentTo(guest);


    }

    [TestMethod]
    public async Task AddGuest_AddInvalidGuest_ReturnBadRequest()
    {

        var fakeService = A.Fake<IGuestService>();
        var fakeValidator = A.Fake<IValidator<CreateGuestDTO>>();
        var fakeUpdateValidator = A.Fake<IValidator<UpdateGuestInfoDTO>>();

        var controller = new GuestController(
            fakeService,
            fakeValidator,
            fakeUpdateValidator);

        var guestDto = new CreateGuestDTO
        {
            FirstName = "",
            LastName = "Testsson",
            Email = "invalidEmail",
            Phone = "**"
        };

        var validationFailures = new List<ValidationFailure>
    {
        new ValidationFailure("FirstName", "Invalid number of character"),
        new ValidationFailure("Email", "Invalid format"),
        new ValidationFailure("Phone", "Invalid format")
    };

        var validationResult = new ValidationResult(validationFailures);

        A.CallTo(() => fakeValidator.ValidateAsync(
                A<CreateGuestDTO>._,
                default))
            .Returns(validationResult);


        IActionResult result = await controller.AddGuest(guestDto);


        var badRequestResult = result.Should()
            .BeAssignableTo<BadRequestObjectResult>()
            .Subject;

        badRequestResult.Value.Should()
            .BeEquivalentTo(validationFailures);

        A.CallTo(() => fakeService.AddGuestAsync(A<CreateGuestDTO>._))
            .MustNotHaveHappened();
    }





    [TestMethod]
    public async Task UpdateGuest_CheckUpdatedGuestInfo_ReturnUpdatedObject()
    {
        var fake = A.Fake<IGuestService>();
        var validatorCreateGuest = A.Fake<IValidator<CreateGuestDTO>>();
        var validatorUpdateGuest = A.Fake<IValidator<UpdateGuestInfoDTO>>();

        A.CallTo(() => validatorUpdateGuest.ValidateAsync(
        A<UpdateGuestInfoDTO>._,
        default))
         .Returns(new FluentValidation.Results.ValidationResult());

        var controller = new GuestController(fake, validatorCreateGuest, validatorUpdateGuest);



        var updatedGuestInfo = new UpdateGuestInfoDTO
        {
            FirstName = "Ove",
            LastName = "Sundberg",
            Email = "Ove@Sundberg.com",
            Phone = "078981232"
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

        var controller = new GuestController(fake, validator, validatorUpdateGuest);


        IActionResult result = await controller.DeleteGuest(1);

        result.Should().BeAssignableTo<NoContentResult>();

    }

    [TestMethod]
    public async Task DeleteGuest_InvalidGuest_ReturnNotFound()
    {
        var fakeService = A.Fake<IGuestService>();
        var validator = A.Fake<IValidator<CreateGuestDTO>>();
        var updateValidator = A.Fake<IValidator<UpdateGuestInfoDTO>>();

        A.CallTo(() => fakeService.DeleteGuestAsync(1))
            .Returns(ServiceResult.NotFound("Guest not found"));

        var controller = new GuestController(fakeService, validator, updateValidator);

        IActionResult result = await controller.DeleteGuest(1);

        var notFound = result.Should().BeAssignableTo<NotFoundObjectResult>().Subject;
    }

    [TestMethod]
    public async Task GetGuestBookingHistory_ExistingGuest_ReturnOk()
    {
      
        var fake = A.Fake<IGuestService>();
        var validator = A.Fake<IValidator<CreateGuestDTO>>();
        var validatorUpdateGuest = A.Fake<IValidator<UpdateGuestInfoDTO>>();

        var controller = new GuestController(
            fake,
            validator,
            validatorUpdateGuest);

        var response = new List<GetBookingResponseDTO>
    {
        new GetBookingResponseDTO
        {
            Id = 1,
            CreatedAt = DateTime.Now.AddDays(-5),
            TotalPrice = 2000m,
            Status = Status.Confirmed,

            Guest = new GuestInfoDTO(
                "Test",
                "Testsson",
                "test@test.com",
                "070123456"
            ),

            RoomReservation = new List<GetRoomReservationResponseDTO>()
        }
    };

        A.CallTo(() => fake.GetGuestBookingHistoryAsync(1))
            .Returns(ServiceResult<List<GetBookingResponseDTO>>
            .Ok(response));

        ActionResult<List<GetBookingResponseDTO>> result =
            await controller.GetUsersBookingHistory(1);

    
        var okResult = result.Result.Should()
            .BeOfType<OkObjectResult>()
            .Subject;

        var returnedResult = okResult.Value.Should()
            .BeAssignableTo<ServiceResult<List<GetBookingResponseDTO>>>()
            .Subject;

        returnedResult.Data.Should()
            .BeEquivalentTo(response);
    }



}