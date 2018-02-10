using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelBookingApp.DAL.EF;
using HotelBookingApp.DAL.Entities;
using HotelBookingApp.DAL.Interfaces;

namespace HotelBookingApp.DAL.Repositories
{
    /// <summary>
    /// Class that implements functions of IUnitOfWork interface that provides work with repositories.
    /// </summary>
    public class EFUnitOfWork : IUnitOfWork
    {
        private HotelContext db;
        private ApartmentRepository apartamentRepository;
        private ApartmentClassRepository apartamentClassRepository;
        private BillRepository billRepository;
        private BookingRepository bookingRepository;
        private RequestRepository requestRepository;
        private bool disposed = false;

        public EFUnitOfWork(string connectionString)
        {
            db = new HotelContext(connectionString);
        }

        public IRepository<Apartment> Apartments
        {
            get
            {
                if (apartamentRepository == null)
                {
                    apartamentRepository = new ApartmentRepository(db);
                }

                return apartamentRepository;
            }
        }

        public IRepository<ApartmentClass> ApartmentClasses
        {
            get
            {
                if (apartamentClassRepository == null)
                {
                    apartamentClassRepository = new ApartmentClassRepository(db);
                }

                return apartamentClassRepository;
            }
        }

        public IRepository<Bill> Bills
        {
            get
            {
                if (billRepository == null)
                {
                    billRepository = new BillRepository(db);
                }

                return billRepository;
            }
        }

        public IRepository<Booking> Bookings
        {
            get
            {
                if (bookingRepository == null)
                {
                    bookingRepository = new BookingRepository(db);
                }

                return bookingRepository;
            }
        }

        public IRepository<Request> Requests
        {
            get
            {
                if (requestRepository == null)
                {
                    requestRepository = new RequestRepository(db);
                }

                return requestRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }

                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
