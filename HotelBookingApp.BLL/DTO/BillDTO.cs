using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingApp.BLL.DTO
{
    public class BillDTO
    {
        public int Id { get; set; }

        public decimal Price { get; set; }

        public DateTime BookingDate { get; set; }

        public DateTime DateOfPayment { get; set; }
    }
}
