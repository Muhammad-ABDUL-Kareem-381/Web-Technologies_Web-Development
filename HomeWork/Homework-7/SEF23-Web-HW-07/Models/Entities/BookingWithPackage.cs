using System;
using System.Collections.Generic;

namespace SEF23_Web_HW_07.Models.Entities
{
    public class BookingWithPackage
    {
        public int BookingId { get; set; } // Unique identifier for the booking
        public string UserId { get; set; } = string.Empty; // User ID of the person who made the booking
        public int PackageId { get; set; } // ID of the travel package being booked
        public string PackageName { get; set; } = string.Empty; // Name of the travel package
        public string Destination { get; set; } = string.Empty; // Destination of the travel package
        public string CustomerName { get; set; } = string.Empty; // Name of the customer making the booking
        public string CustomerEmail { get; set; } = string.Empty; // Email of the customer making the booking
        public int NumberOfTravelers { get; set; } // Number of travelers included in the booking
        public decimal TotalAmount { get; set; } // Total amount for the booking
        public DateTime BookingDate { get; set; } // Date when the booking was made 
        public string Status { get; set; } = string.Empty; // Status of the booking (e.g., Pending, Confirmed, Cancelled)
    }
}