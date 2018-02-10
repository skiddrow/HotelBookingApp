using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingApp.DAL.Entities
{
    /// <summary>
    /// Class that describes the entity of the request.
    /// </summary>
    public class Request
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public string ManagerId { get; set; }

        [Required]
        public DateTime RequestDate { get; set; }

        [Required]
        public int NumberOfPlaces { get; set; }

        [Required]
        public DateTime DateOfArrival { get; set; }

        [Required]
        public int DaysOfStay { get; set; }
        
        public int? ApartmentClassId { get; set; }

        public ApartmentClass ApartmentClass { get; set; }

        public int? ApartmentId { get; set; }

        public Apartment Apartment { get; set; }

        public bool IsProcessed { get; set; }

        public bool IsCanceled { get; set; }

        public bool IsConfirmed { get; set; }

        public string Message { get; set; }
    }
}
