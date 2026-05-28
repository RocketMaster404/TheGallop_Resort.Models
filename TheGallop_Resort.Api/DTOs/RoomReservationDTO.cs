using TheGallop_Resort.Models.Models;

namespace TheGallop_Resort.Api.DTOs
{
    //Ändra till room-dto ist för int roomId??
    public record GetRoomReservationResponseDTO(int Id, int RoomId, DateTime CheckIn, DateTime CheckOut);

    public record CreateRoomReservationDTO (int bookingId, DateTime CheckIn, DateTime CheckOut, int Adults, int Children, RoomType Type );

    public record GetFullRoomReservationResponse(int Id, RoomType Type, DateOnly CheckIn, DateOnly CheckOut, int RoomNr, int Adults,int Children, decimal PricePerNight);
    
}

