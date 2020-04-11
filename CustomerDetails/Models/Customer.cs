using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CustomerDetails.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Invalid Format")]
        public string PhoneNumber { get; set; }

    }
}