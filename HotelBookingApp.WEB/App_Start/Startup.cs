using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using HotelBookingApp.BLL.Services;
using Microsoft.AspNet.Identity;
using HotelBookingApp.BLL.Interfaces;
using Microsoft.Owin.Security.Cookies;

[assembly: OwinStartup(typeof(HotelBookingApp.WEB.App_Start.Startup))]

namespace HotelBookingApp.WEB.App_Start
{
    public class Startup
    {
        IServiceCreator serviceCreator = new ServiceCreator();

        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<IUserService>(CreateUserService);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
        }

        private IUserService CreateUserService()
        {
            return serviceCreator.CreateUserService("HotelConnection");
        }
    }
}
