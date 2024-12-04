using System;
using System.Collections.Generic;

namespace Database.Models;

public partial class UsersNbp
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? Nbpid { get; set; }

    public virtual Nbp? Nbp { get; set; }

    public virtual User? User { get; set; }
}
