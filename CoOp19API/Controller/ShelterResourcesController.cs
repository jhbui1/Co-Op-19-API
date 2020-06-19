using CoOp19API;
using CoOp19API.Domain;
using CoOp19API.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoOp19.App.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ShelterResourcesController : ControllerBase
    {
        private readonly ILogger log;

        public ShelterResourcesController(ILogger<ShelterResourcesController> logger)
        {
            log = logger;
        }

        /// <summary>
        /// retrieves a list of all shelters
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShelterViewResource>>> GetActionAsync([FromServices] IGet get)
        {
            log.LogInformation("Querrys the database for all shelther resources");
            return await TryTask<IEnumerable<ShelterViewResource>>.Run(async () => Ok(await get.Shelters()));
        }

        /// <summary>
        /// retrieves the shelter with id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{ID}")]
        public async Task<ActionResult<ShelterViewResource>> GetOneActionAsync([FromServices] IGet get, int id)
        {
            log.LogInformation($"Querrys the database a shelter resource with id:{id}");
            return await TryTask<ShelterViewResource>.Run(async () => await get.Shelter(id));
        }

        /// <summary>
        /// retrieves all shelters within a specified radius
        /// </summary>
        /// <param name="North">GPS North</param>
        /// <param name="West">GPS West</param>
        /// <param name="Radius">Radius</param>
        /// <returns></returns>
        [HttpGet("{North}/{West}/{Radius}")]
        public async Task<ActionResult<IEnumerable<ShelterViewResource>>> GetAction([FromServices] IGet get, decimal North, decimal West, decimal Radius)
        {
            log.LogInformation($"Querrying the database for all shelter resource within {Radius} miles of N{North}, W{West}");
            return await TryTask<IEnumerable<ShelterViewResource>>.Run(async () => Ok(await get.Shelters(North, West, Radius)));
        }

        /// <summary>
        /// retrieves a list of all shelters within a specified state
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        [HttpGet("City/{city}")]
        public async Task<ActionResult<IEnumerable<ShelterViewResource>>> GetActionCity([FromServices] IGet get, string city)
        {
            log.LogInformation($"Querrys the database for all shelter resources within {city}");
            return await TryTask<IEnumerable<ShelterViewResource>>.Run(async () => Ok(await get.Shelters(item => item.City == city)));
        }

        /// <summary>
        /// retrieves a list of all shelters within a given state
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        [HttpGet("State/{state}")]
        public async Task<ActionResult<IEnumerable<ShelterViewResource>>> GetActionState([FromServices] IGet get, string state)
        {
            log.LogInformation($"Querrys the database for all shelter resources within {state}");
            return await TryTask<IEnumerable<ShelterViewResource>>.Run(async () => Ok(await get.Shelters(item => item.State == state)));
        }

        /// <summary>
        /// posts a shelter to the database
        /// </summary>
        /// <param name="shelt">input</param>
        /// <returns>inputed item with updated ids</returns>
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(ShelterViewResource))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ShelterViewResource>> PostAsync([FromServices] IPost post, [FromBody] ShelterViewResource shelt)
        {
            log.LogInformation($"Adding {shelt.Name} to database");
            return await TryTask<ShelterViewResource>.Run(async () =>
            {
                await post.AddShelterResource(shelt);
                return Ok(shelt);
            });
        }
    }
}