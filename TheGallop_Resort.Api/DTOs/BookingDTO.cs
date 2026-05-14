using System.Text.Json.Serialization;
using TheGallop_Resort.Models.Models;

namespace TheGallop_Resort.Api.DTOs
{

    public record GetBookingResponseDTO
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal TotalPrice { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Status Status { get; set; }
        public GuestInfoDTO Guest { get; set; }
        public IEnumerable<GetRoomReservationResponseDTO> RoomReservation { get; set; }
    }

    public record UpdateBookingGuestDTO(int bookingId, int guestId);

    public record UpdateBookingStatusDTO
    {
        public int BookingId { get; set; }
        public Status Status { get; set; }
    }
}
