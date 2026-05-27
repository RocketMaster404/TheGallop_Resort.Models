using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TheGallop_Resort.Api.Data;
using TheGallop_Resort.Api.DTOs;
using TheGallop_Resort.Api.Services;

namespace TheGallop_Resort.Tests;

[TestClass]
public class GuestServiceTests
{

    private GaloppDbContext _ctx;
    private GuestService _service;

    [TestInitialize]
    public void setup()
    {
        var options = new DbContextOptionsBuilder<GaloppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

        _ctx = new GaloppDbContext(options);
        _service = new GuestService(_ctx);
    }
      
    [TestMethod]
    public async Task AddGuest_AddValidGuest_ReturnOne()
    {
        var guestDto = new CreateGuestDTO()
        {
            FirstName = "Test",
            LastName = "Testsson",
            Email = "Test@Testsson.com",
            Phone = "0727435550"
        };

        var result = await _service.AddGuestAsync(guestDto);

        result.SuccessfulResult.Should().BeTrue();
        var guestCount = await _ctx.Guests.CountAsync();
        guestCount.Should().Be(1);

        var guestCheck = await _ctx.Guests.FirstAsync();

        guestCheck.FirstName.Should().Be(guestDto.FirstName);
        guestCheck.LastName.Should().Be(guestDto.LastName);
        guestCheck.Email.Should().Be(guestDto.Email);
        guestCheck.PhoneNumber.Should().Be(guestDto.Phone);

    }

    [TestMethod]
    public async Task UpdateGuestInfoAsync_UpdateGuest_ReturnNewObject()
    {

        var guestDto = new CreateGuestDTO()
        {
            FirstName = "Test",
            LastName = "Testsson",
            Email = "Test@Testsson.com",
            Phone = "0727435550"
        };

        await _service.AddGuestAsync(guestDto);

        var guestDtoUpdate = new UpdateGuestInfoDTO
        {
            FirstName = "Ove",
            LastName = "Sundberg",
            Email = "ove@sundberg.com",
            Phone = "0727673521"
        };
            
            
       
        await _service.UpdateGuestInfoAsync(1, guestDtoUpdate);

        var guestCheck = await _ctx.Guests.FirstAsync();
        guestCheck.FirstName.Should().Be(guestDtoUpdate.FirstName);
        guestCheck.LastName.Should().Be(guestDtoUpdate.LastName);
        guestCheck.Email.Should().Be(guestDtoUpdate.Email);
        guestCheck.PhoneNumber.Should().Be(guestDtoUpdate.Phone);

    }

    [TestMethod]
    public async Task DeleteGuestAsync_DeleteGuest_ReturnZero()
    {
        var guestDto = new CreateGuestDTO()
        {
            FirstName = "Test",
            LastName = "Testsson",
            Email = "test@testsson.com",
            Phone = "098236752"
        };

        await _service.AddGuestAsync(guestDto);

        await _service.DeleteGuestAsync(1);

        _ctx.Guests.Should().BeEmpty();         
    }

    [TestMethod]
    public async Task GetGuestInfoByIdAsync_GetUserInfo_ReturnGuestInfo()
    {
        var guestDto = new CreateGuestDTO()
        {
            FirstName = "Test",
            LastName = "Testsson",
            Email = "test@testsson.se",
            Phone = "098727262"
        };

        await _service.AddGuestAsync(guestDto);

        var guest = await _service.GetGuestInfoByIdAsync(1);

        guest.Should().NotBeNull();

        guest.Data.FirstName.Should().Be(guestDto.FirstName);
        guest.Data.LastName.Should().Be(guestDto.LastName);
        guest.Data.Email.Should().Be(guestDto.Email);
        guest.Data.Phone.Should().Be(guestDto.Phone);

    }


}
