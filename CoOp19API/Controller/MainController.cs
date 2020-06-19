using CoOp19API;
using CoOp19API.Domain;
using CoOp19API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoOp19.App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MainController : ControllerBase
    {
        private readonly ILogger log;

        public MainController(ILogger<MainController> logger)
        {
            log = logger;
        }

        /// <summary>
        /// retrieves a list of all map items
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MapData>>> GetAction([FromServices] IGet get)
        {
            log.LogInformation("Querrying the database for all maped items");
            return await TryTask<IEnumerable<MapData>>.Run(async () => Ok(await get.MapData()));
        }

        /// <summary>
        /// gets a single map data of primary key "id"
        /// </summary>
        /// <param name="id">id of item to search</param>
        /// <returns>single map data</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<MapData>> GetAction([FromServices] IGet get, int id)
        {
            log.LogInformation($"Querrying the database for item with id:{id}");
            return await TryTask<MapData>.Run(async () => Ok(await get.MapData(id)));
        }

        /// <summary>
        /// retrieves a list of map items within a specified radius
        /// </summary>
        /// <param name="North"></param>
        /// <param name="West"></param>
        /// <param name="Radius"></param>
        /// <returns></returns>
        [HttpGet("{North}/{West}/{Radius}")]
        public async Task<ActionResult<IEnumerable<MapData>>> GetAction([FromServices] IGet get, decimal North, decimal West, decimal Radius)
        {
            log.LogInformation($"Querrying the database for all items within {Radius} miles of N{North}, W{West}");
            return await TryTask<IEnumerable<MapData>>.Run(async () => Ok(await get.MapData(North, West, Radius)));
        }

        /// <summary>
        /// retrieves a list of map items within a given city
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        [HttpGet("City/{city}")]
        public async Task<ActionResult<IEnumerable<MapData>>> GetActionCity([FromServices] IGet get, string city)
        {
            log.LogInformation($"Querrys the database for all items within {city}");
            return await TryTask<IEnumerable<MapData>>.Run(async () => Ok(await get.MapData(item => item.City == city)));
        }

        /// <summary>
        /// retrieves a list of all map items within the given state
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        [HttpGet("State/{state}")]
        public async Task<ActionResult<MapData>> GetActionState([FromServices] IGet get, string state)
        {
            log.LogInformation($"Querrys the database for all items within {state}");
            return await TryTask<MapData>.Run(async () => Ok(await get.MapData(item => item.State == state)));
        }

    }
}
