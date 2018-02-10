using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelBookingApp.BLL.DTO;
using HotelBookingApp.BLL.Services;

namespace HotelBookingApp.BLL.BusinessModels
{
    /// <summary>
    /// Class that provide functions for date validation
    /// </summary>
    public class DateValidation
    {
        /// <summary>
        /// Method that returns collection of apartments, which free for request picked date
        /// </summary>
        /// <param name="apartments"></param>
        /// <param name="bookings"></param>
        /// <param name="arrivalDate"></param>
        /// <param name="daysOfStay"></param>
        /// <returns></returns>
        public static IEnumerable<ApartmentDTO> GetValidApartments(List<ApartmentDTO> apartments, IEnumerable<BookingDTO> bookings, DateTime arrivalDate, int daysOfStay)
        {
            List<ApartmentDTO> validApartments = new List<ApartmentDTO>();
            validApartments.AddRange(apartments);

            foreach (var a in apartments)
            {
                var bookingsForApartment = bookings.Where(b => b.ApartmentId == a.Id).Where(b => b.IsCanceled == false).ToList();

                foreach (var b in bookingsForApartment)
                {
                    if ((arrivalDate.AddDays(daysOfStay) >= b.DateOfArrival && arrivalDate.AddDays(daysOfStay) <= b.DateOfArrival.AddDays(b.DaysOfArrival)) || (arrivalDate >= b.DateOfArrival && arrivalDate <= b.DateOfArrival.AddDays(b.DaysOfArrival)))
                    {
                        validApartments.Remove(a);
                    }
                }
            }

            return validApartments;
        }

        public static bool IsValidApartment(int id, IEnumerable<BookingDTO> bookings, DateTime arrivalDate, int daysOfStay)
        {
            foreach (var b in bookings)
            {
                if (((arrivalDate.AddDays(daysOfStay) >= b.DateOfArrival && arrivalDate.AddDays(daysOfStay) <= b.DateOfArrival.AddDays(b.DaysOfArrival)) || (arrivalDate >= b.DateOfArrival && arrivalDate <= b.DateOfArrival.AddDays(b.DaysOfArrival))) && b.ApartmentId == id)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
