using System;
using System.Collections.Generic;

namespace Database.Models;

public partial class Nbp
{
    public int Id { get; set; }

    public string? TableType { get; set; }

    public string? No { get; set; }

    public DateOnly? TradingDate { get; set; }

    public DateOnly? EffectiveDate { get; set; }

    public virtual ICollection<Nbprate> Nbprates { get; set; } = new List<Nbprate>();

    public virtual ICollection<UsersNbp> UsersNbps { get; set; } = new List<UsersNbp>();
}
