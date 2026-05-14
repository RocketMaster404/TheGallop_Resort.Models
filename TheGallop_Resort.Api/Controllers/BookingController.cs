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
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        //private readonly GaloppDbContext _ctx;

        //public BookingController(GaloppDbContext ctx)
        //{
        //    _ctx = ctx;
        //}

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

        //[HttpPost("AddBooking", Name = "AddBooking")]
        //public async Task<IActionResult> AddBooking(int guestId)
        //{

        //    var booking = new Booking
        //    {
        //        GuestId = guestId
        //    };

        //    await _ctx.Bookings.AddAsync(booking);
        //    await _ctx.SaveChangesAsync();

        //    return Ok(booking);
        //}
    }
}
