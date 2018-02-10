using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingApp.BLL.DTO
{
    public class ApartmentDTO
    {
        public int Id { get; set; }

        public int ApartmentClassId { get; set; }

        public int Number { get; set; }

        public int NumberOfPlaces { get; set; }

        public int Floor { get; set; }

        public int NumberOfRooms { get; set; }

        public decimal PricePerDay { get; set; }

        public bool IsClosed { get; set; }
    }
}
