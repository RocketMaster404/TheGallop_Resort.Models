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

        [EmailAddress(ErrorMessage = "Invalid Email format")]
        public string Email { get; set; } = null!;

        [Phone(ErrorMessage = "Invalid phoneNumber format")]
        public string PhoneNumber { get; set; } = null!;

        

        public ICollection<Booking> Bookings = new List<Booking>();


    }
}
