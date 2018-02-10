using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingApp.DAL.Entities
{
    /// <summary>
    /// Class that describes the entity of booking.
    /// </summary>
    public class Booking
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public DateTime? BookingDate { get; set; }

        public int? ApartmentId { get; set; }

        public Apartment Apartment { get; set; }

        [Required]
        public DateTime DateOfArrival { get; set; }

        [Required]
        public int DaysOfArrival { get; set; }

        public int? BillId { get; set; }

        public Bill Bill { get; set; }

        public bool IsConfirmed { get; set; }

        public bool IsCanceled { get; set; }
    }
}
