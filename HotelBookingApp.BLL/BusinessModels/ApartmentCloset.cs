using HotelBookingApp.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingApp.BLL.BusinessModels
{
    /// <summary>
    /// Class that provides functionality of closing/opening apartments.
    /// </summary>
    public class ApartmentCloset
    {
        public static bool IsCloseableApartment(ApartmentDTO apartment, List<BookingDTO> apartmentBookigns)
        {
            foreach (var b in apartmentBookigns)
            {
                if (b.DateOfArrival.AddDays(b.DaysOfArrival) >= DateTime.Today.Date)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
