﻿using System;
using System.Collections.Generic;
using System.Text;

using CoOp19API.Models;

namespace CoOp19API.Domain.Models
{
    public class ConsumableViewResource
    {
        public ConsumableViewResource() { }

        public ConsumableViewResource(MapData map, GenericResource gen, ConsumableResource consumable) 
        {
            ResourceId = gen.Id;
            Id = consumable.Id;
            Price = consumable.Price;
            Quantity = consumable.Quantity;
            Gpsn = map.Gpsn;
            Gpsw = map.Gpsw;
            Address = map.Address;
            City = map.City;
            State = map.State;
            Name = gen.Title;
            Description = gen.Comment;
        }
        public ConsumableResource ToData()
        {
            var map = new MapData()
            {
                Gpsn = this.Gpsn,
                Gpsw = this.Gpsw,
                Address = this.Address,
                City = this.City,
                State = this.State
            };
            var gen = new GenericResource()
            {
                Title = this.Name,
                Comment = this.Description,
                Loc = map
            };
            return new ConsumableResource
            {
                Price = this.Price,
                Quantity = this.Quantity,
                Resource = gen
            };
        }
        public int ResourceId { get; set; }
        public int Id { get; set; }
        public decimal? Price { get; set; }
        public int Quantity { get; set; }
        public decimal? Gpsn { get; set; }
        public decimal? Gpsw { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
