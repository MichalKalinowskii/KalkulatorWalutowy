using Database.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Models;
using User.UserQueries.Interfaces;

namespace User.UserQueries
{
    public class UserQueries : IUserQueries
    {
        private readonly IKalkulatorContext db;

        public UserQueries(IKalkulatorContext db)
        {
            this.db = db;
        }

        public async Task<IActionResult> Login(UserCredentials userCredentials)
        {
            try
            {
                var user = await db.Set<Database.Models.User>()
                    .FirstOrDefaultAsync(u => u.Username == userCredentials.Username && u.Password == userCredentials.Password);

                var userCredentialsToReturn = new UserCredentials()
                {
                    Username = user.Username,
                };

                return new OkObjectResult(userCredentialsToReturn);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult("Failed to login user.");
            }
        }
    }
}
