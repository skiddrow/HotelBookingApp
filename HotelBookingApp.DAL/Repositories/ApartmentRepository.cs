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
    /// Repository class that provides functions for work with data from Apartments table in database.
    /// </summary>
    public class ApartmentRepository : IRepository<Apartment>
    {
        private HotelContext db;

        public ApartmentRepository(HotelContext dbContext)
        {
            this.db = dbContext;
        }

        public IEnumerable<Apartment> GetAll()
        {
            return db.Apartments.ToList();
        }

        public Apartment Get(int id)
        {
            return db.Apartments.AsNoTracking().Where(a => a.Id == id).FirstOrDefault();
        }

        public List<Apartment> Find(Func<Apartment, bool> predicate)
        {
            return db.Apartments.Where(predicate).ToList();
        }

        public void Create(Apartment item)
        {
            db.Apartments.Add(item);
        }

        public void Update(Apartment item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Apartment app = db.Apartments.Find(id);

            if (app != null)
            {
                db.Apartments.Remove(app);
            }
        }
    }
}
