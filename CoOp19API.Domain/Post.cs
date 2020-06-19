using CoOp19API.DataAccess;
using CoOp19API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoOp19API.Domain
{
    public class Post : IPost
    {
        private readonly IInput input;

        public Post(IInput inp)
        {
            input = inp;
        }

        /// <summary>
        /// adds a map data resource to the database
        /// </summary>
        /// <param name="input">input</param>
        /// <returns>updated item after adding</returns>
        public async Task AddMapData(ViewMapData item)
        {
            var newItem = await input.Add(item.ToData());
            item.ID = newItem.Id;
        }

        /// <summary>
        /// adds a user resource to the database
        /// </summary>
        /// <param name="input">input</param>
        /// <returns>updated item after adding</returns>
        public async Task AddUser(UsersView item)
        {
            item.Id = (await input.Add(item.ToData())).Id;
        }

        /// <summary>
        /// adds a shelter resource to the database
        /// </summary>
        /// <param name="input">input</param>
        /// <returns>updated item after adding</returns>
        public async Task AddShelterResource(ShelterViewResource item)
        {
            item.Id = (await input.Add(item.ToData())).Id;
        }

        /// <summary>
        /// adds a health service resource to the database
        /// </summary>
        /// <param name="input">input</param>
        /// <returns>updated item after adding</returns>
        public async Task AddHealthResourceService(HealthViewResourceService item)
        {
            item.Id = (await input.Add(item.ToData())).Id;
        }

        /// <summary>
        /// adds a health resource to the database
        /// </summary>
        /// <param name="input">input</param>
        /// <returns>updated item after adding</returns>
        public async Task AddHealthResource(HealthViewResource item)
        {
            item.Id = (await input.Add(item.ToData())).Id;
        }

        /// <summary>
        /// adds a generic resource to the database
        /// </summary>
        /// <param name="input">input</param>
        /// <returns>updated item after adding</returns>
        public async Task AddGeneric(GenericViewResource item)
        {
            item.Id = (await input.Add(item.ToData())).Id;
        }

        /// <summary>
        /// adds a consumable resource to the database
        /// </summary>
        /// <param name="input">input</param>
        /// <returns>updated item after adding</returns>
        public async Task AddConsumable(ConsumableViewResource item)
        {
            item.Id = (await input.Add(item.ToData())).Id;
        }
    }
}
