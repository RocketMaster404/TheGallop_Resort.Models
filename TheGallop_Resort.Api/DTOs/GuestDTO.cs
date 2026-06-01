using System.ComponentModel.DataAnnotations;
using TheGallop_Resort.Models.Models;

namespace TheGallop_Resort.Api.DTOs
{
    public record CreateGuestDTO
    {
       
        public string FirstName { get; init; }
        
        public string LastName { get; init; }
        
        public string Email { get; init; }
       
        public string Phone { get; init; }

    }

    public record UpdateGuestInfoDTO
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Email { get; init; }
        public string Phone { get; init; }
    }

    
    public record GuestInfoWithBookingDTO(string FirstName, string LastName, string Email, string Phone,List<GuestBookingInfoDTO> Bookings);

    public record GuestInfoDTO(string FirstName, string LastName, string Email, string Phone);
    public record GuestBookingInfoDTO(int BookingId,DateTime CreatedAt,decimal TotalPrice, IEnumerable<GuestRoomReservationInfoDTO> RoomReservations);
    public record GuestRoomReservationInfoDTO(int RoomReservationId, DateTime CheckIn, DateTime CheckOut, RoomStatus? RoomStatus,int Adults, int Children, decimal PricePerNight);
}
