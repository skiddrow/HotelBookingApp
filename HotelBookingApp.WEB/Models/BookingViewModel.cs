using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelBookingApp.WEB.Models
{
    public class BookingViewModel
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public DateTime? BookingDate { get; set; }

        public int? ApartmentId { get; set; }

        [Required]
        [Display(Name = "Date of arrival")]
        [DataType(DataType.Date)]
        public DateTime DateOfArrival { get; set; }

        [Required]
        [Display(Name = "Days of stay")]
        public int DaysOfArrival { get; set; }

        public int? BillId { get; set; }

        public bool IsConfirmed { get; set; }

        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Number of places")]
        [Range(1, 4)]
        public int NumberOfPlaces { get; set; }

        [Display(Name = "Apartment class")]
        public int ApartmentClassId { get; set; }

        public bool IsCanceled { get; set; }
    }
}