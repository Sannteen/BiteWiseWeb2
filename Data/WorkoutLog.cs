using System;
using System.Collections.Generic;

namespace BiteWiseWeb2.Data;

public partial class WorkoutLog
{
    public int LogId { get; set; }

    public int UserId { get; set; }

    public int? ExerciseId { get; set; }

    public decimal? Duration { get; set; }

    public decimal? CaloriesBurned { get; set; }

    public DateOnly? Date { get; set; }

    public virtual Exercise? Exercise { get; set; }

    public virtual User User { get; set; } = null!;
}
