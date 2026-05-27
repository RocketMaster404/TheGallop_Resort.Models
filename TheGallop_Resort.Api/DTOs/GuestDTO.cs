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

    public record GuestInfoDTO(string FirstName, string LastName, string Email, string Phone);
    public record GuestInfoWithBookingDTO(string FirstName, string LastName, string Email, string Phone,List<BookingDetailsDTO> Bookings);
}
