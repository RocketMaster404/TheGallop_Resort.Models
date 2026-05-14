using TheGallop_Resort.Models.Models;

namespace TheGallop_Resort.Api.DTOs
{

    public record GetBookingResponseDTO
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal TotalPrice { get; set; }
        public Status Status { get; set; }
        public GuestInfoDTO Guests { get; set; }
        public IEnumerable<GetRoomReservationResponseDTO> RoomReservation { get; set; }
    }

    

}
