using Microsoft.AspNetCore.Mvc;
using TheGallop_Resort.Api.DTOs;
using TheGallop_Resort.Models.Models;

namespace TheGallop_Resort.Api.Services
{
    public interface IBookingService 
    {
        Task<ServiceResult<IEnumerable<GetBookingResponseDTO>>> GetAllBookingsAsync();

        Task<ServiceResult<GetBookingResponseDTO>> GetBookingByIdAsync(int bookingId);

        Task<ServiceResult> UpdateGuestOnBookingAsync(UpdateBookingGuestDTO update);
        Task<ServiceResult> UpdateBookingStatusAsync(UpdateBookingStatusDTO update);

        Task<ServiceResult<GetFullBookingResponsDTO>> CreateBookingAsync(GetInputFromUserCreateDTO dto);

        Task<ServiceResult<IEnumerable<GetBookingResponseDTO>>> GetBookingsForNextMonthAsync();

        Task<ServiceResult<IEnumerable<GetBookingResponseDTO>>> GetBookingsForSpecifikDateAsync(DateOnly inputDate);

        Task<ServiceResult<IEnumerable<GetBookingResponseDTO>>> GetBookingsBetweenDatesAsync(SearchBookingBetweenDateDTO dto);

        Task<ServiceResult> DeleteBookingByIdAsync(int bookingId);
    }
}
