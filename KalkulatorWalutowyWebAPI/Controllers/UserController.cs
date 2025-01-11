using Microsoft.AspNetCore.Mvc;
using User.Models;
using User.UserCommands;
using User.UserCommands.Interfaces;
using User.UserQueries.Interfaces;

namespace KalkulatorWalutowyWebAPI.Controllers
{
    [Route("user")]
    public class UserController : ApiBaseController
    {
        private readonly IUserCommands userCommands;
        private readonly IUserQueries userQueries;

        public UserController(IUserCommands userCommands, IUserQueries userQueries)
        {
            this.userCommands = userCommands;
            this.userQueries = userQueries;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserCredentials userCredentials)
        {
            return await userCommands.Register(userCredentials);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserCredentials userCredentials)
        {
            return await userQueries.Login(userCredentials);
        }
    }
}
