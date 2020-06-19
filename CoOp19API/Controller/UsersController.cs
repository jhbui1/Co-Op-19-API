using CoOp19API;
using CoOp19API.Domain;
using CoOp19API.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoOp19.App.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger log;

        public UsersController(ILogger<UsersController> logger)
        {
            log = logger;
        }

        /// <summary>
        /// retrieves a full list of users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsersView>>> GetUsers([FromServices] IGet get)
        {
            log.LogInformation("Querrying the database for all Users");
            return await TryTask<IEnumerable<UsersView>>.Run(async () => Ok(await get.Users()));
        }

        /// <summary>
        /// retrieves a single user
        /// </summary>
        /// <param name="id">id of user</param>
        /// <returns></returns>
        [HttpGet("{ID}")]
        public async Task<ActionResult<UsersView>> GetUserAsync([FromServices] IGet get, int id)
        {
            log.LogInformation($"Querrying the database for a user with id:{id}");
            return await TryTask<UsersView>.Run(async () => await get.User(id));
        }

        /// <summary>
        /// retrieves all users within a radius
        /// </summary>
        /// <param name="North"></param>
        /// <param name="West"></param>
        /// <param name="Radius"></param>
        /// <returns>querried list of users</returns>
        [HttpGet("{North}/{West}/{Radius}")]
        public async Task<ActionResult<IEnumerable<UsersView>>> GetAction([FromServices] IGet get, decimal North, decimal West, decimal Radius)
        {
            log.LogInformation($"Querrying the database for all users within {Radius} miles of N{North}, W{West}");
            return await TryTask<IEnumerable<UsersView>>.Run(async () => Ok(await get.Users(North, West, Radius)));
        }

        /// <summary>
        /// returns all users within the specified city
        /// </summary>
        /// <param name="city">name of city</param>
        /// <returns>querryed list</returns>
        [HttpGet("City/{city}")]
        public async Task<ActionResult<IEnumerable<UsersView>>> GetActionCity([FromServices] IGet get, string city)
        {
            log.LogInformation($"Querrying the database for all users within {city}");
            return await TryTask<IEnumerable<UsersView>>.Run(async () => Ok(await get.Users(item => item.City == city)));
        }

        /// <summary>
        /// returns all users within a specified state
        /// </summary>
        /// <param name="state">querying state</param>
        /// <returns>list of users</returns>
        [HttpGet("State/{state}")]
        public async Task<ActionResult<IEnumerable<UsersView>>> GetActionState([FromServices] IGet get, string state)
        {
            log.LogInformation($"Querrying the database for all users within {state}");
            return await TryTask<IEnumerable<UsersView>>.Run(async () => Ok(await get.Users(item => item.State == state)));
        }

        /// <summary>
        /// returns the user with a specified username
        /// </summary>
        /// <param name="city">name of city</param>
        /// <returns>querryed list</returns>
        [HttpGet("username")]
        public async Task<ActionResult<IEnumerable<UsersView>>> GetActionUserName([FromServices] IGet get, string U)
        {
            var re = Request;
            var headers = re.Headers;
            string token = string.Empty;
            string pwd = string.Empty;
            if (headers.ContainsKey("username"))
            {
                token = headers["username"].ToString();
            }
            if (headers.ContainsKey("password"))
            {
                pwd = headers["password"].ToString();
            }
            log.LogInformation($"Querrying the database for a user with the username {U}");
            var users = await TryTask<IEnumerable<UsersView>>.Run(async () => Ok(await get.Users(item => item.UserName.Trim() == token)));
            if(BCryptHash.VerifyUser(users.Value, pwd))
            {
                return users;
            }
            else
            {
                return new ObjectResult("ERROR: Invalid username/password") { StatusCode = 400 };
            }

        }

        /// <summary>
        /// enters a new user to the database
        /// </summary>
        /// <param name="User">input</param>
        /// <returns>item with updated values</returns>
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(UsersView))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<UsersView>> PostAsync([FromServices] IPost post, [FromBody] UsersView User)
        {
            log.LogInformation($"adding {User.Fname}, {User.Lname} to the database");
            return await TryTask<UsersView>.Run(async () =>
            {
                await post.AddUser(User);
                return User;
            });
        }
    }
}