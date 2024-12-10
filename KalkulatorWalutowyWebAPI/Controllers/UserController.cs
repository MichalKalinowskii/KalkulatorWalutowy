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

        [HttpGet("register")]
        public async Task Register([FromBody] UserCredentials userCredentials)
        {
            //TODO
        }

        [HttpGet("login")]
        public async Task Login([FromBody] UserCredentials userCredentials)
        {
            //TODO
        }
    }
}
