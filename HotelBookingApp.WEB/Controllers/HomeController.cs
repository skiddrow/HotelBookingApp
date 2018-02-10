using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HotelBookingApp.BLL.Interfaces;
using HotelBookingApp.BLL.DTO;
using HotelBookingApp.BLL.Infrastructure;
using HotelBookingApp.WEB.Models;
using AutoMapper;

namespace HotelBookingApp.WEB.Controllers
{
    public class HomeController : Controller
    {
        IApartmentService apartmentService;

        public HomeController(IApartmentService service)
        {
            apartmentService = service;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Test()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Test(ApartmentViewModel app)
        {
            try
            {
                //Mapper.Initialize(cfg => cfg.CreateMap<ApartmentViewModel, ApartmentDTO>());
                var appDto = Mapper.Map<ApartmentViewModel, ApartmentDTO>(app);
                apartmentService.AddApartament(appDto);
                return Content("<h2>Ваш заказ успешно оформлен</h2>");
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }

            return View(app);
        }

        protected override void Dispose(bool disposing)
        {
            apartmentService.Dispose();
            base.Dispose(disposing);
        }
    }
}