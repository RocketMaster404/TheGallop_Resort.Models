using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheGallop_Resort.Api.Data;
using TheGallop_Resort.Api.DTOs;
using TheGallop_Resort.Models.Models;

namespace TheGallop_Resort.Api.Services
{
    public class BookingService : IBookingService
    {
        private readonly GaloppDbContext _ctx;
        private readonly IGuestService _iGuestService;


        public BookingService(GaloppDbContext ctx, IGuestService iGuestService)
        {
            _ctx = ctx;
            _iGuestService = iGuestService;
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
                    RoomReservation = b.RoomReservations.Select(r => new GetRoomReservationResponseDTO
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

        public async Task<ServiceResult<Booking>> AddBookingAsync(int guestId)
        {
            try
            {
                await _iGuestService.GetGuestInfoByIdAsync(guestId);
            }
            catch
            {
                return ServiceResult<Booking>.ValidationError($"No guest with id {guestId} was found!");
            }

            var booking = new Booking
            {
                GuestId = guestId
            };

            await _ctx.Bookings.AddAsync(booking);
            await _ctx.SaveChangesAsync();

            return ServiceResult<Booking>.Ok(booking);
        }
    }
}
