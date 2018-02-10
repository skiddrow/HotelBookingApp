using HotelBookingApp.BLL.DTO;
using HotelBookingApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingApp.BLL.Interfaces
{
    /// <summary>
    /// Interface that defines functionality of Booking service
    /// </summary>
    public interface IBookingService
    {
        void MakeBooking(BookingDTO booking);
        void MakeBookingByRequest(RequestDTO request);
        void EditBooking(BookingDTO booking);
        void DeleteBooking(int? id);
        IEnumerable<BookingDTO> FindBooking(Func<Booking, bool> predicate);
        BookingDTO GetBooking(int? id);
        IEnumerable<BookingDTO> GetBookings();
        IEnumerable<BillDTO> GetOverDueBills();
        void ConfirmBill(int? id);
        List<BillDTO> GetUserUnpaidBills(string userId);
        List<BillDTO> GetUserBills(string userId);
        List<BookingDTO> GetUserBookings(string userId);
        void CancelAllOverDueBills();
        void CancelBooking(int? id);
        void Dispose();
    }
}
