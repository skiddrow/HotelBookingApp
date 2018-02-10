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
    /// Repository class that provides functions for work with data from Requests table in database.
    /// </summary>
    public class RequestRepository : IRepository<Request>
    {
        private HotelContext db;

        public RequestRepository(HotelContext dbContext)
        {
            this.db = dbContext;
        }

        public IEnumerable<Request> GetAll()
        {
            return db.Requests.ToList();
        }

        public Request Get(int id)
        {
            //return db.Requests.Find(id);
            return db.Requests.AsNoTracking().Where(r => r.Id == id).FirstOrDefault();
        }

        public Request GetAsNoTracking(int id)
        {
            return db.Requests.AsNoTracking().First(r => r.Id == id);
        }

        public List<Request> Find(Func<Request, bool> predicate)
        {
            return db.Requests.Where(predicate).ToList();
        }

        public void Create(Request item)
        {
            db.Requests.Add(item);
        }

        public void Update(Request item)
        {
            //db.Entry(item).State = EntityState.Detached;
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Request req = db.Requests.Find(id);

            if (req != null)
            {
                db.Requests.Remove(req);
            }
        }
    }
}
