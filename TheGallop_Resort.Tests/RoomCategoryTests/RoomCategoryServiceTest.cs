using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheGallop_Resort.Api.Data;
using TheGallop_Resort.Api.DTOs;
using TheGallop_Resort.Api.Services;
using TheGallop_Resort.Models.Models;

namespace TheGallop_Resort.Tests;

[TestClass]
public class RoomCategoryServiceTest
{
    private GaloppDbContext _ctx = null!;
    private RoomCategoryService _service = null!;

    [TestInitialize]
    public void SetUp()
    {
        var options = new DbContextOptionsBuilder<GaloppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _ctx = new GaloppDbContext(options);
        _service = new RoomCategoryService(_ctx);
    }

    [TestMethod]
    public async Task AddRoomCategoryAsync_ValidData_AddsRoomCategory()
    {
        var roomDetail = new RoomDetail
        {
            Id = 1,
            Beds = 2,
            MaxAdults = 2,
            MaxChildren = 1
        };

        await _ctx.RoomDetails.AddAsync(roomDetail);
        await _ctx.SaveChangesAsync();

        var dto = new RoomCategoryDTO
        {
            Type = RoomType.DoubleBed,
            CategoryPrice = 1800,
            RoomDetailId = roomDetail.Id
        };

        var result = await _service.AddRoomCategoryAsync(dto);

        result.SuccessfulResult.Should().BeTrue();

        var roomCategoryCount = await _ctx.RoomCategories.CountAsync();
        roomCategoryCount.Should().Be(1);

        var roomCategoryCheck = await _ctx.RoomCategories.FirstAsync();
        roomCategoryCheck.Type.Should().Be(dto.Type);
        roomCategoryCheck.CategoryPrice.Should().Be(dto.CategoryPrice);
        roomCategoryCheck.RoomDetailId.Should().Be(dto.RoomDetailId);
    }

    [TestMethod]
    public async Task AddRoomCategoryAsync_InvalidRoomDetailId_ReturnsValidationError()
    {
        var dto = new RoomCategoryDTO
        {
            Type = RoomType.DoubleBed,
            CategoryPrice = 1800,
            RoomDetailId = 9999
        };

        var result = await _service.AddRoomCategoryAsync(dto);

        result.SuccessfulResult.Should().BeFalse();
        result.Status.Should().Be(ServiceResultStatus.ValidationError);
        result.ErrorMessage.Should().Be("Room detail not found");

        var roomCategoryCount = await _ctx.RoomCategories.CountAsync();
        roomCategoryCount.Should().Be(0);
    }

    [TestMethod]
    public async Task UpdateRoomCategoryAsync_ValidData_UpdatesRoomCategory()
    {
        var roomDetail = new RoomDetail
        {
            Id = 1,
            Beds = 2,
            MaxAdults = 2,
            MaxChildren = 1
        };

        var existingRoomCategory = new RoomCategory
        {
            Id = 1,
            Type = RoomType.SingleBed,
            CategoryPrice = 1500,
            RoomDetailId = 1
        };

        await _ctx.RoomDetails.AddAsync(roomDetail);
        await _ctx.RoomCategories.AddAsync(existingRoomCategory);
        await _ctx.SaveChangesAsync();

        var dto = new RoomCategoryDTO
        {
            Type = RoomType.DoubleBed,
            CategoryPrice = 1900,
            RoomDetailId = 1
        };

        var result = await _service.UpdateRoomCategoryAsync(existingRoomCategory.Id, dto);

        result.SuccessfulResult.Should().BeTrue();

        var updatedRoomCategory = await _ctx.RoomCategories.FindAsync(existingRoomCategory.Id);
        updatedRoomCategory.Should().NotBeNull();
        updatedRoomCategory!.Type.Should().Be(dto.Type);
        updatedRoomCategory.CategoryPrice.Should().Be(dto.CategoryPrice);
        updatedRoomCategory.RoomDetailId.Should().Be(dto.RoomDetailId);
    }

    [TestMethod]
    public async Task UpdateRoomCategoryAsync_IdDoesNotExist_ReturnsNotFound()
    {
        var dto = new RoomCategoryDTO
        {
            Type = RoomType.DoubleBed,
            CategoryPrice = 1900,
            RoomDetailId = 1
        };

        var nonExistingId = 999;

        var result = await _service.UpdateRoomCategoryAsync(nonExistingId, dto);

        result.SuccessfulResult.Should().BeFalse();
        result.Status.Should().Be(ServiceResultStatus.NotFound);
        result.ErrorMessage.Should().Be("Room category not found");

        var count = await _ctx.RoomCategories.CountAsync();
        count.Should().Be(0);
    }

    [TestMethod]
    public async Task UpdateRoomCategoryAsync_InvalidRoomDetailId_ReturnsValidationError()
    {
        var existingRoomCategory = new RoomCategory
        {
            Id = 1,
            Type = RoomType.SingleBed,
            CategoryPrice = 1500,
            RoomDetailId = 1
        };

        await _ctx.RoomCategories.AddAsync(existingRoomCategory);
        await _ctx.SaveChangesAsync();

        var dto = new RoomCategoryDTO
        {
            Type = RoomType.DoubleBed,
            CategoryPrice = 1900,
            RoomDetailId = 9999
        };

        var result = await _service.UpdateRoomCategoryAsync(existingRoomCategory.Id, dto);

        result.SuccessfulResult.Should().BeFalse();
        result.Status.Should().Be(ServiceResultStatus.ValidationError);
        result.ErrorMessage.Should().Be("Room detail not found");

        var unchangedRoomCategory = await _ctx.RoomCategories.FindAsync(existingRoomCategory.Id);
        unchangedRoomCategory.Should().NotBeNull();
        unchangedRoomCategory!.Type.Should().Be(RoomType.SingleBed);
        unchangedRoomCategory.CategoryPrice.Should().Be(1500);
        unchangedRoomCategory.RoomDetailId.Should().Be(1);
    }

    [TestMethod]
    public async Task GetRoomCategoryByIdAsync_IdDoesNotExist_ReturnsNotFound()
    {
        var result = await _service.GetRoomCategoryByIdAsync(9999);

        result.SuccessfulResult.Should().BeFalse();
        result.Status.Should().Be(ServiceResultStatus.NotFound);
        result.ErrorMessage.Should().Be("Room category not found");
    }

    [TestMethod]
    public async Task DeleteRoomCategoryAsync_IdDoesNotExist_ReturnsNotFound()
    {
        var result = await _service.DeleteRoomCategoryAsync(9999);

        result.SuccessfulResult.Should().BeFalse();
        result.Status.Should().Be(ServiceResultStatus.NotFound);
        result.ErrorMessage.Should().Be("Room category not found");
    }
}