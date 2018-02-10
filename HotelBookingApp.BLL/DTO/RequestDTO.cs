using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingApp.BLL.DTO
{
    public class RequestDTO
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string ManagerId { get; set; }

        public DateTime RequestDate { get; set; }

        public int NumberOfPlaces { get; set; }

        public DateTime DateOfArrival { get; set; }

        public int DaysOfStay { get; set; }

        public int ApartmentClassId { get; set; }

        public int? ApartmentId { get; set; }

        public bool IsProcessed { get; set; }

        public bool IsCanceled { get; set; }

        public bool IsConfirmed { get; set; }

        public string Message { get; set; }
    }
}
