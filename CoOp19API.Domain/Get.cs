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
    public class Get: IGet
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
        /// returns a full list of all health resources
        /// </summary>
        /// <returns>list</returns>
        public async Task<IEnumerable<HealthViewResource>> HealthResources()
        {
            var end = new List<HealthViewResource>();
            foreach (var item in await output.Get<HealthResource>())
            {
                var gen = await output.Get<GenericResource>(item.ResourceId);
                var map = await output.Get<MapData>(gen.LocId ?? default);
                end.Add(new HealthViewResource(item, gen, map));
            }
            return end;
        }

        /// <summary>
        /// returns a full list of all generics
        /// </summary>
        /// <returns>list</returns>
        public async Task<IEnumerable<GenericViewResource>> Generics()
        {
            var end = new List<GenericViewResource>();
            foreach (var item in await output.Get<GenericResource>())
            {
                var map = await output.Get<MapData>(item.LocId ?? default);
                end.Add(new GenericViewResource(map, item));
            }
            return end;
        }

        /// <summary>
        /// returns a full list of all health services
        /// </summary>
        /// <returns>list</returns>
        public async Task<IEnumerable<HealthViewResourceService>> HealthServices()
        {
            var end = new List<HealthViewResourceService>();
            foreach (var item in await output.Get<HealthResourceServices>())
            {
                var health = await output.Get<HealthResource>(item.HealthRes);
                var gen = await output.Get<GenericResource>(health.ResourceId);
                var map = await output.Get<MapData>(gen.LocId ?? default);
                end.Add(new HealthViewResourceService(item, health, gen, map));
            }
            return end;
        }

        /// <summary>
        /// returns a full list of all map data
        /// </summary>
        /// <returns>list</returns>
        public async Task<IEnumerable<ViewMapData>> MapData()
        {
            var end = new List<ViewMapData>();
            foreach (var item in await output.Get<MapData>())
            {
                end.Add(new ViewMapData(item));
            }
            return end;
        }

        /// <summary>
        /// returns a full list of all shelters
        /// </summary>
        /// <returns>list</returns>
        public async Task<IEnumerable<ShelterViewResource>> Shelters()
        {
            var end = new List<ShelterViewResource>();
            foreach (var item in await output.Get<ShelterResource>())
            {
                var gen = await output.Get<GenericResource>(item.ResourceId);
                var map = await output.Get<MapData>(gen.LocId ?? default);
                end.Add(new ShelterViewResource(map, item, gen));
            }
            return end;
        }

        /// <summary>
        /// returns a full list of all users
        /// </summary>
        /// <returns>list</returns>
        public async Task<IEnumerable<UsersView>> Users()
        {
            var end = new List<UsersView>();
            foreach (var item in await output.Get<Users>())
            {
                var map = await output.Get<MapData>(item.Loc ?? default);
                end.Add(new UsersView(map, item));
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

        /// <summary>
        /// retrieves a single health resource
        /// </summary>
        /// <param name="id">primary key</param>
        /// <returns>item of the entered id</returns>
        public async Task<HealthViewResource> HealthResource(int id)
        {
            var item = await output.Get<HealthResource>(id);
            var gen = await output.Get<GenericResource>(item.ResourceId);
            var map = await output.Get<MapData>(gen.LocId ?? default);
            return new HealthViewResource(item, gen, map)
            {
                Services = await output.Get<HealthResourceServices>(iTem => iTem.Id == id)
            };
        }

        /// <summary>
        /// retrieves a single generic resource
        /// </summary>
        /// <param name="id">primary key</param>
        /// <returns>item of the entered id</returns>
        public async Task<GenericViewResource> Generic(int id)
        {
            var item = await output.Get<GenericResource>(id);
            var map = await output.Get<MapData>(item.LocId ?? default);
            return new GenericViewResource(map, item)
            {
                ConsumableResources = await output.Get<ConsumableResource>(x => x.Id == id)
            };
        }

        /// <summary>
        /// retrieves a single health service resource
        /// </summary>
        /// <param name="id">primary key</param>
        /// <returns>item of the entered id</returns>
        public async Task<HealthViewResourceService> HealthService(int id)
        {
            var item = await output.Get<HealthResourceServices>(id);
            var health = await output.Get<HealthResource>(item.HealthRes);
            var gen = await output.Get<GenericResource>(health.ResourceId);
            var map = await output.Get<MapData>(gen.LocId ?? default);
            return new HealthViewResourceService(item, health, gen, map);
        }

        /// <summary>
        /// retrieves a single mapdata resource
        /// </summary>
        /// <param name="id">primary key</param>
        /// <returns>item of the entered id</returns>
        public async Task<ViewMapData> MapData(int id)
        {
            return new ViewMapData(await output.Get<MapData>(id));
        }

        /// <summary>
        /// retrieves a single shelter resource
        /// </summary>
        /// <param name="id">primary key</param>
        /// <returns>item of the entered id</returns>
        public async Task<ShelterViewResource> Shelter(int id)
        {
            var item = await output.Get<ShelterResource>(id);
            var gen = await output.Get<GenericResource>(item.ResourceId);
            var map = await output.Get<MapData>(gen.LocId ?? default);
            return new ShelterViewResource(map, item, gen);
        }

        /// <summary>
        /// retrieves a single user resource
        /// </summary>
        /// <param name="id">primary key</param>
        /// <returns>item of the entered id</returns>
        public async Task<UsersView> User(int id)
        {
            var item = await output.Get<Users>(id);
            var map = await output.Get<MapData>(item.Loc ?? default);
            return new UsersView(map, item);
        }

        private IEnumerable<T> Filter<T>(IEnumerable<T> list, Func<T, bool> check)
        {
            return from item in list
                   where check(item)
                   select item;
        }

        private bool InRadius(decimal xn, decimal yn, decimal xw, decimal yw, decimal r)
        {
            var Radius = Math.Pow((double)r / 69, 2);
            var North = Math.Pow((double)(xn - yn), 2);
            var West = Math.Pow((double)(xw - yw), 2);
            return North + West <= Radius;
        }

        /// <summary>
        /// retrieves all consumable resources within a geographical circle
        /// </summary>
        /// <param name="n">GPS North</param>
        /// <param name="w">GPS W</param>
        /// <param name="r">Radius</param>
        /// <returns>Resulting list</returns>
        public async Task<IEnumerable<ConsumableViewResource>> Consumables(decimal n, decimal w, decimal r) =>
            Filter(await Consumables(), item => InRadius(item.Gpsn ?? default, n, item.Gpsw ?? default, w, r));

        /// <summary>
        /// retrieves all health resources within a geographical circle
        /// </summary>
        /// <param name="n">GPS North</param>
        /// <param name="w">GPS W</param>
        /// <param name="r">Radius</param>
        /// <returns>Resulting list</returns>
        public async Task<IEnumerable<HealthViewResource>> HealthResources(decimal n, decimal w, decimal r) =>
            Filter(await HealthResources(), item => InRadius(item.Gpsn ?? default, n, item.Gpsw ?? default, w, r));

        /// <summary>
        /// retrieves all generic resources within a geographical circle
        /// </summary>
        /// <param name="n">GPS North</param>
        /// <param name="w">GPS W</param>
        /// <param name="r">Radius</param>
        /// <returns>Resulting list</returns>
        public async Task<IEnumerable<GenericViewResource>> Generics(decimal n, decimal w, decimal r) =>
            Filter(await Generics(), item => InRadius(item.Gpsn ?? default, n, item.Gpsw ?? default, w, r));

        /// <summary>
        /// retrieves all health service resources within a geographical circle
        /// </summary>
        /// <param name="n">GPS North</param>
        /// <param name="w">GPS W</param>
        /// <param name="r">Radius</param>
        /// <returns>Resulting list</returns>
        public async Task<IEnumerable<HealthViewResourceService>> HealthServices(decimal n, decimal w, decimal r) =>
            Filter(await HealthServices(), item => InRadius(item.Gpsn ?? default, n, item.Gpsw ?? default, w, r));

        /// <summary>
        /// retrieves all mapdata resources within a geographical circle
        /// </summary>
        /// <param name="n">GPS North</param>
        /// <param name="w">GPS W</param>
        /// <param name="r">Radius</param>
        /// <returns>Resulting list</returns>
        public async Task<IEnumerable<ViewMapData>> MapData(decimal n, decimal w, decimal r) =>
            Filter(await MapData(), item => InRadius(item.Gpsn ?? default, n, item.Gpsw ?? default, w, r));

        /// <summary>
        /// retrieves all shelter resources within a geographical circle
        /// </summary>
        /// <param name="n">GPS North</param>
        /// <param name="w">GPS W</param>
        /// <param name="r">Radius</param>
        /// <returns>Resulting list</returns>
        public async Task<IEnumerable<ShelterViewResource>> Shelters(decimal n, decimal w, decimal r) =>
            Filter(await Shelters(), item => InRadius(item.Gpsn ?? default, n, item.Gpsw ?? default, w, r));

        /// <summary>
        /// retrieves all user resources within a geographical circle
        /// </summary>
        /// <param name="n">GPS North</param>
        /// <param name="w">GPS W</param>
        /// <param name="r">Radius</param>
        /// <returns>Resulting list</returns>
        public async Task<IEnumerable<UsersView>> Users(decimal n, decimal w, decimal r) =>
            Filter(await Users(), item => InRadius(item.Gpsn ?? default, n, item.Gpsw ?? default, w, r));


        /// <summary>
        /// retrieves a list of consumable resources given a constraint
        /// </summary>
        /// <param name="filter">boolean function defineing constraint</param>
        /// <returns>list of resources</returns>
        public async Task<IEnumerable<ConsumableViewResource>> Consumables(Func<ConsumableViewResource, bool> filter) =>
            Filter(await Consumables(), filter);

        /// <summary>
        /// retrieves a list of Health resources given a constraint
        /// </summary>
        /// <param name="filter">boolean function defineing constraint</param>
        /// <returns>list of resources</returns>
        public async Task<IEnumerable<HealthViewResource>> HealthResources(Func<HealthViewResource, bool> filter) =>
            Filter(await HealthResources(), filter);

        /// <summary>
        /// retrieves a list of generic resources given a constraint
        /// </summary>
        /// <param name="filter">boolean function defineing constraint</param>
        /// <returns>list of resources</returns>
        public async Task<IEnumerable<GenericViewResource>> Generics(Func<GenericViewResource, bool> filter) =>
            Filter(await Generics(), filter);

        /// <summary>
        /// retrieves a list of health service resources given a constraint
        /// </summary>
        /// <param name="filter">boolean function defineing constraint</param>
        /// <returns>list of resources</returns>
        public async Task<IEnumerable<HealthViewResourceService>> HealthServices(Func<HealthViewResourceService, bool> filter) =>
            Filter(await HealthServices(), filter);

        /// <summary>
        /// retrieves a list of map resources given a constraint
        /// </summary>
        /// <param name="filter">boolean function defineing constraint</param>
        /// <returns>list of resources</returns>
        public async Task<IEnumerable<ViewMapData>> MapData(Func<ViewMapData, bool> filter) =>
            Filter(await MapData(), filter);

        /// <summary>
        /// retrieves a list of shelter resources given a constraint
        /// </summary>
        /// <param name="filter">boolean function defineing constraint</param>
        /// <returns>list of resources</returns>
        public async Task<IEnumerable<ShelterViewResource>> Shelters(Func<ShelterViewResource, bool> filter) =>
            Filter(await Shelters(), filter);

        /// <summary>
        /// retrieves a list of user resources given a constraint
        /// </summary>
        /// <param name="filter">boolean function defineing constraint</param>
        /// <returns>list of resources</returns>
        public async Task<IEnumerable<UsersView>> Users(Func<UsersView, bool> filter) =>
            Filter(await Users(), filter);
    }
}
