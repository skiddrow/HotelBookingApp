using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelBookingApp.DAL.Entities;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HotelBookingApp.DAL.EF
{
    /// <summary>
    /// Class that provide functionality for working with data from database.
    /// </summary>
    public class HotelContext : IdentityDbContext<ApplicationUser>
    {
        public HotelContext()
            : base() 
        { }

        public HotelContext(string connectionString)
            : base("DefaultConnection")
        { }

        //static HotelContext()
        //{
        //    Database.SetInitializer<HotelContext>(new DbInitializer());
        //}

        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<ApartmentClass> ApartmentClasses { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Request> Requests { get; set; }    
        public DbSet<ClientProfile> ClientProfiles { get; set; }
    }

    public class DbInitializer : DropCreateDatabaseAlways<HotelContext>
    {
        protected override void Seed(HotelContext db)
        {
            db.ApartmentClasses.Add(new ApartmentClass { Name = "Lux"});
            db.SaveChanges();
        }
    }
}
