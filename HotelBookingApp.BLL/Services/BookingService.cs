using AutoMapper;
using HotelBookingApp.BLL.BusinessModels;
using HotelBookingApp.BLL.DTO;
using HotelBookingApp.BLL.Infrastructure;
using HotelBookingApp.BLL.Interfaces;
using HotelBookingApp.DAL.Entities;
using HotelBookingApp.DAL.Interfaces;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingApp.BLL.Services
{
    /// <summary>
    /// Class that implements funcionality of IApartmentService interface. It has functions for creating, getting, updating data about bookings.
    /// </summary>
    public class BookingService : IBookingService
    {
        IUnitOfWork DataBase { get; set; }
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Constructor that getting IUnitOfWork of work parameter and initializing DataBase class property by that parametr.
        /// </summary>
        /// <param name="unitOfWork"></param>
        public BookingService(IUnitOfWork unitOfWork)
        {
            DataBase = unitOfWork;
        }

        public void MakeBooking(BookingDTO booking)
        {
            if (booking == null)
            {
                throw new ValidationException("Empty booking instance argument", "");
            }

            try
            {
                var apartment = Mapper.Map<Apartment, ApartmentDTO>(DataBase.Apartments.Get(booking.ApartmentId));

                Bill bill = new Bill
                {
                    BookingDate = DateTime.Now,
                    Price = Calculation.GetSumOfBooking(booking.DaysOfArrival, apartment.PricePerDay)
                };

                DataBase.Bills.Create(bill);
                DataBase.Save();

                Booking newBooking = new Booking
                {
                    ApartmentId = booking.ApartmentId,
                    BillId = DataBase.Bills.GetAll().OrderBy(b => b.Id).Last().Id,
                    BookingDate = DateTime.UtcNow,
                    DateOfArrival = booking.DateOfArrival,
                    DaysOfArrival = booking.DaysOfArrival,
                    IsConfirmed = false,
                    IsCanceled = false,
                    UserId = booking.UserId
                };

                DataBase.Bookings.Create(newBooking);
                DataBase.Save();
            }
            catch (Exception ex)
            {
                // Залогировать "Exceptin: " + ex.Message + " in BookingService in MakeBooking method"
                throw ex;
            }
        }

        public void MakeBookingByRequest(RequestDTO request)
        {
            if (request == null)
            {
                throw new ValidationException("Empty request instance argument", "");
            }

            try
            {
                var apartment = Mapper.Map<Apartment, ApartmentDTO>(DataBase.Apartments.Get(request.ApartmentId.Value));

                Bill bill = new Bill
                {
                    BookingDate = DateTime.Now,
                    Price = Calculation.GetSumOfBooking(request.DaysOfStay, apartment.PricePerDay)
                };

                DataBase.Bills.Create(bill);
                DataBase.Save();

                Booking booking = new Booking
                {
                    ApartmentId = request.ApartmentId,
                    BookingDate = DateTime.Now,
                    BillId = DataBase.Bills.GetAll().OrderBy(b => b.Id).Last().Id,
                    DateOfArrival = request.DateOfArrival,
                    DaysOfArrival = request.DaysOfStay,
                    IsCanceled = false,
                    IsConfirmed = false,
                    UserId = request.UserId
                };


                DataBase.Bookings.Create(booking);
                DataBase.Save();
            }
            catch (Exception ex)
            {
                // Залогировать "Exceptin: " + ex.Message + " in BookingService in MakeBookingByRequest method"
                throw ex;
            }
        }

        public void EditBooking(BookingDTO booking)
        {
            if (booking == null)
            {
                throw new ValidationException("Empty booking instance argument", "");
            }

            try
            {
                Mapper.Initialize(cfg => cfg.CreateMap<BookingDTO, Booking>());
                DataBase.Bookings.Update(Mapper.Map<BookingDTO, Booking>(booking));
            }
            catch (Exception ex)
            {
                // Залогировать "Exceptin: " + ex.Message + " in BookingService in EditBooking method"
                throw ex;
            }
        }

        public void DeleteBooking(int? id)
        {
            if (id == null)
            {
                throw new ValidationException("Empty booking id argument", "");
            }

            try
            {
                DataBase.Bookings.Delete(id.Value);
            }
            catch (Exception ex)
            {
                // Залогировать "Exceptin: " + ex.Message + " in BookingService in DeleteBooking method"
                throw ex;
            }
        }

        public IEnumerable<BookingDTO> FindBooking(Func<Booking, bool> predicate)
        {
            if (predicate == null)
            {
                throw new ValidationException("Empty predicate argument", "");
            }
            try
            {
                var booking = DataBase.Bookings.Find(predicate);

                if (booking == null)
                {
                    throw new ValidationException("Booking is not found", "");
                }

                Mapper.Initialize(cfg => cfg.CreateMap<Booking, BookingDTO>());

                return Mapper.Map<IEnumerable<Booking>, IEnumerable<BookingDTO>>(booking);
            }
            catch (Exception ex)
            {
                // Залогировать "Exceptin: " + ex.Message + " in BookingService in FindBooking method"
                throw ex;
            }
        }

        public BookingDTO GetBooking(int? id)
        {
            if (id == null)
            {
                throw new ValidationException("Empty booking id argument", "");
            }

            try
            {
                var booking = DataBase.Bookings.Get(id.Value);

                if (booking == null)
                {
                    throw new ValidationException("Booking is not found", "");
                }

                Mapper.Initialize(cfg => cfg.CreateMap<Booking, BookingDTO>());

                return Mapper.Map<Booking, BookingDTO>(booking);
            }
            catch (Exception ex)
            {
                // Залогировать "Exceptin: " + ex.Message + " in BookingService in GetBooking method"
                throw ex;
            }
        }

        public IEnumerable<DTO.BookingDTO> GetBookings()
        {
            try
            {
                return Mapper.Map<IEnumerable<Booking>, List<BookingDTO>>(DataBase.Bookings.GetAll());
            }
            catch (Exception ex)
            {
                // Залогировать "Exceptin: " + ex.Message + " in BookingService in GetBookings method"
                throw ex;
            }
        }


        public IEnumerable<BillDTO> GetOverDueBills()
        {
            try
            {
                var bills = Mapper.Map<IEnumerable<Bill>, IEnumerable<BillDTO>>(DataBase.Bills.Find(b => b.DateOfPayment == null).Where(b => b.BookingDate.Value.AddDays(2) < DateTime.Now));

                return bills;
            }
            catch (Exception ex)
            {
                // Залогировать "Exceptin: " + ex.Message + " in BookingService in GetOverDueBills method"
                throw ex;
            }
        }

        public void ConfirmBill(int? id)
        {
            try
            {
                var bill = DataBase.Bills.Get(id.Value);

                if (bill.BookingDate.Value.AddDays(2) > DateTime.Now)
                {
                    bill.DateOfPayment = DateTime.Now;
                }
                else
                {
                    throw new ValidationException("Bill with id " + bill.Id.ToString() + " is already overdue", "");
                }
            }
            catch (Exception ex)
            {
                // Залогировать "Exceptin: " + ex.Message + " in BookingService in ConfirmBill method"
                throw ex;
            }
        }

        public List<BillDTO> GetUserUnpaidBills(string userId)
        {
            try
            {
                var bookings = Mapper.Map<IEnumerable<Booking>, IEnumerable<BookingDTO>>(DataBase.Bookings.Find(b => b.UserId == userId).Where(b => b.IsCanceled == false));
                List<BillDTO> bills = new List<BillDTO>();

                foreach (var b in bookings)
                {
                    var bill = DataBase.Bills.Get(b.BillId);

                    if ((bill.DateOfPayment == null || bill.BookingDate.Value.AddDays(2) < bill.DateOfPayment) && !b.IsCanceled)
                    {
                        bills.Add(Mapper.Map<Bill, BillDTO>(bill));
                    }
                }

                return bills;
            }
            catch (Exception ex)
            {
                // Залогировать "Exceptin: " + ex.Message + " in BookingService in GetUserUnpaidBills method"
                throw ex;
            }
        }

        public List<BookingDTO> GetUserBookings(string userId)
        {
            try
            {
                var bookings = Mapper.Map<List<Booking>, List<BookingDTO>>(DataBase.Bookings.Find(b => b.UserId == userId).ToList());

                return bookings;
            }
            catch (Exception ex)
            {
                // Залогировать "Exceptin: " + ex.Message + " in BookingService in GetUserBills method"
                throw ex;
            }
        }

        /// <summary>
        /// Method that cancel bookings that wasn't paid by the time.
        /// </summary>
        public void CancelAllOverDueBills()
        {
            try
            {
                //LoggerInvoker.Logger.Error("CancelAllOverDueBills method in BookingService invoking");
                _logger.Trace("CancelAllOverDueBills method in BookingService invoking");

                var bookings = Mapper.Map<List<Booking>, List<BookingDTO>>(DataBase.Bookings.GetAll().ToList());
                var bills = GetOverDueBills();

                foreach (var bill in bills)
                {
                    var currentBooking = bookings.FirstOrDefault(booking => booking.BillId == bill.Id);
                    currentBooking.IsCanceled = true;
                    DataBase.Bookings.Update(Mapper.Map<BookingDTO, Booking>(currentBooking));
                }

                DataBase.Save();
            }
            catch (Exception ex)
            {
                // Залогировать "Exceptin: " + ex.Message + " in BookingService in GetUserBills method"
                throw ex;
            }
        }

        public List<BillDTO> GetUserBills(string userId)
        {
            try
            {
                var bookings = Mapper.Map<IEnumerable<Booking>, IEnumerable<BookingDTO>>(DataBase.Bookings.Find(b => b.UserId == userId));
                List<BillDTO> bills = new List<BillDTO>();

                foreach (var b in bookings)
                {
                    var bill = DataBase.Bills.Get(b.BillId);
                    bills.Add(Mapper.Map<Bill, BillDTO>(bill));
                }

                return bills;
            }
            catch (Exception ex)
            {
                // Залогировать "Exceptin: " + ex.Message + " in BookingService in GetUserUnpaidBills method"
                throw ex;
            }
        }

        public void CancelBooking(int? id)
        {
            try
            {
                var booking = DataBase.Bookings.Get(id.Value);

                booking.IsCanceled = true;

                DataBase.Bookings.Update(booking);
                DataBase.Save();
            }
            catch (Exception ex)
            {
                // Залогировать "Exceptin: " + ex.Message + " in BookingService in CancelBooking method"
                throw ex;
            }
        }

        public void Dispose()
        {
            DataBase.Dispose();
        }
    }
}
