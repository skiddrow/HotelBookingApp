using HotelBookingApp.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using HotelBookingApp.WEB.Models;
using HotelBookingApp.BLL.Infrastructure;
using AutoMapper;
using HotelBookingApp.BLL.DTO;

namespace HotelBookingApp.WEB.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        IApartmentService apartmentService;
        IBookingService bookingService;

        public BookingController(IBookingService bookService, IApartmentService apService)
        {
            bookingService = bookService;
            apartmentService = apService;
        }

        public ActionResult Index()
        {
            try
            {
                bookingService.CancelAllOverDueBills();
                var alerts = bookingService.GetUserUnpaidBills(User.Identity.GetUserId()).Count;
                ViewBag.Alerts = alerts;
                var bills = bookingService.GetUserBills(User.Identity.GetUserId());
                var bookings = bookingService.GetUserBookings(User.Identity.GetUserId());
                var apartments = apartmentService.GetAllApartments().ToList();
                List<BookingViewModel> userBookings = new List<BookingViewModel>();

                foreach (var b in bookings)
                {
                    var currentBill = bills.Find(bill => bill.Id == b.BillId);
                    var currentApartment = apartments.Find(ap => ap.Id == b.ApartmentId);

                    userBookings.Add(new BookingViewModel
                    {
                        Id = b.Id,
                        BookingDate = b.BookingDate,
                        DateOfArrival = b.DateOfArrival,
                        DaysOfArrival = b.DaysOfArrival,
                        Price = currentBill.Price,
                        NumberOfPlaces = currentApartment.NumberOfPlaces,
                        ApartmentClassId = currentApartment.ApartmentClassId,
                        IsCanceled = b.IsCanceled,
                        IsConfirmed = b.IsConfirmed
                    });
                }

                return View(userBookings);
            }
            catch (ValidationException ex)
            {
                // Залогировать эксепшн
            }

            return View("Error");
        }

        [HttpGet]
        public ActionResult MakeBooking()
        {
            ViewBag.Date = DateTime.Today.Date.ToString("yyyy-MM-dd");
            bookingService.CancelAllOverDueBills();

            return View();
        }

        [HttpPost]
        public ActionResult MakeBooking(BookingViewModel model, string apartmentClass)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.UserId = User.Identity.GetUserId();

                    if (apartmentClass == null)
                    {
                        ModelState.AddModelError("Empty class", "Empty apartment class field");

                        return View(model);
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

                    var apartments = Mapper.Map<IEnumerable<ApartmentDTO>, IEnumerable<ApartmentViewModel>>(apartmentService.GetApartmentsForPickForBooking(Mapper.Map<BookingViewModel, BookingDTO>(model)));

                    if (apartments.Count(a => a.Id > 0) == 0)
                    {
                        ViewBag.Error = "No free apartments for picked date";

                        return View();
                    }

                    BookingContainer.Date = model.DateOfArrival;
                    BookingContainer.Days = model.DaysOfArrival;

                    return PartialView("ApartmentsPartial", apartments);
                }
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }

            return View(model);
        }

        public ActionResult PickApartment(int? id)
        {
            try
            {
                if (id == null || BookingContainer.Date == null)
                {
                    return View("InvalidData");
                }

                var apartment = Mapper.Map<ApartmentDTO, ApartmentViewModel>(apartmentService.GetApartment(id));

                if (apartmentService.IsCorrectApartmentForPickedDate(id, BookingContainer.Date, BookingContainer.Days))
                {
                    bookingService.MakeBooking(new BookingDTO
                    {
                        ApartmentClassId = apartment.ApartmentClassId,
                        ApartmentId = id.Value,
                        DateOfArrival = BookingContainer.Date,
                        DaysOfArrival = BookingContainer.Days,
                        UserId = User.Identity.GetUserId()
                    });

                    return RedirectToAction("Index");
                }
                else
                {
                    if (apartment.ApartmentClassId == 1)
                    {
                        ViewBag.Error = "No free luxury apartments for date " + BookingContainer.Date.ToShortDateString() + " ";
                    }
                    else if (apartment.ApartmentClassId == 1)
                    {
                        ViewBag.Error = "No free comfort apartments for date " + BookingContainer.Date.ToShortDateString() + " ";
                    }
                    else if (apartment.ApartmentClassId == 1)
                    {
                        ViewBag.Error = "No free economy apartments for date " + BookingContainer.Date.ToShortDateString() + " ";
                    }

                    return View("MakeBooking");
                }
            }
            catch (ValidationException ex)
            {
                // Залогировать эксепшн
            }

            return HttpNotFound();
        }

        public ActionResult CancelBooking(int? id)
        {
            try
            {
                if (id == null)
                {
                    return HttpNotFound();
                }

                bookingService.CancelBooking(id);
            }
            catch (ValidationException ex)
            {
                // Залогировать эксепшн
            }

            return RedirectToAction("Index");
        }
    }

    static class BookingContainer
    {
        public static DateTime Date { get; set; }
        public static int Days { get; set; }
    }
}