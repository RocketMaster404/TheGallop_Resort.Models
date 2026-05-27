namespace TheGallop_Resort.Api.DTOs
{
    //Ändra till room-dto ist för int roomId??
    public record GetRoomReservationResponseDTO(int Id, int RoomId, DateTime CheckIn, DateTime CheckOut);

    public record CreateRoomReservationDTO (int bookingId, DateTime CheckIn, DateTime CheckOut, int Adults, int Children);

    public record GetFullRoomReservationResponse(int Id, int RoomId, DateTime CheckIn, DateTime CheckOut, int Adults,int Children, decimal PricePerNight);
    
}

