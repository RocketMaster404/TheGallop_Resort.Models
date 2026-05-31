using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using TheGallop_Resort.Api.Controllers;
using TheGallop_Resort.Api.DTOs;
using TheGallop_Resort.Api.Services;
using TheGallop_Resort.Models.Models;

namespace TheGallop_Resort.Tests;

[TestClass]
public class RoomCategoryControllerTest
{

    [TestMethod]
public async Task AddRoomCategory_AddValidRoomCategory_Return200()
{
    var fake = A.Fake<IRoomCategoryService>();
    var controller = new RoomCategoryController(fake);

    var roomCategoryDto = new RoomCategoryDTO
    {
        Type = RoomType.DoubleBed,
        CategoryPrice = 1800,
        RoomDetailId = 1
    };

    var roomCategory = new RoomCategory
    {
        Id = 1,
        Type = roomCategoryDto.Type,
        CategoryPrice = roomCategoryDto.CategoryPrice,
        RoomDetailId = roomCategoryDto.RoomDetailId
    };

    A.CallTo(() => fake.AddRoomCategoryAsync(A<RoomCategoryDTO>._))
        .Returns(ServiceResult<RoomCategory>.Ok(roomCategory));

    IActionResult result = await controller.AddRoomCategory(roomCategoryDto);

    var okResult = result.Should()
        .BeAssignableTo<OkObjectResult>()
        .Subject;

    var returnedRoomCategory = okResult.Value.Should()
        .BeAssignableTo<RoomCategory>()
        .Subject;
    returnedRoomCategory.Type.Should().Be(roomCategoryDto.Type);
    returnedRoomCategory.CategoryPrice.Should().Be(roomCategoryDto.CategoryPrice);
    returnedRoomCategory.RoomDetailId.Should().Be(roomCategoryDto.RoomDetailId);

    A.CallTo(() => fake.AddRoomCategoryAsync(A<RoomCategoryDTO>._))
        .MustHaveHappenedOnceExactly();
}

    [TestMethod]
    public async Task GetRoomCategoryById_CheckIfIdExists_ReturnOk()
    {
        var fake = A.Fake<IRoomCategoryService>();
        var controller = new RoomCategoryController(fake);

        var roomCategory = new RoomCategory
        {
            Id = 1,
            Type = RoomType.DoubleBed,
            CategoryPrice = 1800,
            RoomDetailId = 1
        };

        A.CallTo(() => fake.GetRoomCategoryByIdAsync(1))
            .Returns(ServiceResult<RoomCategory>.Ok(roomCategory));

        IActionResult result = await controller.GetRoomCategoryById(1);

        var okResult = result.Should()
            .BeAssignableTo<OkObjectResult>()
            .Subject;

        var returnedRoomCategory = okResult.Value.Should()
            .BeAssignableTo<RoomCategory>()
            .Subject;

        returnedRoomCategory.Id.Should().Be(roomCategory.Id);
        returnedRoomCategory.Type.Should().Be(roomCategory.Type);
        returnedRoomCategory.CategoryPrice.Should().Be(roomCategory.CategoryPrice);
        returnedRoomCategory.RoomDetailId.Should().Be(roomCategory.RoomDetailId);

        A.CallTo(() => fake.GetRoomCategoryByIdAsync(1))
            .MustHaveHappenedOnceExactly();
    }

    
}