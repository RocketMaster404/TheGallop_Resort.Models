using Microsoft.AspNetCore.Mvc;
using TheGallop_Resort.Api.DTOs;
using TheGallop_Resort.Models.Models;

namespace TheGallop_Resort.Api.Services
{
    public interface IGuestService
    {
       
        Task<Guest> AddGuestAsync(CreateGuestDTO dto);
        Task<List<Guest>> GetAllGuestsInfoAsync();
        Task<GuestInfoDTO> GetGuestInfoByIdAsync(int guestId);
        Task<Guest> DeleteGuestAsync(int guestId);
    }
}


