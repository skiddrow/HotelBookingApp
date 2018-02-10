using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelBookingApp.WEB.Models
{
    public class ApartmentViewModel
    {
        public int Id { get; set; }

        [Required]
        public int ApartmentClassId { get; set; }

        [Required]
        [Display(Name="Apartment number")]
        public int Number { get; set; }

        [Required]
        [Display(Name = "Number of places")]
        public int NumberOfPlaces { get; set; }

        [Required]
        public int Floor { get; set; }

        [Required]
        [Display(Name = "Number of rooms")]
        public int NumberOfRooms { get; set; }

        [Required]
        [Display(Name = "Price per day")]
        public decimal PricePerDay { get; set; }

        public bool IsClosed { get; set; }

        public bool Closeable { get; set; }
    }
}