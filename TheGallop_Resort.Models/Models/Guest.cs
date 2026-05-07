using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGallop_Resort.Models.Models
{
    public class Guest
    {
        public int Id { get; set; }

        [MinLength(2, ErrorMessage = "Minimum of 2 character is Required")]
        [MaxLength(50, ErrorMessage = "Maximum of 50 Character")]

        public string FirstName { get; set; } = null!;

        [MinLength(2, ErrorMessage = "Minimum of 2 character is Required")]
        [MaxLength(50, ErrorMessage = "Maximum of 50 Character")]

        public string LastName { get; set; } = null!;

        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$",
        ErrorMessage = "Not a valid fomrat for Email")]
        public string Email { get; set; } = null!;

        [MinLength(2, ErrorMessage = "Minimum of 2 characters")]
        [MaxLength(20, ErrorMessage = "Maximum of 20 characters")]
        public string PhoneNumber { get; set; } = null!;

        

        public ICollection<Reservation> Reservations = new List<Reservation>();


    }
}
