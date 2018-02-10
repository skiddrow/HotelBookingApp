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
    /// Interface that defines functionality of Request service
    /// </summary>
    public interface IRequestService
    {
        void MakeRequest(RequestDTO request);
        void EditRequest(RequestDTO request);
        void DeleteRequest(int? id);
        IEnumerable<RequestDTO> FindRequest(Func<Request, bool> predicate);
        RequestDTO GetRequest(int? id);
        IEnumerable<RequestDTO> GetRequests(string id);
        IEnumerable<RequestDTO> GetRequestsForManager();
        void ConfirmRequest(int? id, int? aprtId, string managerId);
        void RejectRequest(int? id, string Message);
        int GetNotProccesedRequestCount();
        void Dispose();
    }
}
