using System;
using System.Collections.Generic;

namespace BiteWiseWeb2.Models;

public partial class DailySummary
{
    public int SummaryId { get; set; }

    public int? UserId { get; set; }

    public DateOnly Date { get; set; }

    public decimal? TotalCaloriesConsumed { get; set; }

    public decimal? TotalCaloriesBurned { get; set; }

    public decimal? NetCalories { get; set; }

    public virtual User? User { get; set; }
}
