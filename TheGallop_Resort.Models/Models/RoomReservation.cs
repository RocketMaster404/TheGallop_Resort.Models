using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGallop_Resort.Models.Models
{
    public class RoomReservation
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public int BookingId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public RoomStatus Status { get; set; }

        [Range(1, 100, ErrorMessage = "At least 1 adult is required")]
        public int Adults { get; set; }
        [Range(0, 100, ErrorMessage = "Number of children cannot be negative")]
        public int Children { get; set; }
        public decimal PricePerNight { get; set; }
        public Booking Booking { get; set; } = null!;
        public Room Room { get; set; } = null!;
    }

    public enum RoomStatus
    {
        Confirmed,
        Cancelled,
        Preliminary
    }
}
