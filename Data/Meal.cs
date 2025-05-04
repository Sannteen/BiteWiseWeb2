using System;
using System.Collections.Generic;

namespace BiteWiseWeb2.Data;

public partial class Meal
{
    public int MealId { get; set; }

    public int? UserId { get; set; }

    public int? FoodId { get; set; }

    public decimal? Quantity { get; set; }

    public string? MealType { get; set; }

    public DateTime? ConsumedAt { get; set; }

    public virtual Food? Food { get; set; }

    public virtual User? User { get; set; }
}
