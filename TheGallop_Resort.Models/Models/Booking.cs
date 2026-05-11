using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TheGallop_Resort.Models.Models
{
    public class Booking
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        public Status? Status { get; set; }
        public decimal TotalPrice { get; set; }

        public int GuestId { get; set; }

        public Guest Guests { get; set; } = null!;

        public ICollection<RoomReservation> RoomReservations { get; set; } = new List<RoomReservation>();
    }
    public enum Status
    {
        Booked,
        Preliminary,
        Cancelled,
        Completed
    }

}



