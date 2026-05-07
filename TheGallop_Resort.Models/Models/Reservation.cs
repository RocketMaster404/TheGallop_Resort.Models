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
    public class Reservation
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }

        public Status? Status { get; set; }

        [Range(1, 100, ErrorMessage = "Det måste finnas minst 1 vuxen.")]
        public int Adults { get; set; }
        
        [Range(0, 100, ErrorMessage = "Det kan inte vara ett negativt antal barn.")]
        public int Children { get; set; }
        public int GuestId { get; set; } 

    }
    public enum Status
    {
        Bokad,
        Preliminär,
        Avbokad,
        Genomförd
    }

}



