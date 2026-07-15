using SEF23_Web_HW_07.Models.Entities;

namespace SEF23_Web_HW_07.Models.Interfaces
{
    public interface ITravelService
    {
        List<TravelPackage> GetAllPackages(); // Get all Packages
        TravelPackage GetPackageById(int id); // Get a single Package by its ID
        bool CreateBooking(Booking booking); // Create a new booking
        List<Booking> GetUserBookings(string userId); //Get all bookings for a user
        Booking GetBookingById(int bookingId, string userId); // Get a booking by its ID for a specific user
        bool UpdateBookingStatus(int bookingId, string userId, string status); // Update the status of a booking for a specific user
        public List<BookingWithPackage> GetUserBookingsWithDetails(string userId); // Get all bookings with package details for a user
    }
}