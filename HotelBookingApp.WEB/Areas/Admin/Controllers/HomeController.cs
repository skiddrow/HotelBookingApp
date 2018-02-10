using AutoMapper;
using HotelBookingApp.BLL.DTO;
using HotelBookingApp.BLL.Interfaces;
using HotelBookingApp.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HotelBookingApp.BLL.Infrastructure;

namespace HotelBookingApp.WEB.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        IRequestService requestService;
        IApartmentService apartmentService;
        IBookingService bookingService;

        public HomeController(IRequestService reqService, IApartmentService apService, IBookingService bookService)
        {
            requestService = reqService;
            apartmentService = apService;
            bookingService = bookService;
        }

        public ActionResult Index()
        {
            var apartments = Mapper.Map<IEnumerable<ApartmentDTO>, IEnumerable<ApartmentViewModel>>(apartmentService.GetAllApartments());
            var bookings = bookingService.GetBookings();

            foreach (var a in apartments)
            {
                if (apartmentService.IsApartmentHasBookingsInFuture(a.Id))
                {
                    a.Closeable = false;
                }
                else
                {
                    a.Closeable = true;
                }
            }

            return View(apartments);
        }

        public ActionResult CloseApartmentDetails(int? id)
        {
            try
            {
                var apartment = Mapper.Map<ApartmentDTO, ApartmentViewModel>(apartmentService.GetApartment(id));

                if (apartment == null)
                {
                    return HttpNotFound();
                }

                if (apartmentService.IsApartmentHasBookingsInFuture(id))
                {
                    ViewBag.Error = "This apartment cannot be closed because of bookings in future.";
                }

                return PartialView(apartment);
            }
            catch (ValidationException ex)
            {
                //Log ex
            }

            return HttpNotFound();
        }

        public ActionResult CloseApartment(int? id)
        {
            if (id != null)
            {
                try
                {
                    apartmentService.CloseApartment(id);
                    
                    return RedirectToAction("Index");
                }
                catch (ValidationException ex)
                {
                    //Log ex
                }
            }

            return HttpNotFound();
        }

        public ActionResult OpenApartment(int? id)
        {
            if (id != null)
            {
                try
                {
                    apartmentService.OpenApartment(id);

                    return RedirectToAction("Index");
                }
                catch (ValidationException ex)
                {
                    //Log ex
                }
            }

            return HttpNotFound();
        }
    }
}