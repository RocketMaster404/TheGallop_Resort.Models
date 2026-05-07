using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGallop_Resort.Models.Models
{
    public class ReservationRoom
    {
        public int ReservationId { get; set; }
        public int RoomId { get; set; }

        public Reservation Reservation { get; set; } = null!;
        public Room Room { get; set; } = null!;

    }
}
