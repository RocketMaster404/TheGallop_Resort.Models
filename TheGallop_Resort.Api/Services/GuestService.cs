using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheGallop_Resort.Api.Data;
using TheGallop_Resort.Api.DTOs;
using TheGallop_Resort.Models.Models;

namespace TheGallop_Resort.Api.Services
{
    public class GuestService : IGuestService
    {

        private readonly GaloppDbContext _ctx;

        public GuestService(GaloppDbContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<ServiceResult<List<GetBookingResponseDTO>>> GetGuestBookingHistoryAsync(int guestId)
        {
            var guestCheck = await _ctx.Guests.AnyAsync(g => g.Id == guestId);

            if (!guestCheck)
            {
                return ServiceResult<List<GetBookingResponseDTO>>
                .ValidationError("Guest not found");
            }

            var bookings = await _ctx.Bookings
                .Where(b => b.GuestId == guestId &&
                            b.RoomReservations.Any(rr => rr.CheckOut < DateTime.Now))
                .Select(b => new GetBookingResponseDTO
                {
                    Id = b.Id,
                    CreatedAt = b.CreatedAt,
                    TotalPrice = b.TotalPrice,
                    Status = b.Status,

                    Guest = new GuestInfoDTO
                    (

                        b.Guest.FirstName,
                        b.Guest.LastName,
                        b.Guest.Email,
                        b.Guest.PhoneNumber
                    ),

                    RoomReservation = b.RoomReservations
                        .Where(rr => rr.CheckOut < DateTime.Now)
                        .Select(rr => new GetRoomReservationResponseDTO
                        (
                            rr.Id,
                            rr.RoomId,
                            rr.CheckIn,
                            rr.CheckOut
                        ))
                })
                .ToListAsync();

            if(!bookings.Any())
            {
                return ServiceResult<List<GetBookingResponseDTO>>
                .ValidationError("Not booking history");
            }
            

            return ServiceResult<List<GetBookingResponseDTO>>.Ok(bookings);

        }

        public async Task<ServiceResult<List<GetBookingResponseDTO>>> GetGuestFutureBookingsAsync(int guestId)
        {
            var guestCheck = await _ctx.Guests.AnyAsync(g => g.Id == guestId);

            if (!guestCheck)
            {
                return ServiceResult<List<GetBookingResponseDTO>>
                .ValidationError("Guest not found");
            }

            var bookings = await _ctx.Bookings
                .Where(b => b.GuestId == guestId &&
                            b.RoomReservations.Any(rr => rr.CheckIn > DateTime.Now))
                .Select(b => new GetBookingResponseDTO
                {
                    Id = b.Id,
                    CreatedAt = b.CreatedAt,
                    TotalPrice = b.TotalPrice,
                    Status = b.Status,

                    Guest = new GuestInfoDTO
                    (

                        b.Guest.FirstName,
                        b.Guest.LastName,
                        b.Guest.Email,
                        b.Guest.PhoneNumber
                    ),

                    RoomReservation = b.RoomReservations
                        .Where(rr => rr.CheckIn > DateTime.Now)
                        .Select(rr => new GetRoomReservationResponseDTO
                        (
                            rr.Id,
                            rr.RoomId,
                            rr.CheckIn,
                            rr.CheckOut
                        ))
                })
                .ToListAsync();

            if (!bookings.Any())
            {
                return ServiceResult<List<GetBookingResponseDTO>>
                .ValidationError("No reservations");
            }


            return ServiceResult<List<GetBookingResponseDTO>>.Ok(bookings);

        }




        public async Task<ServiceResult<Guest>> AddGuestAsync(CreateGuestDTO dto)
        {
            var guest = new Guest
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PhoneNumber = dto.Phone
            };

            bool emailMatch = await _ctx.Guests.AnyAsync(e => e.Email == dto.Email);

            if (emailMatch)
            {
                return ServiceResult<Guest>.ValidationError("Duplicated email");
            }



            await _ctx.Guests.AddAsync(guest);
            await _ctx.SaveChangesAsync();

            return ServiceResult<Guest>.Ok(guest);

        }


        public async Task<IEnumerable<Guest>> GetAllGuestsInfoAsync()
        {
            var guests = await _ctx.Guests.ToListAsync();

            return guests;
        }


        public async Task<ServiceResult<GuestInfoWithBookingDTO>> GetGuestInfoByIdAsync(int guestId)
        {

            var guest = await _ctx.Guests.Where(g => g.Id == guestId).AsNoTracking().Select(g => new GuestInfoWithBookingDTO(
               g.FirstName,
               g.LastName,
               g.Email,
               g.PhoneNumber,
               g.Bookings.Select(b => new BookingDetailsDTO
               {
                   Id = b.Id,
                   Totalprice = b.TotalPrice,
                   Status = (Status)b.Status,
                   CreatedAt = b.CreatedAt

               }).ToList()
               )).FirstOrDefaultAsync();

            if (guest == null)
            {
                return ServiceResult<GuestInfoWithBookingDTO>.NotFound("Guest Not Found");
            }



            return ServiceResult<GuestInfoWithBookingDTO>.Ok(guest);
        }

        public async Task<ServiceResult> DeleteGuestAsync(int guestId)
        {
            
            var guestToDelete = await _ctx.Guests
                                .Include(g => g.Bookings)
                                .ThenInclude(b => b.RoomReservations)
                                .FirstOrDefaultAsync(g => g.Id == guestId);


            if (guestToDelete == null)
            {
                return ServiceResult.NotFound("Guest not found");
            }

            foreach(var b in guestToDelete.Bookings)
            {
                b.Status = Status.Cancelled;
                b.TotalPrice = 0;     
                
                foreach(var r in b.RoomReservations)
                {
                    r.RoomStatus = (RoomStatus)Status.Cancelled;
                    r.PricePerNight = 0;
                }

            }



            _ctx.Guests.Remove(guestToDelete);
            await _ctx.SaveChangesAsync();

            return ServiceResult.Ok();

        }

        public async Task<ServiceResult> UpdateGuestInfoAsync(int guestId, UpdateGuestInfoDTO dto)
        {
            var guestUpdate = await _ctx.Guests.FirstOrDefaultAsync(g => g.Id == guestId);

            if (guestUpdate == null)
            {
                return ServiceResult.NotFound("Guest not found");
            }

            var emailCheck = await _ctx.Guests.AnyAsync(g => g.Email == dto.Email && g.Id != guestUpdate.Id);
            if (emailCheck)
            {
                return ServiceResult.ValidationError("Duplicated Email");
            }


            guestUpdate.FirstName = dto.FirstName;
            guestUpdate.LastName = dto.LastName;
            guestUpdate.Email = dto.Email;
            guestUpdate.PhoneNumber = dto.Phone;

            await _ctx.SaveChangesAsync();

            return ServiceResult.Ok();


        }






    }
}
