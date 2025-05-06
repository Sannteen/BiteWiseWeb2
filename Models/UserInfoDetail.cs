using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BiteWiseWeb2.Models;

public partial class UserInfoDetail
{
  
    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Gender { get; set; }

    public int? Age { get; set; }

    public decimal? Height { get; set; }

    public decimal? Weight { get; set; }

    public string? UserGoal { get; set; }

    public byte[]? ProfilePicture { get; set; }
}
