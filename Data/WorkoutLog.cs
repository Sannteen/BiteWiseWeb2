using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BiteWiseWeb2.Data;

public partial class WorkoutLog
{
    [Key]
    public int LogId { get; set; }
    
    public int UserId { get; set; }
   
    public int? ExerciseId { get; set; }

    public decimal? Duration { get; set; }

    public decimal? CaloriesBurned { get; set; }

    public DateOnly? Date { get; set; }

    public virtual Exercise? Exercise { get; set; }

    public virtual User User { get; set; } = null!;
}
