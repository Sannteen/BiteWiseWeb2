using System;
using System.Collections.Generic;

namespace BiteWiseWeb2.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int? Age { get; set; }

    public string? Gender { get; set; }

    public decimal Height { get; set; }

    public decimal? Weight { get; set; }

    public string? ActivityLevel { get; set; }

    //public string? FitnessGoal { get; set; }

 
    public string? Goal { get; set; }

    public short? LockStatus { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? LoginAttempts { get; set; }

    public virtual ICollection<DailySummary> DailySummaries { get; set; } = new List<DailySummary>();

    public virtual ICollection<FoodLog> FoodLogs { get; set; } = new List<FoodLog>();

    public virtual ICollection<Food> Foods { get; set; } = new List<Food>();

    public virtual ICollection<Goal> Goals { get; set; } = new List<Goal>();

    public virtual ICollection<Meal> Meals { get; set; } = new List<Meal>();

    public virtual ICollection<UserMeasurement> UserMeasurements { get; set; } = new List<UserMeasurement>();

    public virtual UserPicture? UserPicture { get; set; }

    public virtual ICollection<UserPreference> UserPreferences { get; set; } = new List<UserPreference>();

    public virtual ICollection<WorkoutLog> WorkoutLogs { get; set; } = new List<WorkoutLog>();
  //  public string PasswordHash { get; internal set; }
}
