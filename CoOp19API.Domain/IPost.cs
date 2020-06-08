using CoOp19API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoOp19API.Domain
{
    public interface IPost
    {
        Task AddConsumable(ConsumableViewResource item);
        Task AddGeneric(GenericViewResource item);
        Task AddHealthResource(HealthViewResource item);
        Task AddHealthResourceService(HealthViewResourceService item);
        Task AddMapData(ViewMapData item);
        Task AddShelterResource(ShelterViewResource item);
        Task AddUser(UsersView item);
    }
}
