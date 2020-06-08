using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoOp19API.Domain;
using CoOp19API.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoOp19API.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class GenericResourceController : ControllerBase
    {
        private readonly ILogger log;

        public GenericResourceController(ILogger<GenericResourceController> logger)
        {
            log = logger;
        }

        /// <summary>
        /// retrieves a list of all generic
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenericViewResource>>> GetAction([FromServices] IGet get)
        {
            log.LogInformation($"Querrying the database for all generic data");
            return await TryTask<IEnumerable<GenericViewResource>>.Run(async () => Ok(await get.Generics()));
        }

        /// <summary>
        /// gets a single generic resource "id"
        /// </summary>
        /// <param name="id"></param>
        /// <returns>a single generic</returns>
        [HttpGet("{ID}")]
        public async Task<ActionResult<GenericViewResource>> GetAction([FromServices] IGet get, int id)
        {
            log.LogInformation($"Querrying the database for generic resource with id:{id}");
            return await TryTask<GenericViewResource>.Run(async () => Ok(await get.Generic(id)));
        }

        /// <summary>
        /// retrieves all generic resources within a specified radius
        /// </summary>
        /// <param name="North"></param>
        /// <param name="West"></param>
        /// <param name="Radius"></param>
        /// <returns> cordinates of that generic location</returns>
        [HttpGet("{North}/{West}/{Radius}")]
        public async Task<ActionResult<IEnumerable<GenericViewResource>>> GetAction([FromServices] IGet get, decimal North, decimal West, decimal Radius)
        {
            log.LogInformation($"Querrys the database for all generic data within {Radius} miles of N{North}, W{West}");
            return await TryTask<IEnumerable<GenericViewResource>>.Run(async () => Ok(await get.Generics(North, West, Radius)));
        }

        /// <summary>
        /// retrieves a list of generic resource within a given city
        /// </summary>
        /// <param name="city"></param>
        /// <returns>a single city with generic resources</returns>
        [HttpGet("City/{city}")]
        public async Task<ActionResult<IEnumerable<GenericViewResource>>> GetActionCity([FromServices] IGet get, string city)
        {
            log.LogInformation($"Querrying the database for all generic recources in {city}");
            return await TryTask<IEnumerable<GenericViewResource>>.Run(async () => Ok(await get.Generics(item => item.City == city)));
        }
        /// <summary>
        /// retrieves a list of generic resources within a given state
        /// </summary>
        /// <param name="state"></param>
        /// <returns>a single state with generic resource</returns>
        [HttpGet("State/{state}")]
        public async Task<ActionResult<IEnumerable<GenericViewResource>>> GetActionState([FromServices] IGet get, string state)
        {
            log.LogInformation($"Querrying the database for all generic recources in {state}");
            return await TryTask<IEnumerable<GenericViewResource>>.Run(async () => Ok(await get.Generics(item => item.State == state)));
        }
        /// <summary>
        /// post a generic resource to the database
        /// </summary>
        /// <param name="gen"></param>
        /// <returns>input items with updated ids</returns>
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(GenericViewResource))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<GenericViewResource>> PostAction([FromServices] IPost post, [FromBody] GenericViewResource gen)
        {
            log.LogInformation($"Adding {gen.Name} to the database");
            return await TryTask<GenericViewResource>.Run(async () =>
            {
                await post.AddGeneric(gen);
                return Ok(gen);
            });
        }
    }
}