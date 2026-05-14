using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using TheGallop_Resort.Api.Data;
using TheGallop_Resort.Api.DTOs;
using TheGallop_Resort.Api.Services;
using TheGallop_Resort.Models.Models;

namespace TheGallop_Resort.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : BaseController
    {
        private readonly IBookingService _bookingService;


        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet("getAllBookings", Name = "GetAllBooking")]
        public async Task<ActionResult<IEnumerable<GetBookingResponseDTO>>> GetAllBookings()
        {
            var bookings = await _bookingService.GetAllBookingsAsync();

            return Ok(bookings);
        }

        [HttpGet("getBookingsById/{bookingId}", Name = "getBookingsById")]
        public async Task<ActionResult<GetBookingResponseDTO>> GetBookingById(int bookingId)
        {
            var booking = await _bookingService.GetBookingByIdAsync(bookingId);

            if (!booking.SuccessfulResult)
            {
                return NotFound(booking.ErrorMessage);
            }

            return Ok(booking);
        }

        [HttpPost("AddBooking/{guestId}", Name = "AddBooking")]
        public async Task<ActionResult<Booking>> AddBooking(int guestId)
        {
            var booking = await _bookingService.AddBookingAsync(guestId);

            if (!booking.SuccessfulResult)
            {
                return NotFound(booking.ErrorMessage);
            }

            return Ok(booking);
        }

        [HttpPut("updateGuestOnBooking", Name = "UpdateGuestOnBooking")]
        public async Task<IActionResult> UpdateGuestOnBooking(UpdateBookingGuestDTO update)
        {
            var booking = await _bookingService.UpdateGuestOnBookingAsync(update);

            if (!booking.SuccessfulResult)
            {
                return ToErrorResponse(booking);
            }

            return Ok(booking);
        }

        [HttpPut("updateStatusOnBooking", Name = "UpdateStausOnBooking")]
        public async Task<IActionResult> UpdateBookingStatus(UpdateBookingStatusDTO update)
        {
            var booking = await _bookingService.UpdateBookingStatusAsync(update);

            if (!booking.SuccessfulResult)
            {
                return ToErrorResponse(booking);
            }

            return Ok(booking);
        }

    }
}
