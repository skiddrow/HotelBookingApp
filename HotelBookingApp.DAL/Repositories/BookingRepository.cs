using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelBookingApp.DAL.Interfaces;
using HotelBookingApp.DAL.Entities;
using HotelBookingApp.DAL.EF;
using System.Data.Entity;

namespace HotelBookingApp.DAL.Repositories
{
    /// <summary>
    /// Repository class that provides functions for work with data from Bookings table in database.
    /// </summary>
    public class BookingRepository : IRepository<Booking>
    {
        private HotelContext db;

        public BookingRepository(HotelContext dbContext)
        {
            this.db = dbContext;
        }

        public IEnumerable<Booking> GetAll()
        {
            return db.Bookings.AsNoTracking().ToList();
        }

        public Booking Get(int id)
        {
            return db.Bookings.AsNoTracking().Where(b => b.Id == id).FirstOrDefault();
        }

        public List<Booking> Find(Func<Booking, bool> predicate)
        {
            return db.Bookings.Where(predicate).ToList();
        }

        public void Create(Booking item)
        {
            db.Bookings.Add(item);
        }

        public void Update(Booking item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Booking booking = db.Bookings.Find(id);

            if (booking != null)
            {
                db.Bookings.Remove(booking);
            }
        }
    }
}
