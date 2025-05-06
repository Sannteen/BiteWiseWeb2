using System;
using System.Collections.Generic;

namespace BiteWiseWeb2.Models;

public partial class Food
{
    public int FoodId { get; set; }

    public string Name { get; set; } = null!;

    public int Calories { get; set; }

    public decimal? Protein { get; set; }

    public decimal? Carbs { get; set; }

    public decimal? Fats { get; set; }

    public decimal? Fiber { get; set; }

    public string? ServingSize { get; set; }

    public string? Category { get; set; }

    public short? IsLocal { get; set; }

    public int? UserId { get; set; }

    public virtual ICollection<Meal> Meals { get; set; } = new List<Meal>();

    public virtual User? User { get; set; }
}
