using System;
using System.Collections.Generic;

namespace Database.Models;

public partial class Nbprate
{
    public int Id { get; set; }

    public int? Nbpid { get; set; }

    public string? Currency { get; set; }

    public string? Code { get; set; }

    public double? Mid { get; set; }

    public virtual Nbp? Nbp { get; set; }
}
