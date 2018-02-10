using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingApp.DAL.Entities
{
    /// <summary>
    /// Class that describes the entity of the bill.
    /// </summary>
    public class Bill
    {
        public int Id { get; set; }

        [Required]
        public decimal Price { get; set; }

        public DateTime? BookingDate { get; set; }

        public DateTime? DateOfPayment { get; set; }
    }
}
