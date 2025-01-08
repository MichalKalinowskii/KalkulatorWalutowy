using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Models;

namespace User.UserCommands.Interfaces
{
    public interface IUserCommands
    {
        Task<IActionResult> Register(UserCredentials userCredentials);
    }
}
