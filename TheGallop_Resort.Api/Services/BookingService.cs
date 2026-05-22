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

        public async Task<ServiceResult<IEnumerable<GetBookingResponseDTO>>> GetAllBookingsAsync()
        {
            var bookings = await _ctx.Bookings
                .AsNoTracking()
                .Select(b => new GetBookingResponseDTO
                {
                    Id = b.Id,
                    CreatedAt = b.CreatedAt,
                    TotalPrice = b.TotalPrice,
                    Status = b.Status,
                    Guest = new GuestInfoDTO(
                        b.Guest.FirstName,
                        b.Guest.LastName,
                        b.Guest.Email,
                        b.Guest.PhoneNumber
                    ),
                    RoomReservation = b.RoomReservations.Select(r => new GetRoomReservationResponseDTO
                  (
                      r.Id,
                      r.RoomId,
                      r.CheckIn,
                      r.CheckOut
                  ))
                }).ToListAsync();

            if (bookings.Count == 0)
            {
                return ServiceResult<IEnumerable<GetBookingResponseDTO>>.NotFound("No bookings were found.");
            }

            return ServiceResult<IEnumerable<GetBookingResponseDTO>>.Ok(bookings);
        }

        public async Task<ServiceResult<GetBookingResponseDTO>> GetBookingByIdAsync(int bookingId)
        {
            var bookings = await _ctx.Bookings
                .AsNoTracking()
                .Where(b => b.Id == bookingId)
                .Select(b => new GetBookingResponseDTO
                {
                    Id = b.Id,
                    CreatedAt = b.CreatedAt,
                    TotalPrice = b.TotalPrice,
                    Status = b.Status,
                    Guest = new GuestInfoDTO(
                        b.Guest.FirstName,
                        b.Guest.LastName,
                        b.Guest.Email,
                        b.Guest.PhoneNumber
                    ),
                    RoomReservation = b.RoomReservations.Select(r => new GetRoomReservationResponseDTO
                  (
                      r.Id,
                      r.RoomId,
                      r.CheckIn,
                      r.CheckOut
                  ))
                }).FirstOrDefaultAsync();

            if (bookings is null)
            {
                return ServiceResult<GetBookingResponseDTO>.NotFound("No bookings were found.");
            }

            return ServiceResult<GetBookingResponseDTO>.Ok(bookings);
        }

        public async Task<ServiceResult<Booking>> AddBookingAsync(int guestId)
        {
            if (guestId <= 0)
            {
                return ServiceResult<Booking>.ValidationError($"GuestId can't be a negative number.");
            }

            try
            {
                await _iGuestService.GetGuestInfoByIdAsync(guestId);
            }
            catch
            {
                return ServiceResult<Booking>.NotFound($"No guest with id {guestId} was found!");
            }

            var booking = new Booking
            {
                GuestId = guestId
            };

            await _ctx.Bookings.AddAsync(booking);
            await _ctx.SaveChangesAsync();

            return ServiceResult<Booking>.Ok(booking);
        }



        public async Task<ServiceResult> UpdateGuestOnBookingAsync(UpdateBookingGuestDTO update)
        {
            var guestExist = await _ctx.Guests.AnyAsync(g => g.Id == update.guestId);

            if (!guestExist)
            {
                return ServiceResult.NotFound($"Guest with id {update.guestId} was not found!");
            }

            var booking = await _ctx.Bookings.FirstOrDefaultAsync(b => b.Id == update.bookingId);

            if (booking == null)
            {
                return ServiceResult.NotFound($"Booking with id {update.bookingId} was not found!");
            }

            booking.GuestId = update.guestId;


            _ctx.Bookings.Update(booking);
            await _ctx.SaveChangesAsync();

            return ServiceResult.Ok();
        }

        public async Task<ServiceResult> UpdateBookingStatusAsync(UpdateBookingStatusDTO update)
        {
            var booking = await _ctx.Bookings.FirstOrDefaultAsync(b => b.Id == update.BookingId);

            if (booking == null)
            {
                return ServiceResult.NotFound($"Booking with id {update.BookingId} was not found!");
            }

            booking.Status = update.Status;

            _ctx.Bookings.Update(booking);
            await _ctx.SaveChangesAsync();

            return ServiceResult.Ok();
        }
    }
}
