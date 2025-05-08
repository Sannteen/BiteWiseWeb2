using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiteWiseWeb2.Models;

public class DailySummary
{
    [Key]
    public int SummaryId { get; set; }
    [Required]
    public int? UserId { get; set; }

    public DateOnly Date { get; set; }

    public decimal? TotalCaloriesConsumed { get; set; }

    public decimal? TotalCaloriesBurned { get; set; }

    public decimal? NetCalories { get; set; }

    public virtual User? User { get; set; }
}
