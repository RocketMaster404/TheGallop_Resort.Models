using Microsoft.AspNetCore.Mvc;
using TheGallop_Resort.Api.DTOs;

namespace TheGallop_Resort.Api.Services
{
    public interface IBookingService 
    {
        Task<ActionResult<IEnumerable<GetBookingResponseDTO>>> GetAllBookingsAsync();
    }
}
