using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGallop_Resort.Models.Models
{
    [Index(nameof(RoomNr),IsUnique = true)]
    public class Room
    {
        public int Id { get; set; }

        [Range(1001, 1301, ErrorMessage = "Room number must be between 1001-1301")]
        
        public int RoomNr { get; set; } 
        
        public int RoomCategoryId { get; set; }

        public RoomCategory RoomCategory { get; set; } = null!;

        public ICollection<RoomReservation> RoomReservations { get; set; } = new List<RoomReservation>();
    }
}
