using CoOp19API.DataAccess;
using CoOp19API.Domain.Models;
using CoOp19API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoOp19API.Domain
{
    public class Get
    {
        private readonly IOutput output;

        public Get(IOutput output)
        {
            this.output = output;
        }
        /// <summary>
        /// returns a full list of all consumables
        /// </summary>
        /// <returns>list</returns>
        public async Task<IEnumerable<ConsumableViewResource>> Consumables()
        {
            var end = new List<ConsumableViewResource>();
            foreach (var item in await output.Get<ConsumableResource>())
            {
                var gen = await output.Get<GenericResource>(item.ResourceId);
                var map = await output.Get<MapData>(gen.LocId ?? default);
                end.Add(new ConsumableViewResource(map, gen, item));
            }
            return end;
        }
        /// <summary>
        /// retrieves a single consumable resource
        /// </summary>
        /// <param name="id">primary key</param>
        /// <returns>item of the entered id</returns>
        public async Task<ConsumableViewResource> Consumable(int id)
        {
            var item = await output.Get<ConsumableResource>(id);
            var gen = await output.Get<GenericResource>(item.ResourceId);
            var map = await output.Get<MapData>(gen.LocId ?? default);
            return new ConsumableViewResource(map, gen, item);
        }

        private bool InRadius(decimal xn, decimal yn, decimal xw, decimal yw, decimal r)
        {
            var Radius = Math.Pow((double)r / 69, 2);
            var North = Math.Pow((double)(xn - yn), 2);
            var West = Math.Pow((double)(xw - yw), 2);
            return North + West <= Radius;
        }
        private IEnumerable<T> Filter<T>(IEnumerable<T> list, Func<T, bool> check)
        {
            return from item in list
                   where check(item)
                   select item;
        }

        public async Task<IEnumerable<ConsumableViewResource>> Consumables(decimal n, decimal w, decimal r) =>
            Filter(await Consumables(), item => InRadius(item.Gpsn ?? default, n, item.Gpsw ?? default, w, r));

        public async Task<IEnumerable<ConsumableViewResource>> Consumables(Func<ConsumableViewResource, bool> filter) =>
            Filter(await Consumables(), filter);


    }
}
