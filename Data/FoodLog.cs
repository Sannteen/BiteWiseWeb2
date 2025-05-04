using System;
using System.Collections.Generic;

namespace BiteWiseWeb2.Data;

public partial class FoodLog
{
    public int RecordId { get; set; }

    public int UserId { get; set; }

    public string FoodName { get; set; } = null!;

    public string CalsConsumed { get; set; } = null!;

    public DateTime Date { get; set; }

    public virtual User User { get; set; } = null!;
}
