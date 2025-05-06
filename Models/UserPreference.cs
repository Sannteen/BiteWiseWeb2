using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace BiteWiseWeb2.Models;

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
