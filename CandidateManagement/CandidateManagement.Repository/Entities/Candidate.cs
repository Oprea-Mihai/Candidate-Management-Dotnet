using CandidateManagement.Repository.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateManagement.Repository.Entities
{
    public class Candidate
    {
        [Key]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string? PhoneNumber { get; set; }


        [Time(ErrorMessage = "Invalid time format.")]
        public string? CallAvailabilityStart { get; set; }

        [Time(ErrorMessage = "Invalid time format.")]
        public string? CallAvailabilityEnd { get; set; }

        public string? LinkedInUrl { get; set; }

        public string? GitHubUrl { get; set; }

        [Required]
        public string FreeTextComment { get; set; }
    }
}
