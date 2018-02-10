using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingApp.DAL.Entities
{
    /// <summary>
    /// Class that describes the entity of the apartment.
    /// </summary>
    public class Apartment
    {
        public int Id { get; set; }

        [Required]
        public int ApartmentClassId { get; set; }

        [Required]
        public int Number { get; set; }

        [Required]
        public int NumberOfPlaces { get; set; }

        [Required]
        public int Floor { get; set; }

        [Required]
        public int NumberOfRooms { get; set; }

        [Required]
        public decimal PricePerDay { get; set; }

        /// <summary>
        /// This property is showing that apartment closed by some reason
        /// </summary>
        public bool IsClosed { get; set; }

        public ICollection<Request> Requests { get; set; }

        public ICollection<Booking> Bookings { get; set; }
    }
}
