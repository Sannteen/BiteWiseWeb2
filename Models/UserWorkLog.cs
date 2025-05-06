using System;
using System.Collections.Generic;

namespace BiteWiseWeb2.Models;

public partial class UserWorkLog
{
    public string UserName { get; set; } = null!;

    public string ExerciseName { get; set; } = null!;

    public string? Category { get; set; }

    public decimal? TotalCaloriesBurnt { get; set; }

    public decimal? Duration { get; set; }

    public DateOnly? Date { get; set; }
}
