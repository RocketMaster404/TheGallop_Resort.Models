using System.Text.Json.Serialization;
using TheGallop_Resort.Models.Models;

namespace TheGallop_Resort.Api.DTOs
{

    public record GetBookingResponseDTO
    {
        public int Id { get; init; }
        public DateTime CreatedAt { get; init; }
        public decimal TotalPrice { get; init; }
        public Status Status { get; init; }
        public GuestInfoDTO Guest { get; init; }
        public IEnumerable<GetRoomReservationResponseDTO> RoomReservation { get; init; }
    }

    public record SearchBookingByIdDTO(int bookingId);

    public record UpdateBookingGuestDTO(int bookingId, int guestId);

    public record UpdateBookingStatusDTO(int BookingId, Status Status);
    public record BookingDetailsDTO
    {
        public int Id { get; init; }
        public decimal Totalprice { get; init; }
        public Status Status { get; init; }
        public DateTime CreatedAt { get; init; }
    }
}
