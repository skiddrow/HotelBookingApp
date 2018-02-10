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
    /// Interface that defines functionality of Bill service
    /// </summary>
    public interface IBillService
    {
        void MakeBill(BillDTO bill);
        void EditApartament(BillDTO bill);
        IEnumerable<BillDTO> FindBill(Func<Bill, bool> predicate);
        BillDTO GetBill(int? id);
        IEnumerable<BillDTO> GetBills();
        void ConfirmBill(int? id);
        IEnumerable<BillDTO> GetOverDueBills();
        void Dispose();
    }
}
