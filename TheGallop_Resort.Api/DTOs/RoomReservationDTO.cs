namespace TheGallop_Resort.Api.DTOs
{
    //Ändra till room-dto ist för int roomId??
    public record GetRoomReservationResponseDTO(int Id, int RoomId, DateTime CheckIn, DateTime CheckOut);

}

