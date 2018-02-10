using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelBookingApp.WEB.Models
{
    public class RequestViewModel
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        [Required]
        [Display(Name = "Number of places")]
        [Range(1,4)]
        public int NumberOfPlaces { get; set; }

        [Required]
        [Display(Name = "Date of arrival")]
        [DataType(DataType.Date)]
        public DateTime DateOfArrival { get; set; }

        [Required]
        [Display(Name = "Days of stay")]
        public int DaysOfStay { get; set; }

        [Display(Name = "Apartment class")]
        public int ApartmentClassId { get; set; }

        public DateTime RequestDate { get; set; }

        public bool IsProcessed { get; set; }

        public bool IsConfirmed { get; set; }

        public bool IsCanceled { get; set; }

        public string Message { get; set; }
    }
}