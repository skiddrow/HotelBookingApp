using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HotelBookingApp.WEB.Models;
using Microsoft.AspNet.Identity;
using HotelBookingApp.BLL.Interfaces;
using HotelBookingApp.BLL.Infrastructure;
using HotelBookingApp.BLL.DTO;
using AutoMapper;

namespace HotelBookingApp.WEB.Controllers
{
    [Authorize]
    public class RequestController : Controller
    {
        IRequestService requestService;
        IBookingService bookingService;            

        public RequestController(IRequestService reqService,IBookingService bookService)
        {
            requestService = reqService;
            bookingService = bookService;
        }

        public ActionResult Index()
        {
            var requestsDto = requestService.GetRequests(User.Identity.GetUserId());
            var requests = Mapper.Map<IEnumerable<RequestDTO>,IEnumerable<RequestViewModel>>(requestsDto);
            var alerts = bookingService.GetUserUnpaidBills(User.Identity.GetUserId()).Count;
            ViewBag.Alerts = alerts;

            return View(requests);
        }

        [HttpGet]
        public ActionResult MakeRequest()
        {
            ViewBag.Date = DateTime.Today.Date.ToString("yyyy-MM-dd");

            return View();
        }

        [HttpPost]
        public ActionResult MakeRequest(RequestViewModel model, string apartmentClass)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.UserId = User.Identity.GetUserId();

                    if (apartmentClass == null)
                    {
                        ModelState.AddModelError("Empty class", "Empty apartment class field");
                    }
                    else if (apartmentClass == "Luxury")
                    {
                        model.ApartmentClassId = 1;
                    }
                    else if (apartmentClass == "Comfort")
                    {
                        model.ApartmentClassId = 2;
                    }
                    else if (apartmentClass == "Economy")
                    {
                        model.ApartmentClassId = 3;
                    }

                    var reqDto = Mapper.Map<RequestViewModel, RequestDTO>(model);
                    requestService.MakeRequest(reqDto);

                    return RedirectToAction("Index");
                }
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }

            return View(model);
        }

        public ActionResult CancelRequest(int? id)
        {
            return View();
        }
    }
}