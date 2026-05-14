using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<List<Guest>> GetAllGuestsInfoAsync()
        {
            var guests = await _ctx.Guests.ToListAsync();

            if (!guests.Any())
            {
                throw new Exception("Guests not found");
            }

            return guests;
        }

        public async Task<GuestInfoDTO> GetGuestInfoByIdAsync(int guestId)
        {
            var guest = await _ctx.Guests.Where(g => g.Id == guestId).Select(g => new GuestInfoDTO(
               g.FirstName,
               g.LastName,
               g.Email,
               g.PhoneNumber
               )).FirstOrDefaultAsync();

            if (guest == null)
            {
                throw new Exception("Guest not found");
            }

            return guest;
        }

        public async Task<Guest> DeleteGuestAsync(int guestId)
        {
            var guestToDelete = await _ctx.Guests.FirstOrDefaultAsync(g => g.Id == guestId);

            if (guestToDelete == null)
            {
                throw new Exception("Guest not found");
            }

            _ctx.Guests.Remove(guestToDelete);
            await _ctx.SaveChangesAsync();

            return guestToDelete;

        }

       
    }
}
