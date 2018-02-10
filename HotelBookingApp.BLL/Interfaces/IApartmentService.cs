using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelBookingApp.BLL.DTO;
using HotelBookingApp.DAL.Entities;

namespace HotelBookingApp.BLL.Interfaces
{
    /// <summary>
    /// Interface that defines functionality of Apartment service
    /// </summary>
    public interface IApartmentService
    {
        void AddApartament(ApartmentDTO apartment);
        void EditApartament(ApartmentDTO apartment);
        void DeleteApartament(int? id);
        IEnumerable<ApartmentDTO> FindApartment(Func<Apartment, bool> predicate);
        ApartmentDTO GetApartment(int? id);
        IEnumerable<ApartmentDTO> GetApartments();
        IEnumerable<ApartmentDTO> GetAllApartments();
        IEnumerable<ApartmentDTO> GetApartmentsForPick(int? id);
        IEnumerable<ApartmentDTO> GetApartmentsForPickForBooking(BookingDTO booking);
        void CloseApartment(int? id);
        void OpenApartment(int? id);
        bool IsCorrectApartmentForPickedDate(int? id, DateTime date, int days);
        bool IsApartmentHasBookingsInFuture(int? id);
        void Dispose();
    }
}
