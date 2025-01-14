using Database.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Models;
using User.UserCommands.Interfaces;

namespace User.UserCommands
{
    public class UserCommands : IUserCommands
    {
        private readonly IKalkulatorContext db;

        public UserCommands(IKalkulatorContext db)
        {
            this.db = db;
        }

        public async Task<IActionResult> Register(UserCredentials userCredentials)
        {
            if (!ValidateUserCredentials(userCredentials))
            {
                return new BadRequestObjectResult("Invalid user credentials.");
            }

            try
            {
                if (db.Set<Database.Models.User>().Any(u => u.Username == userCredentials.Username))
                {
                    return new BadRequestObjectResult("Username already exists.");
                }

                var entityToSave = new Database.Models.User()
                {
                    Username = userCredentials.Username,
                    Password = userCredentials.Password
                };

                db.Set<Database.Models.User>().Add(entityToSave);

                await db.SaveChangesAsync();

                return new OkObjectResult(new { message = "User registered successfully." });
            }
            catch (Exception)
            {
                return new BadRequestObjectResult("Failed to register user.");
            }
        }

        private bool ValidateUserCredentials(UserCredentials userCredentials)
        {
            if (userCredentials is null)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(userCredentials.Username))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(userCredentials.Password))
            {
                return false;
            }

            return true;
        }
    }
}
