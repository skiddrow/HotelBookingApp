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
    /// Repository class that provides functions for work with data from Bills table in database.
    /// </summary>
    public class BillRepository : IRepository<Bill>
    {
        private HotelContext db;

        public BillRepository(HotelContext dbContext)
        {
            this.db = dbContext;
        }

        public IEnumerable<Bill> GetAll()
        {
            return db.Bills.ToList();
        }

        public Bill Get(int id)
        {
            return db.Bills.AsNoTracking().Where(b => b.Id == id).FirstOrDefault();
        }

        public List<Bill> Find(Func<Bill, bool> predicate)
        {
            return db.Bills.Where(predicate).ToList();
        }

        public void Create(Bill item)
        {
            db.Bills.Add(item);
        }

        public void Update(Bill item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Bill bill = db.Bills.Find(id);

            if (bill != null)
            {
                db.Bills.Remove(bill);
            }
        }
    }
}
