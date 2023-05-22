using Service_Demo.Entites.DataEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Demo.Entites.Models
{
    public class User : AuditableEntity<long>
    {
        [Required]
        public string FirstName { get; set; }  = null!;
        [Required]
        public string LastName { get; set; } = null!;
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        public long PhoneNumber { get; set; } = 0;
        [Required]
        public string Avatar { get; set; } = "/Image/user1.png";
        public string? WhyIVolunteer { get; set; }
        public string? EmployeeId { get; set; }
        public string? Department { get; set; }
        public long? CityId { get; set; }
        public long? CountryId { get; set; }
        public string? ProfileText { get; set; }
        public string? LinkedInUrl { get; set; }
        public string? Title { get; set; }
        public bool? Status { get; set; }
        public string? Availablity { get; set; }
        public string? Role { get; set; }

    }
}
