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

        public async Task<ServiceResult<GetFullBookingResponsDTO>> CreateRoomReservationAsync(CreateRoomReservationDTO dto)
        {
            var booking = await _ctx.Bookings
               .FirstOrDefaultAsync(b => b.Id == dto.bookingId);

            if (booking == null)
            {
                return ServiceResult<GetFullBookingResponsDTO>.NotFound($"Booking with id {dto.bookingId} was not found.");
            }

            var checkIn = dto.CheckIn.ToDateTime(TimeOnly.MinValue);
            var checkOut = dto.CheckOut.ToDateTime(TimeOnly.MinValue);

            var room = await _ctx.Rooms
                .Where(r => r.RoomCategory.Type == dto.Type)
                .Where(r => !r.RoomReservations.Any(rr =>
                    checkIn < rr.CheckOut &&
                    checkOut > rr.CheckIn))
                .FirstOrDefaultAsync();

            if (room == null)
            {
                return ServiceResult<GetFullBookingResponsDTO>.NotFound($"There are no available rooms of type {dto.Type} on chosen date.");
            }

            var roomCatoegory = await _ctx.RoomCategories.FirstOrDefaultAsync(c => c.Id == room.RoomCategoryId);
            var roomReservationDb = await _ctx.RoomReservations.FirstOrDefaultAsync(rr => rr.RoomId == room.Id);

            int nights = (int)(checkIn - checkOut).TotalDays;

            var roomReservationDTO = new CreateRoomReservationDTO
           (
               dto.bookingId,
               dto.CheckIn,
               dto.CheckOut,
               dto.Adults,
               dto.Children,
               dto.Type
           );

            var roomReservation = new RoomReservation
            {
                BookingId = roomReservationDTO.bookingId,
                RoomId = room.Id,
                CheckIn = checkIn,
                CheckOut = checkOut,
                RoomStatus = RoomStatus.Confirmed,
                Adults = roomReservationDTO.Adults,
                Children = roomReservationDTO.Children
            };

            var categoryPrice = roomCatoegory.CategoryPrice;
            var pricePerNight = roomReservation.PricePerNight;

            var calculatedTotalPrice = (nights * pricePerNight) + categoryPrice;

            booking.TotalPrice = calculatedTotalPrice;
            await _ctx.SaveChangesAsync();


            booking.TotalPrice += calculatedTotalPrice;
            _ctx.Bookings.Update(booking);

            await _ctx.SaveChangesAsync();

            var response = new GetFullBookingResponsDTO
            {
                Id = booking.Id,
                CreatedAt = booking.CreatedAt,
                Status = booking.Status,
                TotalPrice = calculatedTotalPrice,
                GuestId = booking.GuestId,

                RoomReservations = booking.RoomReservations.Select(r => new GetFullRoomReservationResponse
                (
                    r.Id,
                    dto.Type,
                    DateOnly.FromDateTime(r.CheckIn),
                    DateOnly.FromDateTime(r.CheckOut),
                    r.Room.RoomNr,
                    r.Adults,
                    r.Children,
                    r.PricePerNight
                ))
            };

            return ServiceResult<GetFullBookingResponsDTO>.Ok(response);
        }
    }
}
