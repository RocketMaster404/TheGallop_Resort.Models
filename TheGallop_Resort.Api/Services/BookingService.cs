using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheGallop_Resort.Api.Data;
using TheGallop_Resort.Api.DTOs;
using TheGallop_Resort.Models.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TheGallop_Resort.Api.Services
{
    public class BookingService : IBookingService
    {
        private readonly GaloppDbContext _ctx;


        public BookingService(GaloppDbContext ctx)
        {
            _ctx = ctx;
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

        //DTOist
        public async Task<ServiceResult<GetFullBookingResponsDTO>> CreateBookingAsync(GetInputFromUserCreateDTO dto)
        {
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

            var bookingDTO = new CreateBookingDTO
            {
                GuestId = dto.GuestId,
            };

            var booking = new Booking
            {
                CreatedAt = DateTime.Now,
                GuestId = bookingDTO.GuestId,
                Status = Status.Confirmed,
                RoomReservations = new List<RoomReservation>()

            };

            await _ctx.Bookings.AddAsync(booking);
            await _ctx.SaveChangesAsync();


            var roomCategoryDTO = new AddCategoryToBookingDTO(dto.Type);

            var roomReservationDTO = new CreateRoomReservationDTO
                (
                booking.Id,
                dto.CheckIn.ToDateTime(TimeOnly.MinValue),
                dto.CheckOut.ToDateTime(TimeOnly.MinValue),
                dto.Adults,
                dto.Children,
                roomCategoryDTO.Type
                );

            var roomReservation = new RoomReservation
            {
                BookingId = booking.Id,
                CheckIn = roomReservationDTO.CheckIn,
                CheckOut = roomReservationDTO.CheckOut,
                RoomStatus = RoomStatus.Confirmed,
                Adults = roomReservationDTO.Adults,
                Children = roomReservationDTO.Children,
                RoomId = room.Id,

            };

            await _ctx.RoomReservations.AddAsync(roomReservation);
            await _ctx.SaveChangesAsync();

            var roomCatoegory = await _ctx.RoomCategories.FirstOrDefaultAsync(c => c.Id == room.RoomCategoryId);

            int nights = (int)(checkOut - checkIn).TotalDays;

            var categoryPrice = roomCatoegory.CategoryPrice;
            var pricePerNight = roomReservation.PricePerNight;

            var calculatedTotalPrice = (nights * pricePerNight) + categoryPrice;

            booking.TotalPrice = calculatedTotalPrice;
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
                    calculatedTotalPrice
                ))
            };

            return ServiceResult<GetFullBookingResponsDTO>.Ok(response);
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

        public async Task<ServiceResult<IEnumerable<GetBookingResponseDTO>>> GetBookingsForNextMonthAsync()
        {
            var today = DateTime.Now;

            var startOfNextMonth = new DateTime(today.Year, today.Month, 1).AddMonths(1);

            var endOfNextMonth = new DateTime(today.Year, today.Month, 1).AddMonths(2);


            var bookings = await _ctx.Bookings
                .AsNoTracking()
                .Where(b => b.RoomReservations.Any(r => r.CheckIn >= startOfNextMonth && r.CheckOut <= endOfNextMonth))
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

        public async Task<ServiceResult<IEnumerable<GetBookingResponseDTO>>> GetBookingsForSpecifikDateAsync(DateOnly inputDate)
        {
            var date = inputDate.ToDateTime(TimeOnly.MinValue);

            var bookings = await _ctx.Bookings
                .AsNoTracking()
                .Where(b => b.RoomReservations.Any(r => date >= r.CheckIn && date <= r.CheckOut))
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

        public async Task<ServiceResult> DeleteBookingByIdAsync(int bookingId)
        {
            var booking = await _ctx.Bookings
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == bookingId);


            if (booking is null)
            {
                return ServiceResult.NotFound("No bookings were found.");
            }

            _ctx.Bookings.Remove(booking);

            await _ctx.SaveChangesAsync();

            return ServiceResult.Ok();
        }

    }
}
