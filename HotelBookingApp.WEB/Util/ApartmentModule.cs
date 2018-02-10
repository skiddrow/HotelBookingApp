using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject.Modules;
using HotelBookingApp.BLL.Interfaces;
using HotelBookingApp.BLL.Services;

namespace HotelBookingApp.WEB.Util
{
    public class ApartmentModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IApartmentService>().To<ApartmentService>();
            Bind<IRequestService>().To<RequestService>();
            Bind<IBookingService>().To<BookingService>();
            Bind<IUserService>().To<UserService>();
        }
    }
}