using AutoMapper;
using HotelBookingApp.BLL.DTO;
using HotelBookingApp.BLL.Infrastructure;
using HotelBookingApp.BLL.Interfaces;
using HotelBookingApp.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelBookingApp.WEB.Areas.Manager.Controllers
{
    [Authorize(Roles = "Manager,Admin")]
    public class HomeController : Controller
    {
        IRequestService requestService;
        IApartmentService apartmentService;

        public HomeController(IRequestService reqService, IApartmentService apService)
        {
            requestService = reqService;
            apartmentService = apService;
        }

        public ActionResult Index()
        {
            ViewBag.ReqCount = requestService.GetNotProccesedRequestCount();

            return View();
        }
    }
}