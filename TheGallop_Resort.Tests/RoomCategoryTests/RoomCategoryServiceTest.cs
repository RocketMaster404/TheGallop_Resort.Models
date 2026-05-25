using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TheGallop_Resort.Api.Data;
using TheGallop_Resort.Api.DTOs;
using TheGallop_Resort.Api.Services;
using TheGallop_Resort.Models.Models;

namespace TheGallop_Resort.Tests;

[TestClass]
public class RoomCategoryServiceTest
{
    private GaloppDbContext _ctx;
    private RoomCategoryService _service;

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
    public async Task AddRoomCategoryAsync_AddValidRoomCategory_ReturnOne()
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
}
