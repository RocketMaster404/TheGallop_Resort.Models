using System.ComponentModel.DataAnnotations;

namespace TheGallop_Resort.Api.DTOs
{
    public record CreateRoomDTO
    {
        // Add this validation into a Validator like Erik's
        [Range(1001, 1301, ErrorMessage = "Room number must be between 1001-1301")]

        public int RoomNr { get; init; }
        public int RoomCategoryId { get; init; }
    }

    public record RoomInfoDTO(int Id, int RoomNr, int RoomCategoryId);

    public record CreateRoomForBookingDTO (int RoomNr);
}
