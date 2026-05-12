using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using TheGallop_Resort.Api.Data;
using TheGallop_Resort.Api.DTOs;
using TheGallop_Resort.Models.Models;

namespace TheGallop_Resort.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly GaloppDbContext _ctx;

        public BookingController(GaloppDbContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet("getAllBookings", Name = "GetAllBooking")]
        public async Task<ActionResult<IEnumerable<GetBookingResponseDTO>>> GetAllBookings()
        {
            var booking = await _ctx.Bookings
                .AsNoTracking()
                .Select(b => new GetBookingResponseDTO
                {
                    Id = b.Id,
                    CreatedAt = b.CreatedAt,
                    TotalPrice = b.TotalPrice,
                    Status = b.Status,
                    Guests = new GuestInfoDTO(
                        b.Guests.FirstName,
                        b.Guests.LastName,
                        b.Guests.Email,
                        b.Guests.PhoneNumber
                    ),
                    Rooms = b.RoomReservations.Select(r => new GetRoomReservationResponseDTO
                  (
                      r.Id,
                      r.RoomId,
                      r.CheckIn,
                      r.CheckOut
                  ))
                }).ToListAsync();

            return Ok(booking);
        }

        [HttpPost("AddBooking", Name = "AddBooking")]
        public async Task<IActionResult> AddBooking(CreateBookingDTO dto)
        {

            var booking = new Booking
            {
                GuestId = dto.GuestId,
            };

            await _ctx.Bookings.AddAsync(booking);
            await _ctx.SaveChangesAsync();

            return Ok(booking);
        }
    }
}
