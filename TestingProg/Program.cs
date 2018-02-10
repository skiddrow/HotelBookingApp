using HotelBookingApp.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingProg
{
    class Program
    {
        static void Main(string[] args)
        {
            List<ApartmentDTO> aps = new List<ApartmentDTO>();
            aps.Add(new ApartmentDTO { Id = 1 });
            aps.Add(new ApartmentDTO { Id = 2 });
            //aps.Add(new ApartmentDTO { Id = 3 });
            List<BookingDTO> books = new List<BookingDTO>();
            books.Add(new BookingDTO { ApartmentId = 1, DateOfArrival = DateTime.Today.Date, DaysOfArrival = 0 });
            books.Add(new BookingDTO { ApartmentId = 2, DateOfArrival = new DateTime(2018, 02, 01), DaysOfArrival = 2 });

            var valAps = GetValidApartments(aps, books, new DateTime(2018, 1, 29), 3);

            foreach (var item in valAps)
            {
                Console.WriteLine(item.Id);
            }

        }

        public static IEnumerable<ApartmentDTO> GetValidApartments(List<ApartmentDTO> apartments, List<BookingDTO> bookings, DateTime arrivalDate, int daysOfStay)
        {
            List<ApartmentDTO> validApartments = new List<ApartmentDTO>();
            validApartments.AddRange(apartments);

            foreach (var a in apartments)
            {
                var bookingsForApartment = bookings.Where(b => b.ApartmentId == a.Id).ToList();

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
    }
}
