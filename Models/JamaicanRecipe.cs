using System;
using System.Collections.Generic;

namespace BiteWiseWeb2.Models;

public partial class JamaicanRecipe
{
    public int RecipeId { get; set; }

    public string Recipename { get; set; } = null!;

    public string? Description { get; set; }

    public decimal? CaloriesPerServing { get; set; }

    public string? Ingredients { get; set; }

    public string? Instructions { get; set; }
}
