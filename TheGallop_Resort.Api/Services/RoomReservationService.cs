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

        //public async Task<ServiceResult<GetFullBookingResponsDTO>> CreateRoomReservationAsync(CreateNewRoomReservationWithBookingIdDTO dto)
        //{
        //    var booking = await _ctx.Bookings
        //       .FirstOrDefaultAsync(r => r.Id == dto.bookingId);

        //    if (booking == null)
        //    {
        //        return ServiceResult<GetFullBookingResponsDTO>.NotFound($"Booking with id {dto.bookingId} was not found.");
        //    }

        //    var checkIn = dto.CheckIn.ToDateTime(TimeOnly.MinValue);
        //    var checkOut = dto.CheckOut.ToDateTime(TimeOnly.MinValue);

        //    var roomCategoryDTO = new AddCategoryToBookingDTO(dto.Type);

        //    var booking = new CreateNewRoomReservationWithBookingIdDTO
        //(
        //    dto.bookingId,
        //    dto.CheckIn,
        //    dto.CheckOut,
        //    dto.Adults,
        //    dto.Children,
        //    roomCategoryDTO.Type
        //);

        //    var roomReservation = new RoomReservation
        //    {
        //        BookingId = dto.bookingId,
        //        CheckIn = dto.CheckIn.ToDateTime(TimeOnly.MinValue),
        //        CheckOut = dto.CheckOut.ToDateTime(TimeOnly.MinValue),
        //        RoomStatus = RoomStatus.Confirmed,
        //        Adults = dto.Adults,
        //        Children = dto.Children,
        //        RoomId = room.Id,
        //    };

        //    await _ctx.RoomReservations.AddAsync(roomReservation);
        //    await _ctx.SaveChangesAsync();


        //}
    }
}
