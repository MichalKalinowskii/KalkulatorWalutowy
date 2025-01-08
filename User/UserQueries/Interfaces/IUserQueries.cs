using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Models;

namespace User.UserQueries.Interfaces
{
    public interface IUserQueries
    {
        Task<IActionResult> Login(UserCredentials userCredentials);
    }
}
