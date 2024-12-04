using System;
using System.Collections.Generic;

namespace Database.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<UsersNbp> UsersNbps { get; set; } = new List<UsersNbp>();
}
