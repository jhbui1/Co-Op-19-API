using CoOp19API.Domain;
using CoOp19API.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoOp19API.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class HealthRecourceServiceController : ControllerBase
    {
        private readonly ILogger log;

        public HealthRecourceServiceController(ILogger<HealthRecourceServiceController> logger)
        {
            log = logger;
        }

        /// <summary>
        /// retrieves a list of all health resources
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HealthViewResourceService>>> GetAction([FromServices] IGet get)
        {
            log.LogInformation($"Querring the database for all Health services");
            return await TryTask<IEnumerable<HealthViewResourceService>>.Run(async () => Ok(await get.HealthServices()));
        }
        /// <summary>
        /// gets a single healthservice resource by "id"
        /// </summary>
        /// <param name="id"></param>
        /// <returns>a single healthservice resource</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<HealthViewResourceService>> GetAction([FromServices] IGet get, int id)
        {
            log.LogInformation($"Querrying the database for a health resource with id:{id}");
            return await TryTask<HealthViewResourceService>.Run(async () => await get.HealthService(id));
        }
        /// <summary>
        /// retrives all health resources within a specified radius
        /// </summary>
        /// <param name="North"></param>
        /// <param name="West"></param>
        /// <param name="Radius"></param>
        /// <returns></returns>
        [HttpGet("{North}/{West}/{Radius}")]
        public async Task<ActionResult<IEnumerable<HealthViewResourceService>>> GetAction([FromServices] IGet get, decimal North, decimal West, decimal Radius)
        {
            log.LogInformation($"Querrying the database for all health services within {Radius} miles of N{North}, W{West}");
            return await TryTask<IEnumerable<HealthViewResourceService>>.Run(async () => Ok(await get.HealthServices(North, West, Radius)));
        }
        /// <summary>
        /// retrieves all health resources within a given city
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        [HttpGet("City/{city}")]
        public async Task<ActionResult<IEnumerable<HealthViewResourceService>>> GetActionCity([FromServices] IGet get, string city)
        {
            log.LogInformation($"Querrys the database for all health resources in {city}");
            return await TryTask<IEnumerable<HealthViewResourceService>>.Run(async () => Ok(await get.HealthServices(item => item.City == city)));
        }
        /// <summary>
        /// retrieves all health resources within a given state
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        [HttpGet("State/{state}")]
        public async Task<ActionResult<IEnumerable<HealthViewResourceService>>> GetActionState([FromServices] IGet get, string state)
        {
            log.LogInformation($"Querrys the database for all health resources in {state}");
            return await TryTask<IEnumerable<HealthViewResourceService>>.Run(async () => Ok(await get.HealthServices(item => item.State == state)));
        }
        /// <summary>
        /// post a health service to the database
        /// </summary>
        /// <param name="serv"></param>
        /// <returns>input items with updated ids</returns>
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(HealthViewResourceService))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<HealthViewResourceService>> PostAction([FromServices] IPost post, [FromBody] HealthViewResourceService serv)
        {
            log.LogInformation($"adding {serv.ServiceName} to the database");
            return await TryTask<HealthViewResourceService>.Run(async () =>
            {
                await post.AddHealthResourceService(serv);
                return Ok(serv);
            });
        }
    }
}
