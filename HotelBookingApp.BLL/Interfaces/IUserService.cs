using HotelBookingApp.BLL.DTO;
using HotelBookingApp.BLL.Infrastructure;
using HotelBookingApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingApp.BLL.Interfaces
{
    /// <summary>
    /// Interface that defines functionality of User service
    /// </summary>
    public interface IUserService : IDisposable
    {
        Task<OperationDetails> Create(UserDTO userDto);
        Task<ClaimsIdentity> Authenticate(UserDTO userDto);
        Task SetInitialData(UserDTO adminDto, List<string> roles);
        List<UserDTO> GetUsers();
    }
}
