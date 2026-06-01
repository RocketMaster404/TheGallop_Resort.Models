using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
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
        public async Task<ServiceResult<GuestInfoWithBookingDTO>> GetGuestBookingHistoryAsync(int guestId)
        {
            var guest = await _ctx.Guests
                .Where(g => g.Id == guestId)
                .Select(g => new GuestInfoWithBookingDTO(
                    g.FirstName,
                    g.LastName,
                    g.Email,
                    g.PhoneNumber,

                    g.Bookings
                        .Where(b => b.RoomReservations
                            .Any(rr => rr.CheckOut < DateTime.Now))

                        .Select(b => new GuestBookingInfoDTO(
                            b.Id,
                            b.CreatedAt,
                            b.TotalPrice,

                            b.RoomReservations
                                .Where(rr => rr.CheckOut < DateTime.Now)

                                .Select(rr => new GuestRoomReservationInfoDTO(
                                    rr.Id,
                                    rr.CheckIn,
                                    rr.CheckOut,
                                    rr.RoomStatus,
                                    rr.Adults,
                                    rr.Children,
                                    rr.PricePerNight
                                ))
                                .ToList()
                        ))
                        .ToList()
                ))
                .FirstOrDefaultAsync();

            if (guest is null)
            {
                return ServiceResult<GuestInfoWithBookingDTO>
                    .ValidationError("Guest not found");
            }

            if (!guest.Bookings.Any())
            {
                return ServiceResult<GuestInfoWithBookingDTO>
                    .ValidationError("No booking history");
            }

            return ServiceResult<GuestInfoWithBookingDTO>
                .Ok(guest);
        }


        public async Task<ServiceResult<GuestInfoWithBookingDTO>> GetGuestFutureBookingsAsync(int guestId)
        {
            var guest = await _ctx.Guests
                .Where(g => g.Id == guestId)

                .Select(g => new GuestInfoWithBookingDTO(
                    g.FirstName,
                    g.LastName,
                    g.Email,
                    g.PhoneNumber,

                    g.Bookings
                        .Where(b => b.RoomReservations
                            .Any(rr => rr.CheckIn > DateTime.Now))

                        .Select(b => new GuestBookingInfoDTO(
                            b.Id,
                            b.CreatedAt,
                            b.TotalPrice,

                            b.RoomReservations
                                .Where(rr => rr.CheckIn > DateTime.Now)

                                .Select(rr => new GuestRoomReservationInfoDTO(
                                    rr.Id,
                                    rr.CheckIn,
                                    rr.CheckOut,
                                    rr.RoomStatus,
                                    rr.Adults,
                                    rr.Children,
                                    rr.PricePerNight
                                ))
                                .ToList()
                        ))
                        .ToList()
                ))
                .FirstOrDefaultAsync();

            if (guest is null)
            {
                return ServiceResult<GuestInfoWithBookingDTO>
                    .ValidationError("Guest not found");
            }

            if (!guest.Bookings.Any())
            {
                return ServiceResult<GuestInfoWithBookingDTO>
                    .ValidationError("No future reservations");
            }

            return ServiceResult<GuestInfoWithBookingDTO>
                .Ok(guest);
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
            var guest = await _ctx.Guests
                .Where(g => g.Id == guestId)
                .AsNoTracking()

                .Select(g => new GuestInfoWithBookingDTO(
                    g.FirstName,
                    g.LastName,
                    g.Email,
                    g.PhoneNumber,

                    g.Bookings.Select(b => new GuestBookingInfoDTO(
                        b.Id,
                        b.CreatedAt,
                        b.TotalPrice,

                        b.RoomReservations.Select(rr => new GuestRoomReservationInfoDTO(
                            rr.Id,
                            rr.CheckIn,
                            rr.CheckOut,
                            rr.RoomStatus,
                            rr.Adults,
                            rr.Children,
                            rr.PricePerNight
                        ))
                        .ToList()
                    ))
                    .ToList()
                ))
                .FirstOrDefaultAsync();

            if (guest == null)
            {
                return ServiceResult<GuestInfoWithBookingDTO>
                    .NotFound("Guest Not Found");
            }

            return ServiceResult<GuestInfoWithBookingDTO>
                .Ok(guest);
        }

        public async Task<ServiceResult> DeleteGuestAsync(int guestId)
        {

            var guestToDelete = await _ctx.Guests.FirstOrDefaultAsync(g => g.Id == guestId);


            if (guestToDelete == null)
            {
                return ServiceResult.NotFound("Guest not found");
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
