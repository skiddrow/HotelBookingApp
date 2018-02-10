using HotelBookingApp.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using HotelBookingApp.BLL.DTO;
using HotelBookingApp.BLL.Infrastructure;
using HotelBookingApp.WEB.Models;
using Microsoft.AspNet.Identity;

namespace HotelBookingApp.WEB.Areas.Manager.Controllers
{
    [Authorize(Roles = "Manager,Admin")]
    public class RequestController : Controller
    {
        IRequestService requestService;
        IApartmentService apartmentService;
        IBookingService bookingService;

        public RequestController(IRequestService reqService, IApartmentService apService,IBookingService bookService)
        {
            requestService = reqService;
            apartmentService = apService;
            bookingService = bookService;
        }

        public ActionResult Index()
        {
            var requests = Mapper.Map<IEnumerable<RequestDTO>, IEnumerable<RequestViewModel>>(requestService.GetRequestsForManager());

            return View(requests);
        }

        [HttpGet]
        public ActionResult CancelRequest(int? id)
        {
            var request = Mapper.Map<RequestDTO, RequestViewModel>(requestService.GetRequest(id.Value));

            return View(request);
        }

        [HttpPost]
        public ActionResult CancelRequest(int? id, string message)
        {
            try
            {
                requestService.RejectRequest(id, message);
            }
            catch (ValidationException ex)
            {
                ViewBag.Error = ex.Message;

                return View("CancelError");
            }

            return RedirectToAction("Index");

        }

        public ActionResult RoomsForPick(int? id)
        {
            try
            {
                bookingService.CancelAllOverDueBills();
                var apartmentsDto = apartmentService.GetApartmentsForPick(id.Value);
                ApartmentContainer.roomsForPickIds.Clear();
                ApartmentContainer.requestId = id.Value;

                foreach (var a in apartmentsDto)
                {
                    ApartmentContainer.roomsForPickIds.Add(a.Id);
                }

                if (apartmentsDto.Count(a => a.Id >= 1) == 0)
                {
                    ViewBag.Error = "There are not available rooms for this date!";

                    return View();
                }

                var apartments = Mapper.Map<IEnumerable<ApartmentDTO>, IEnumerable<ApartmentViewModel>>(apartmentsDto);
                ViewBag.ReqId = id.Value;

                return View(apartments);
            }
            catch (ValidationException ex)
            {
                // Залогировать эксепшн
            }

            ViewBag.Error = "Can't find request!";

            return View();
        }

        public ActionResult RoomPickingConfirmation(int? aprtId, int? reqId)
        {
            try
            {
                var apartmentDto = apartmentService.GetApartment(aprtId.Value);

                if (apartmentDto == null)
                {
                    ViewBag.Error = "Invalid apartment id!";

                    return View();
                }

                var apartment = Mapper.Map<ApartmentDTO, ApartmentViewModel>(apartmentDto);
                ViewBag.ReqId = reqId.Value;

                return View(apartment);
            }
            catch (ValidationException ex)
            {
                // Залогировать эксепшн
            }

            return RedirectToAction("RoomsForPick", new { @id = reqId });
        }

        public ActionResult ConfirmRequest(int? aprtId, int? reqId)
        {
            if (ApartmentContainer.roomsForPickIds.Contains(aprtId.Value) && ApartmentContainer.requestId == reqId.Value)
            {
                try
                {
                    var request = Mapper.Map<RequestDTO, RequestViewModel>(requestService.GetRequest(reqId));

                    if (apartmentService.IsCorrectApartmentForPickedDate(aprtId, request.DateOfArrival, request.DaysOfStay))
                    {
                        requestService.ConfirmRequest(reqId.Value, aprtId.Value, User.Identity.GetUserId());
                    }
                    else
                    {
                        return View("NoAvailableApartments");
                    }
                }
                catch (ValidationException ex)
                {
                    // Залогировать эксепшн
                }

                return RedirectToAction("Index");
            }
            else
            {
                return View("IncorrectData");
            }
        }
    }

    static class ApartmentContainer
    {
        public static List<int> roomsForPickIds;
        public static int requestId;

        static ApartmentContainer()
        {
            roomsForPickIds = new List<int>();
        }
    }
}