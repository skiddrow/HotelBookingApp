using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Mvc;
using HotelBookingApp.WEB.Util;
using HotelBookingApp.BLL.Infrastructure;

namespace HotelBookingApp.WEB
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            NinjectModule apartmentModule = new ApartmentModule();
            NinjectModule serviceModule = new ServiceModule("HotelConnection");
            var kernel = new StandardKernel(apartmentModule, serviceModule);
            kernel.Unbind<ModelValidatorProvider>();
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));

            // Initializing AutoMapper config
            AutoMapperConfig.Initialize();
        }
    }
}
