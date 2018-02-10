using HotelBookingApp.DAL.EF;
using HotelBookingApp.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingApp.DAL.Repositories
{
    public class ClientManagerRepository : IClientManager
    {
        public HotelContext Database { get; set; }

        public ClientManagerRepository(HotelContext db)
        {
            Database = db;
        }

        public void Create(Entities.ClientProfile item)
        {
            Database.ClientProfiles.Add(item);
            Database.SaveChanges();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
