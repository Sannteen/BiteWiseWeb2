using System;
using System.Collections.Generic;

namespace BiteWiseWeb2.Models;

public partial class Exercise
{
    public int ExerciseId { get; set; }

    public string Name { get; set; } = null!;

    public string? Category { get; set; }

    public decimal CaloriesBurnedPerMin { get; set; }

    public virtual ICollection<WorkoutLog> WorkoutLogs { get; set; } = new List<WorkoutLog>();
}
