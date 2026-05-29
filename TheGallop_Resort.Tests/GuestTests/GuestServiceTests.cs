using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TheGallop_Resort.Api.Data;
using TheGallop_Resort.Api.DTOs;
using TheGallop_Resort.Api.Services;
using TheGallop_Resort.Models.Models;

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
    public async Task AddGuest_AddDuplicatedEmail_ReturnValidationError()
    {
        var guest = new CreateGuestDTO
        {
            FirstName = "Erik",
            LastName = "Bosse",
            Email = "valid@email.com",
            Phone = "09872652"
        };

        var guestDuplicate = new CreateGuestDTO
        {
            FirstName = "Erik",
            LastName = "Bosse",
            Email = "valid@email.com",
            Phone = "09872652"
        };

        await _service.AddGuestAsync(guest);
        var result = await _service.AddGuestAsync(guestDuplicate);

        var counter = await _ctx.Guests.CountAsync();
        counter.Should().Be(1);
        result.SuccessfulResult.Should().BeFalse();
        result.Status.Should().Be(ServiceResultStatus.ValidationError);

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
    public async Task UpdateGuestAsync_UpdateNonExistingGuest_ReturnNotFound()
    {
        var guestInfo = new UpdateGuestInfoDTO()
        {
            FirstName = "test",
            LastName = "Testsson",
            Email = "test@testsson.com",
            Phone = "098376211"
        };

        var result = await _service.UpdateGuestInfoAsync(10, guestInfo);

        result.SuccessfulResult.Should().BeFalse();
        result.Status.Should().Be(ServiceResultStatus.NotFound);
    }

    [TestMethod]
    public async Task UpdateGuest_UpdateDuplicateEmail_ReturnValidationError()
    {
        var guestOne = new CreateGuestDTO
        {
            FirstName = "Test",
            LastName = "Testsson",
            Email = "test@testsson.com",
            Phone = "029379211"
        };

        var guestTwo = new CreateGuestDTO
        {
            FirstName = "Ove",
            LastName = "Sundberg",
            Email = "Ove@Sundberg.se",
            Phone = "0928372929"
        };

        await _service.AddGuestAsync(guestOne);
        await _service.AddGuestAsync(guestTwo);

        var update = new UpdateGuestInfoDTO
        {
            FirstName = "Test",
            LastName = "Testsson",
            Email = "test@testsson.com",
            Phone = "029379211"
        };


        var result = await _service.UpdateGuestInfoAsync(2, update);

        result.SuccessfulResult.Should().BeFalse();

    }

    [TestMethod]
    public async Task UpdateGuest_UpdateGuestinfoSameEmail_ReturnNoError()
    {
        var guestOne = new CreateGuestDTO
        {
            FirstName = "Test",
            LastName = "Testsson",
            Email = "test@testsson.com",
            Phone = "029379211"
        };

        

        await _service.AddGuestAsync(guestOne);
        

        var update = new UpdateGuestInfoDTO
        {
            FirstName = "Testare",
            LastName = "Uttcecklarsson",
            Email = "test@testsson.com",
            Phone = "029379211"
        };


        var result = await _service.UpdateGuestInfoAsync(1, update);

        result.SuccessfulResult.Should().BeTrue();

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
    public async Task DeleteGuestAsync_DeleteNonExisitingGuest_ReturnNotFound()
    {
        var invalidId = 10;

        var result = await _service.DeleteGuestAsync(invalidId);

        result.SuccessfulResult.Should().BeFalse();
        result.Status.Should().Be(ServiceResultStatus.NotFound);
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

    [TestMethod]
    public async Task GetGuestInfoByIdAsync_GetGuestinfoFromInvalidGuest_ReturnNotFound()
    {
        var invalidId = 11;

        var result = await _service.GetGuestInfoByIdAsync(invalidId);

        result.SuccessfulResult.Should().BeFalse();
        result.Status.Should().Be(ServiceResultStatus.NotFound);
    }

    [TestMethod]
    public async Task GetGuestBookingHistoryAsync_ExistingGuest_ReturnsBookingHistory()
    {
        var guest = new Guest
        {
            Id = 1,
            FirstName = "Test",
            LastName = "Testsson",
            Email = "test@testsson.com",
            PhoneNumber = "09383728"
        };

        var booking = new Booking
        {
            Id = 1,
            GuestId = guest.Id,
            Guest = guest,
            CreatedAt = DateTime.Now.AddDays(-5),
            Status = Status.Confirmed,
            TotalPrice = 2000m
        };

        var room = new Room
        {
            Id = 1
        };

        var roomReservation = new RoomReservation
        {
            Id = 1,

            BookingId = booking.Id,
            Booking = booking,

            RoomId = room.Id,
            Room = room,

            CheckIn = DateTime.Now.AddDays(-5),
            CheckOut = DateTime.Now.AddDays(-2),

            Adults = 1,
            Children = 1,

            RoomStatus = RoomStatus.Confirmed,
            PricePerNight = 1000
        };

        booking.RoomReservations.Add(roomReservation);
        guest.Bookings.Add(booking);

        await _ctx.Rooms.AddAsync(room);
        await _ctx.Guests.AddAsync(guest);

        await _ctx.SaveChangesAsync();

        var result = await _service.GetGuestBookingHistoryAsync(guest.Id);

        result.Should().NotBeNull();
        result.SuccessfulResult.Should().BeTrue();
        result.Data.Should().HaveCount(1);
    }


}
