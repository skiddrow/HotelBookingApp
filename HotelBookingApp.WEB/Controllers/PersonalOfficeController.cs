using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using HotelBookingApp.BLL.Interfaces;
using HotelBookingApp.WEB.Models;

namespace HotelBookingApp.WEB.Controllers
{
    [Authorize]
    public class PersonalOfficeController : Controller
    {
        IBookingService bookingService;

        public PersonalOfficeController(IBookingService service)
        {
            bookingService = service;
        }

        public ActionResult Index()
        {
            var alerts = bookingService.GetUserUnpaidBills(User.Identity.GetUserId()).Count;
            ViewBag.Alerts = alerts;

            return View();
        }

        public ActionResult Alerts()
        {
            var bills = bookingService.GetUserUnpaidBills(User.Identity.GetUserId());
            var bookings = bookingService.GetUserBookings(User.Identity.GetUserId());
            List<BookingViewModel> userBookings = new List<BookingViewModel>();

            foreach (var b in bills)
            {
                var currentBooking = bookings.Find(booking => booking.BillId == b.Id);

                if (!currentBooking.IsCanceled)
                {
                    userBookings.Add(new BookingViewModel
                    {
                        Id = currentBooking.Id,
                        BillId = b.Id,
                        BookingDate = currentBooking.BookingDate,
                        DateOfArrival = currentBooking.DateOfArrival,
                        DaysOfArrival = currentBooking.DaysOfArrival,
                        Price = b.Price
                    });
                }
            }

            return View(userBookings);
        }
    }
}