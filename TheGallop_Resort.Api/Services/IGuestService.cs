using Microsoft.AspNetCore.Mvc;
using TheGallop_Resort.Api.DTOs;
using TheGallop_Resort.Models.Models;

namespace TheGallop_Resort.Api.Services
{
    public interface IGuestService
    {
       
        Task<ServiceResult<Guest>> AddGuestAsync(CreateGuestDTO dto);
        Task<IEnumerable<Guest>> GetAllGuestsInfoAsync();
        Task<ServiceResult<GuestInfoWithBookingDTO>> GetGuestInfoByIdAsync(int guestId);
        Task<ServiceResult> DeleteGuestAsync(int guestId);
        Task<ServiceResult> UpdateGuestInfoAsync(int guestId, UpdateGuestInfoDTO dto);
        Task<ServiceResult<GuestInfoWithBookingDTO>> GetGuestBookingHistoryAsync(int guestId);
        Task<ServiceResult<List<GuestBookingInfoDTO>>> GetGuestFutureBookingsAsync(int guestId);

    }
}


