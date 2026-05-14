using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheGallop_Resort.Api.Data;
using TheGallop_Resort.Api.DTOs;

namespace TheGallop_Resort.Api.Services
{
    public class BookingService : IBookingService
    {
        private readonly GaloppDbContext _ctx;

        public BookingService(GaloppDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<ActionResult<IEnumerable<GetBookingResponseDTO>>> GetAllBookingsAsync()
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

            if (booking.Count == 0)
            {
                return null;
            }

            return booking;
        }
    }
}
