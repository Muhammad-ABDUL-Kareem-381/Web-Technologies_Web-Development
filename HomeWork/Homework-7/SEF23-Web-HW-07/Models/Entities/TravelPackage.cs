namespace SEF23_Web_HW_07.Models.Entities
{
    public class TravelPackage
    {
        // Fields representing the properties of a travel package
        public int PackageId { get; set; } // Unique identifier for the travel package
        public string PackageName { get; set; } = string.Empty; // Name of the travel package
        public string Destination { get; set; } = string.Empty; // Destination of the travel package
        public int Duration { get; set; } // Duration in days
        public decimal Price { get; set; } // Price of the travel package
        public int AvailableSeats { get; set; } // Number of available seats
        public DateTime DepartureDate { get; set; } // Departure date of the travel package
    }
}
