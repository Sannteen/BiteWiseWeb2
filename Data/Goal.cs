using System;
using System.Collections.Generic;

namespace BiteWiseWeb2.Data;

public partial class Goal
{
    public int GoalId { get; set; }

    public int? UserId { get; set; }

    public decimal? TargetWeight { get; set; }

    public decimal? DailyCaloricTarget { get; set; }

    public decimal? WeeklyWeightChangeGoal { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User? User { get; set; }
}
