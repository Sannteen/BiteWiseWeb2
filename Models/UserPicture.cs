using System;
using System.Collections.Generic;

namespace BiteWiseWeb2.Models;

public partial class UserPicture
{
    public int UserId { get; set; }

    public byte[]? Image { get; set; }

    public virtual User User { get; set; } = null!;
}
