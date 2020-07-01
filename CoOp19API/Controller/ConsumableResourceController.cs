using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using CoOp19API.Domain;
using CoOp19API.Domain.Models;
using CoOp19API;
using Microsoft.AspNetCore.Authorization;

namespace CoOp19.App.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ConsumableResourceController : ControllerBase
    {
        private readonly ILogger log;

        public ConsumableResourceController(ILogger<ConsumableResourceController> logger)
        {
            this.log = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConsumableViewResource>>> GetAction([FromServices] IGet get)
        {
            return await TryTask<IEnumerable<ConsumableViewResource>>.Run(async () => Ok(await get.Consumables()));
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ConsumableViewResource>> GetAction([FromServices] IGet get, int id)
        {
            log.LogInformation($"Querying db for consumable res with id:{id}");
            return await TryTask<ConsumableViewResource>.Run(async () => Ok(await get.Consumable(id)));
        }
        /// <summary>
        /// retrieves all consumables within a specified radius
        /// </summary>
        /// <param name="North"></param>
        /// <param name="West"></param>
        /// <param name="Radius"></param>
        /// <returns> cordinates of that location</returns>
        [HttpGet("{North}/{West}/{Radius}")]
        public async Task<ActionResult<IEnumerable<ConsumableViewResource>>> GetAction([FromServices] IGet get, decimal North, decimal West, decimal Radius)
        {
            log.LogInformation($"querrys the data base for all items within {Radius} miles of N{North}, W{West}");
            return await TryTask<IEnumerable<ConsumableViewResource>>.Run(async () => Ok(await get.Consumables(North, West, Radius)));
        }
        /// <summary>
        /// retrieves all consumables within a given city
        /// </summary>
        /// <param name="city"></param>
        /// <returns>a single city with its consumables</returns>
        [HttpGet("City/{city}")]
        public async Task<ActionResult<IEnumerable<ConsumableViewResource>>> GetActionCity([FromServices] IGet get, string city)
        {
            log.LogInformation($"querrys the database for all consumable resources in {city}");
            return await TryTask<IEnumerable<ConsumableViewResource>>.Run(async () => Ok(await get.Consumables(item => item.City == city)));
        }
        /// <summary>
        /// retrieves all consumables within a given state
        /// </summary>
        /// <param name="state"></param>
        /// <returns>a single state with its consumables</returns>
        [HttpGet("State/{state}")]
        public async Task<ActionResult<IEnumerable<ConsumableViewResource>>> GetActionState([FromServices] IGet get, string state)
        {
            log.LogInformation($"querrys the database for all consumable resources in {state}");
            return await TryTask<IEnumerable<ConsumableViewResource>>.Run(async () => Ok(await get.Consumables(item => item.State == state)));
        }
        /// <summary>
        /// post a consumable to the database
        /// </summary>
        /// <param name="consume"></param>
        /// <returns>input items with updated ids</returns>
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(ConsumableViewResource))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ConsumableViewResource>> PostAction([FromServices] IPost post, [FromBody] ConsumableViewResource consume)
        {
            HeaderDecode.DecodeHeader(Request);
            log.LogInformation($"Adding {consume.Name} to consumable resources");
            return await TryTask<ConsumableViewResource>.Run(async () =>
            {
                await post.AddConsumable(consume);
                return consume;
            });
        }
    }
}
