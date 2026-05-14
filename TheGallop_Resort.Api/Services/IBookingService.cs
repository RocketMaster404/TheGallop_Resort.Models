using Microsoft.AspNetCore.Mvc;
using TheGallop_Resort.Api.DTOs;
using TheGallop_Resort.Models.Models;

namespace TheGallop_Resort.Api.Services
{
    public interface IBookingService 
    {
        Task<ActionResult<IEnumerable<GetBookingResponseDTO>>> GetAllBookingsAsync();

        Task<ServiceResult<Booking>> AddBookingAsync(int guestId);
    }
}
