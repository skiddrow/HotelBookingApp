using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelBookingApp.DAL.Entities;
using HotelBookingApp.DAL.Identity;

namespace HotelBookingApp.DAL.Interfaces
{
    /// <summary>
    /// Interface for working with repositories.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Apartment> Apartments { get; }
        IRepository<ApartmentClass> ApartmentClasses { get; }
        IRepository<Bill> Bills { get; }
        IRepository<Booking> Bookings { get; }
        IRepository<Request> Requests { get; }
        void Save();
    }
}
