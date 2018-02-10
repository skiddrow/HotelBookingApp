using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelBookingApp.BLL.Interfaces;
using HotelBookingApp.DAL.Interfaces;
using HotelBookingApp.BLL.DTO;
using HotelBookingApp.DAL.Entities;
using HotelBookingApp.BLL.Infrastructure;
using AutoMapper;
using HotelBookingApp.BLL.BusinessModels;

namespace HotelBookingApp.BLL.Services
{
    /// <summary>
    /// Class that implements funcionality of IApartmentService interface. It has functions for creating, getting, updating data about requests.
    /// </summary>
    public class RequestService : IRequestService
    {
        IUnitOfWork DataBase { get; set; }
        BookingService bookingService { get; set; }

        /// <summary>
        /// Constructor that getting IUnitOfWork of work parameter and initializing DataBase class property by that parametr.
        /// </summary>
        /// <param name="unitOfWork"></param>
        public RequestService(IUnitOfWork unitOfWork)
        {
            DataBase = unitOfWork;
            bookingService = new BookingService(unitOfWork);
        }

        public void MakeRequest(RequestDTO request)
        {
            if (request == null)
            {
                throw new ValidationException("Empty request instance argument", "");
            }

            Request newRequest = new Request
            {
                ApartmentClassId = request.ApartmentClassId,
                DateOfArrival = request.DateOfArrival,
                DaysOfStay = request.DaysOfStay,
                IsConfirmed = false,
                IsProcessed = false,
                IsCanceled = false,
                NumberOfPlaces = request.NumberOfPlaces,
                RequestDate = DateTime.UtcNow,
                UserId = request.UserId
            };

            try
            {
                DataBase.Requests.Create(newRequest);
                DataBase.Save();
            }
            catch (Exception ex)
            {
                // Залогировать "Exceptin: " + ex.Message + " in RequestService in MakeRequest method"
                throw ex;
            }
        }

        public void EditRequest(RequestDTO request)
        {
            if (request == null)
            {
                throw new ValidationException("Empty request instance argument", "");
            }

            try
            {
                Mapper.Initialize(cfg => cfg.CreateMap<RequestDTO, Request>());
                DataBase.Requests.Update(Mapper.Map<RequestDTO, Request>(request));
            }
            catch (Exception ex)
            {
                // Залогировать "Exceptin: " + ex.Message + " in RequestService in EditRequest method"
                throw ex;
            }
        }

        public void DeleteRequest(int? id)
        {
            if (id == null)
            {
                throw new ValidationException("Empty request id argument", "");
            }

            try
            {
                DataBase.Requests.Delete(id.Value);
            }
            catch (Exception ex)
            {
                // Залогировать "Exceptin: " + ex.Message + " in RequestService in DeleteRequest method"
                throw ex;
            }
        }

        public IEnumerable<RequestDTO> FindRequest(Func<Request, bool> predicate)
        {
            if (predicate == null)
            {
                throw new ValidationException("Empty predicate argument", "");
            }
            try
            {
                var request = DataBase.Requests.Find(predicate);

                if (request == null)
                {
                    throw new ValidationException("Request is not found", "");
                }

                Mapper.Initialize(cfg => cfg.CreateMap<Request, RequestDTO>());

                return Mapper.Map<IEnumerable<Request>, IEnumerable<RequestDTO>>(request);
            }
            catch (Exception ex)
            {
                // Залогировать "Exceptin: " + ex.Message + " in RequestService in FindRequest method"
                throw ex;
            }
        }

        public RequestDTO GetRequest(int? id)
        {
            if (id == null)
            {
                throw new ValidationException("Empty request id argument", "");
            }

            try
            {
                var request = DataBase.Requests.Get(id.Value);

                if (request == null)
                {
                    throw new ValidationException("Request is not found", "");
                }

                return Mapper.Map<Request, RequestDTO>(request);
            }
            catch (Exception ex)
            {
                // Залогировать "Exceptin: " + ex.Message + " in RequestService in GetRequest method"
                throw ex;
            }
        }

        public IEnumerable<RequestDTO> GetRequests(string id)
        {
            try
            {
                //Mapper.Initialize(cfg => cfg.CreateMap<Request, RequestDTO>());

                var requests = Mapper.Map<IEnumerable<Request>, List<RequestDTO>>(DataBase.Requests.GetAll());

                return requests.Where(r => r.UserId == id);
            }
            catch (Exception ex)
            {
                // Залогировать "Exceptin: " + ex.Message + " in RequestService in GetRequests method"
                throw ex;
            }
        }

        /// <summary>
        /// Method that allows to confirm user request for booking.
        /// </summary>
        /// <param name="id"></param>
        public void ConfirmRequest(int? id, int? aprtId, string managerId)
        {
            if (id == null)
            {
                throw new ValidationException("Empty request id argument", "");
            }

            try
            {
                var request = GetRequest(id);
                request.ApartmentId = aprtId.Value;
                request.ManagerId = managerId;
                bookingService.MakeBookingByRequest(request);
                request.IsProcessed = true;
                request.IsConfirmed = true;

                DataBase.Requests.Update(Mapper.Map<RequestDTO,Request>(request));
                DataBase.Save();
            }
            catch (Exception ex)
            {
                // Залогировать "Exceptin: " + ex.Message + " in RequestService in ConfirmRequest method"
                throw ex;
            }
        }

        /// <summary>
        /// Method that allows to cancel request by some reason and send message to user with reason of rejecting. For example hotel doesn't has free numbers for picked by user date.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Message"></param>
        public void RejectRequest(int? id, string Message)
        {
            if (id == null)
            {
                throw new ValidationException("Empty request id argument", "");
            }

            try
            {
                var request = GetRequest(id);

                request.IsProcessed = true;
                request.IsCanceled = true;
                request.IsConfirmed = false;
                request.Message = Message;

                DataBase.Requests.Update(Mapper.Map<RequestDTO, Request>(request));
                DataBase.Save();
            }
            catch (Exception ex)
            {
                // Залогировать "Exceptin: " + ex.Message + " in RequestService in RejectRequest method"
                throw ex;
            }
        }

        /// <summary>
        /// Method that finds and returns not processed requests for manager.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RequestDTO> GetRequestsForManager()
        {
            try
            {
                var requests = Mapper.Map<IEnumerable<Request>, List<RequestDTO>>(DataBase.Requests.Find(r => r.IsProcessed == false));

                return requests;
            }
            catch (Exception ex)
            {
                // Залогировать "Exceptin: " + ex.Message + " in RequestService in GetRequestsForManager method"
                throw ex;
            }
        }

        /// <summary>
        /// Method that counting number of requests that need to be processed by manager.
        /// </summary>
        /// <returns></returns>
        public int GetNotProccesedRequestCount()
        {
            try
            {
                var count = DataBase.Requests.Find(r => r.IsProcessed == false).ToList().Count;

                return count;
            }
            catch (Exception ex)
            {
                // Залогировать "Exceptin: " + ex.Message + " in RequestService in GetNotProccesedRequestCount method"
                throw ex;
            }
        }


        public void Dispose()
        {
            DataBase.Dispose();
        }
    }
}
