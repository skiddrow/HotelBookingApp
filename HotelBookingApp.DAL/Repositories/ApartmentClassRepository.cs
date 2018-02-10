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
    /// Repository class that provides functions for work with data from ApartmentClasses table in database.
    /// </summary>
    public class ApartmentClassRepository : IRepository<ApartmentClass>
    {
        private HotelContext db;

        public ApartmentClassRepository(HotelContext dbContext)
        {
            this.db = dbContext;
        }

        public IEnumerable<ApartmentClass> GetAll()
        {
            return db.ApartmentClasses.ToList();
        }

        public ApartmentClass Get(int id)
        {
            return db.ApartmentClasses.Find(id);
        }

        public List<ApartmentClass> Find(Func<ApartmentClass, bool> predicate)
        {
            return db.ApartmentClasses.Where(predicate).ToList();
        }

        public void Create(ApartmentClass item)
        {
            db.ApartmentClasses.Add(item);
        }

        public void Update(ApartmentClass item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            ApartmentClass appClass = db.ApartmentClasses.Find(id);

            if (appClass != null)
            {
                db.ApartmentClasses.Remove(appClass);
            }
        }
    }
}
