using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using CoOp19API.Domain.Models;

namespace CoOp19API.Domain
{
    interface IGet
    {
        Task<ConsumableViewResource> Consumable(int id);
        Task<IEnumerable<ConsumableViewResource>> Consumables();
        Task<IEnumerable<ConsumableViewResource>> Consumables(decimal n, decimal w, decimal r);
        Task<IEnumerable<ConsumableViewResource>> Consumables(Func<ConsumableViewResource, bool> filter);
        Task<GenericViewResource> Generic(int id);
        Task<IEnumerable<GenericViewResource>> Generics();
        Task<IEnumerable<GenericViewResource>> Generics(decimal n, decimal w, decimal r);
        Task<IEnumerable<GenericViewResource>> Generics(Func<GenericViewResource, bool> filter);
        Task<HealthViewResource> HealthResource(int id);
        Task<IEnumerable<HealthViewResource>> HealthResources();
        Task<IEnumerable<HealthViewResource>> HealthResources(decimal n, decimal w, decimal r);
        Task<IEnumerable<HealthViewResource>> HealthResources(Func<HealthViewResource, bool> filter);
        Task<HealthViewResourceService> HealthService(int id);
        Task<IEnumerable<HealthViewResourceService>> HealthServices();
        Task<IEnumerable<HealthViewResourceService>> HealthServices(decimal n, decimal w, decimal r);
        Task<IEnumerable<HealthViewResourceService>> HealthServices(Func<HealthViewResourceService, bool> filter);
        Task<IEnumerable<ViewMapData>> MapData();
        Task<IEnumerable<ViewMapData>> MapData(decimal n, decimal w, decimal r);
        Task<IEnumerable<ViewMapData>> MapData(Func<ViewMapData, bool> filter);
        Task<ViewMapData> MapData(int id);
        Task<ShelterViewResource> Shelter(int id);
        Task<IEnumerable<ShelterViewResource>> Shelters();
        Task<IEnumerable<ShelterViewResource>> Shelters(decimal n, decimal w, decimal r);
        Task<IEnumerable<ShelterViewResource>> Shelters(Func<ShelterViewResource, bool> filter);
        Task<UsersView> User(int id);
        Task<IEnumerable<UsersView>> Users();
        Task<IEnumerable<UsersView>> Users(decimal n, decimal w, decimal r);
        Task<IEnumerable<UsersView>> Users(Func<UsersView, bool> filter);
    }
}
