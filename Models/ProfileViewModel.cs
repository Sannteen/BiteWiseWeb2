using System.ComponentModel.DataAnnotations;

namespace BiteWiseWeb2.Models
{
    public class ProfileViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Full Name")]
        public string Name { get; set; } = string.Empty;

        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string Gender { get; set; } = string.Empty;

        [Display(Name = "Height (cm)")]
        public float Height { get; set; }

        [Display(Name = "Weight (kg)")]
        public float Weight { get; set; }

        [Display(Name = "Fitness Goal")]
        public string FitnessGoal { get; set; } = string.Empty;

        [Display(Name = "Activity Level")]
        public string ActivityLevel { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Display(Name = "Current Password")]
        public string CurrentPassword { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; } = string.Empty;
    }
}

