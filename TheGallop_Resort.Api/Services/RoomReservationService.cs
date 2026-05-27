using TheGallop_Resort.Api.Data;

namespace TheGallop_Resort.Api.Services
{
    public class RoomReservationService : IRoomReservationService
    {
        private readonly GaloppDbContext _galoppDbContext;

        public RoomReservationService(GaloppDbContext ctx)
        {
            _galoppDbContext = ctx;
        }

        

       

    }
}
