using System.ComponentModel.DataAnnotations;
using TheGallop_Resort.Models.Models;

namespace TheGallop_Resort.Api.DTOs
{
    public record CreateGuestDTO
    {
        [MinLength(2, ErrorMessage = "Minimum of 2 character is Required")]
        [MaxLength(50, ErrorMessage = "Maximum of 50 Character")]
        public string FirstName { get; init; }
        [MinLength(2, ErrorMessage = "Minimum of 2 character is Required")]
        [MaxLength(50, ErrorMessage = "Maximum of 50 Character")]
        public string LastName { get; init; }
        [EmailAddress(ErrorMessage = "Invalid Email format")]
        public string Email { get; init; }
        [Phone(ErrorMessage = "Invalid phoneNumber format")]
        public string Phone { get; init; }

    }

    public record GuestInfoDTO(string FirstName, string LastName, string Email,string Phone);
    public record GuestInfoWithBookingDTO(string FirstName, string LastName, string Email, string Phone,List<BookingDetailsDTO> Bookings);
}
