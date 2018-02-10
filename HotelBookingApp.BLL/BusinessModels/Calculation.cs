using HotelBookingApp.BLL.DTO;
using HotelBookingApp.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingApp.BLL.BusinessModels
{
    /// <summary>
    /// Class that executing operations with all calculatings.
    /// </summary>
    public class Calculation
    {
        /// <summary>
        /// Method that calculating sum of order.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="apartment"></param>
        /// <returns></returns>
        public static decimal GetSumOfBooking(int daysOfStay, decimal pricePerDay)
        {
            decimal totalSum = daysOfStay * pricePerDay;

            return totalSum;
        }
    }
}
