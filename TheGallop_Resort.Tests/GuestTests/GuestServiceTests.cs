using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
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


}
