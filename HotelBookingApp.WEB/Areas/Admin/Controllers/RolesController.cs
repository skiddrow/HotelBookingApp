using AutoMapper;
using HotelBookingApp.BLL.DTO;
using HotelBookingApp.BLL.Interfaces;
using HotelBookingApp.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelBookingApp.WEB.Areas.Admin.Controllers
{
    [Authorize(Roles="Admin")]
    public class RolesController : Controller
    {
        IUserService userService;

        public RolesController(IUserService userService)
        {
            this.userService = userService;
        }

        public ActionResult Index()
        {
            var users = Mapper.Map<List<UserDTO>, List<UserViewModel>>(userService.GetUsers());

            return View();
        }
    }
}