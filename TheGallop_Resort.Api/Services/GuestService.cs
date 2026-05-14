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

        public async Task<ServiceResult<Guest>> AddGuestAsync(CreateGuestDTO dto)
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

            return ServiceResult<Guest>.Ok(guest);

        }
        public async Task<IEnumerable<Guest>> GetAllGuestsInfoAsync()
        {
            var guests = await _ctx.Guests.ToListAsync();

            return guests;
        }

        public async Task<ServiceResult<GuestInfoDTO>> GetGuestInfoByIdAsync(int guestId)
        {
            var guest = await _ctx.Guests.Where(g => g.Id == guestId).Select(g => new GuestInfoDTO(
               g.FirstName,
               g.LastName,
               g.Email,
               g.PhoneNumber
               )).FirstOrDefaultAsync();

            if(guest == null)
            {
                return ServiceResult<GuestInfoDTO>.NotFound("Guest Not Found");
            }

            

            return ServiceResult<GuestInfoDTO>.Ok(guest);
        }

        public async Task<ServiceResult> DeleteGuestAsync(int guestId)
        {
            var guestToDelete = await _ctx.Guests.FirstOrDefaultAsync(g => g.Id == guestId);

            if (guestToDelete == null)
            {
                return ServiceResult.NotFound("Guest not found");
            }

            _ctx.Guests.Remove(guestToDelete);
            await _ctx.SaveChangesAsync();

            return ServiceResult.Ok();

        }

        public async Task<ServiceResult> UpdateGuestInfoAsync(int guestId, GuestInfoDTO dto)
        {
            var guestUpdate = await _ctx.Guests.FirstOrDefaultAsync(g => g.Id == guestId);

            if(guestUpdate == null)
            {
                return ServiceResult.NotFound("Guest not found");
            }

            guestUpdate.FirstName = dto.FirstName;
            guestUpdate.LastName = dto.LastName;
            guestUpdate.Email = dto.Email;
            guestUpdate.PhoneNumber = dto.Phone;

            await _ctx.SaveChangesAsync();

            return ServiceResult.Ok();

          
        }

        



       
    }
}
