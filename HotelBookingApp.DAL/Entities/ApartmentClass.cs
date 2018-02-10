using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingApp.DAL.Entities
{
    /// <summary>
    /// Class that describes the entity of the class of the apartment.
    /// </summary>
    public class ApartmentClass
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Apartment> Apartments { get; set; }

        public ICollection<Request> Requests { get; set; }

    }
}
