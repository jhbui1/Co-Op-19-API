using CoOp19API.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoOp19API.Domain.Models
{
    public class UsersView
    {
        public UsersView() { }

        public UsersView(MapData map, Users user)
        {
            Id = user.Id;
            Loc = user.Loc;
            UserName = user.UserName.Trim();
            Password = user.Password.Trim();
            Salt = user.Salt;
            Fname = user.Fname;
            Lname = user.Lname;
            Phone = user.Phone;
            Email = user.Email;
            IsAdmin = user.IsAdmin;
            Gpsn = map.Gpsn;
            Gpsw = map.Gpsw;
            Address = map.Address;
            City = map.City;
            State = map.State;
        }

        public Users ToData()
        {
            var map = new MapData
            {
                Gpsn = this.Gpsn,
                Gpsw = this.Gpsw,
                Address = this.Address,
                City = this.City,
                State = this.State
            };

            string[] hashAndSalt = BCryptHash.HashPassword(this.Password);
            return new Users
            {
                UserName = this.UserName,
                Password = hashAndSalt[0],
                Salt = hashAndSalt[1],
                Fname = this.Fname,
                Lname = this.Lname,
                Phone = this.Phone,
                Email = this.Email,
                IsAdmin = this.IsAdmin,
                LocNavigation = map
            };
        }

        public int Id { get; set; }
        public int? Loc { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public decimal? Phone { get; set; }
        public string Email { get; set; }
        public decimal? Gpsn { get; set; }
        public decimal? Gpsw { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public bool IsAdmin { get; set; }
    }
}
