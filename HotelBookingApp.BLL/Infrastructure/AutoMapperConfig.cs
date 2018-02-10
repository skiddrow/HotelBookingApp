using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelBookingApp.DAL.Entities;
using HotelBookingApp.BLL.DTO;
using HotelBookingApp.WEB.Models;

namespace HotelBookingApp.BLL.Infrastructure
{
    /// <summary>
    /// Class that initializing AutoMapper config.
    /// </summary>
    public class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Apartment, ApartmentDTO>();
                cfg.CreateMap<ApartmentDTO, Apartment>();
                cfg.CreateMap<ApartmentViewModel, ApartmentDTO>();
                cfg.CreateMap<ApartmentDTO, ApartmentViewModel>();
                cfg.CreateMap<ApartmentClass, ApartmentClassDTO>();
                cfg.CreateMap<ApartmentClassDTO, ApartmentClass>();
                cfg.CreateMap<Booking, BookingDTO>();
                cfg.CreateMap<BookingDTO, Booking>();
                cfg.CreateMap<BookingViewModel, BookingDTO>();
                cfg.CreateMap<BookingDTO, BookingViewModel>();
                cfg.CreateMap<Bill, BillDTO>();
                cfg.CreateMap<BillDTO, Bill>();
                cfg.CreateMap<BillViewModel, BillDTO>();
                cfg.CreateMap<BillDTO, BillViewModel>();
                cfg.CreateMap<Request, RequestDTO>();
                cfg.CreateMap<RequestDTO, Request>();
                cfg.CreateMap<RequestViewModel, RequestDTO>();
                cfg.CreateMap<RequestDTO, RequestViewModel>();
                cfg.CreateMap<ApplicationUser, UserDTO>();
                cfg.CreateMap<UserDTO, UserViewModel>();
                cfg.CreateMap<UserViewModel, UserDTO>();
            });
        }
    }
}
