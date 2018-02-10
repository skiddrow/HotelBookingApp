using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingApp.BLL.DTO
{
    public class BookingDTO
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int ApartmentClassId { get; set; }

        public DateTime BookingDate { get; set; }

        public int ApartmentId { get; set; }

        public int NumberOfPlaces { get; set; }

        public DateTime DateOfArrival { get; set; }

        public int DaysOfArrival { get; set; }

        public int BillId { get; set; }

        public bool IsConfirmed { get; set; }

        public bool IsCanceled { get; set; }
    }
}
