using HotelBookingApp.DAL.EF;
using HotelBookingApp.DAL.Entities;
using HotelBookingApp.DAL.Identity;
using HotelBookingApp.DAL.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingApp.DAL.Repositories
{
    public class IdentityUnitOfWork : IUnitOfWorkIdentity
    {
        private HotelContext _db;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        private IClientManager _clientManagerRep;
        private bool _disposed = false;

        public IdentityUnitOfWork(string connectionString)
        {
            _db = new HotelContext(connectionString);
            _userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(_db));
            _roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(_db));
            _clientManagerRep = new ClientManagerRepository(_db);
        }

        public ApplicationUserManager UserManager
        {
            get { return _userManager; }
        }

        public IClientManager ClientManager
        {
            get { return _clientManagerRep; }
        }

        public ApplicationRoleManager RoleManager
        {
            get { return _roleManager; }
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _userManager.Dispose();
                    _roleManager.Dispose();
                    _clientManagerRep.Dispose();
                }
                this._disposed = true;
            }
        }
    }
}
