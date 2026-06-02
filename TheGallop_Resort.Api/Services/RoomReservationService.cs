using Microsoft.EntityFrameworkCore;
using TheGallop_Resort.Api.Data;
using TheGallop_Resort.Api.DTOs;
using TheGallop_Resort.Models.Models;

namespace TheGallop_Resort.Api.Services
{
    public class RoomReservationService : IRoomReservationService
    {
        private readonly GaloppDbContext _ctx;

        public RoomReservationService(GaloppDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<ServiceResult<CreateRoomReservationDTO>> CreateRoomReservationAsync(CreateRoomReservationDTO dto)
        {
            var booking = await _ctx.Bookings
               .FirstOrDefaultAsync(b => b.Id == dto.bookingId);

            if (booking == null)
            {
                return ServiceResult<CreateRoomReservationDTO>.NotFound($"Booking with id {dto.bookingId} was not found.");
            }
            var room = await _ctx.Rooms
                .Where(r => r.RoomCategory.Type == dto.Type)
                .Where(r => !r.RoomReservations.Any(rr =>
                    dto.CheckIn < rr.CheckOut &&
                    dto.CheckOut > rr.CheckIn))
                .FirstOrDefaultAsync();

            if (room == null)
            {
                return ServiceResult<CreateRoomReservationDTO>.NotFound($"There are no available rooms of type {dto.Type} on chosen date.");
            }

            var roomCatoegory = await _ctx.RoomCategories.FirstOrDefaultAsync(c => c.Id == room.RoomCategoryId);
            var roomReservationDb = await _ctx.RoomReservations.FirstOrDefaultAsync(rr => rr.RoomId == room.Id);

            int nights = (int)(dto.CheckOut - dto.CheckIn).TotalDays;

            var categoryPrice = roomCatoegory.CategoryPrice;
            var pricePerNight = roomReservationDb.PricePerNight;

            var calculatedTotalPrice = (nights * pricePerNight) + categoryPrice;

            booking.TotalPrice = calculatedTotalPrice;
            await _ctx.SaveChangesAsync();

            var roomReservation = new CreateRoomReservationDTO
           (
               dto.bookingId,
               dto.CheckIn,
               dto.CheckOut,
               dto.Adults,
               dto.Children,
               dto.Type
           );

            booking.TotalPrice += calculatedTotalPrice;
            _ctx.Bookings.Update(booking);

            await _ctx.SaveChangesAsync();

            return ServiceResult<CreateRoomReservationDTO>.Ok(roomReservation);
        }
    }
}
