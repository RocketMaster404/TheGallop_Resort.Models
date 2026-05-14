using Microsoft.AspNetCore.Mvc;
using TheGallop_Resort.Api.DTOs;
using TheGallop_Resort.Models.Models;

namespace TheGallop_Resort.Api.Services
{
    public interface IBookingService 
    {
        Task<ServiceResult<IEnumerable<GetBookingResponseDTO>>> GetAllBookingsAsync();

        Task<ServiceResult<GetBookingResponseDTO>> GetBookingByIdAsync(int bookingId);

        Task<ServiceResult<Booking>> AddBookingAsync(int guestId);
    }
}
