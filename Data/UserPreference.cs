using System;
using System.Collections.Generic;

namespace BiteWiseWeb2.Data;

public partial class UserPreference
{
    public int PreferenceId { get; set; }

    public int? UserId { get; set; }

    public string? DietType { get; set; }

    public string? PreferredFoods { get; set; }

    public string? Allergies { get; set; }

    public string? DislikedFoods { get; set; }

    public virtual User? User { get; set; }
}
