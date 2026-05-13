using Microsoft.AspNetCore.Mvc;
using TheGallop_Resort.Api.Data;
using TheGallop_Resort.Api.DTOs;
using TheGallop_Resort.Models.Models;

namespace TheGallop_Resort.Api.Services
{
    public class GuestService : IGuestService
    {

        private readonly GaloppDbContext _ctx;

        public GuestService(GaloppDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Guest> AddGuestAsync(CreateGuestDTO dto)
        {
            var guest = new Guest
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PhoneNumber = dto.Phone
            };

            await _ctx.Guests.AddAsync(guest);
            await _ctx.SaveChangesAsync();

            return guest;

        }
    }
}
