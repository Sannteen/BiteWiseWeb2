namespace BiteWiseWeb2.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public float Height { get; set; }
        public float Weight { get; set; }
        public string FitnessGoal { get; set; } = string.Empty;
        public string ActivityLevel { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
    }
}
