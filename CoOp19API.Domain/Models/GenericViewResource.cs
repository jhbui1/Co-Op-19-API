using CoOp19API.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoOp19API.Domain.Models
{
    public class GenericViewResource
    {
        public GenericViewResource() { }
        public GenericViewResource(MapData map, GenericResource generic)
        {
            Id = generic.Id;
            LocId = map.Id;
            Gpsn = map.Gpsn;
            Gpsw = map.Gpsw;
            Address = map.Address;
            City = map.City;
            State = map.State;
            Name = generic.Title;
            Description = generic.Comment;

        }

        public GenericResource ToData()
        {
            var map = new MapData
            {
                Gpsn = this.Gpsn,
                Gpsw = this.Gpsw,
                City = this.City,
                Address = this.Address,
                State = this.State
            };
            return new GenericResource
            {
                Title = this.Name,
                Comment = this.Description,
                Loc = map
            };
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? LocId { get; set; }
        public decimal? Gpsn { get; set; }
        public decimal? Gpsw { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string State { get; set; }

        public List<ConsumableResource> ConsumableResources { get; set; }
    }
}
