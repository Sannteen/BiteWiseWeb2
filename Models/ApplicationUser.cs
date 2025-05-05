using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace BiteWiseWeb2.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Full Name")]
        public required string FullName { get; set; }

        public int? Age { get; set; }

        public double? Weight { get; set; }

        public double? Height { get; set; }

        [Display(Name = "Profile Picture")]
        public required string ProfilePicturePath { get; set; }

        public required string Gender { get; set; }

        [Display(Name = "Fitness Goal")]
        public required string FitnessGoal { get; set; }

        [Display(Name = "Activity Level")]
        public required string ActivityLevel { get; set; }
    }
}
