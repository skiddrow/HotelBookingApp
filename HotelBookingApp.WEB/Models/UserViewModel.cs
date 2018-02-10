using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBookingApp.WEB.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
    }
}