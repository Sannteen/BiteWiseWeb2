using System;
using System.Collections.Generic;

namespace BiteWiseWeb2.Data;

public partial class UserMeasurement
{
    public int MeasurementId { get; set; }

    public int? UserId { get; set; }

    public DateOnly? Date { get; set; }

    public decimal? Weight { get; set; }

    public decimal? WaistSize { get; set; }

    public decimal? HipSize { get; set; }

    public decimal? ChestSize { get; set; }

    public decimal? ArmSize { get; set; }

    public decimal? LegSize { get; set; }

    public virtual User? User { get; set; }
}
