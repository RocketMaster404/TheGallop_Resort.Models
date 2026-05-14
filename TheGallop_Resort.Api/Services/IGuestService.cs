using Microsoft.AspNetCore.Mvc;
using TheGallop_Resort.Api.DTOs;
using TheGallop_Resort.Models.Models;

namespace TheGallop_Resort.Api.Services
{
    public interface IGuestService
    {
       
        Task<ServiceResult<Guest>> AddGuestAsync(CreateGuestDTO dto);
        Task<IEnumerable<Guest>> GetAllGuestsInfoAsync();
        Task<GuestInfoDTO> GetGuestInfoByIdAsync(int guestId);
        Task<Guest> DeleteGuestAsync(int guestId);
    }
}


