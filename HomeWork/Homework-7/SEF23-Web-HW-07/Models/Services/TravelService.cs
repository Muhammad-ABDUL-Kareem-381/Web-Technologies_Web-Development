using Microsoft.Data.SqlClient;
using SEF23_Web_HW_07.Models.Entities;
using SEF23_Web_HW_07.Models.Interfaces;

public class TravelService : ITravelService
{
    private readonly string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TravelBookingDB;Integrated Security=True;";
    private readonly SqlConnection connection;

    public TravelService()
    {
        connection = new SqlConnection(connectionString);
    }

    public bool CreateBooking(Booking booking)
    {
        if (booking == null || string.IsNullOrEmpty(booking.UserId))
        {
            return false;
        }
        connection.Open();
        // Updated query with UserId and Status
        string query = "INSERT INTO Bookings (UserId, PackageId, CustomerName, CustomerEmail, NumberOfTravelers, TotalAmount, BookingDate, Status) VALUES (@UserId, @PackageId, @CustomerName, @CustomerEmail, @NumberOfTravelers, @TotalAmount, @BookingDate, @Status)";
        SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@UserId", booking.UserId);
        cmd.Parameters.AddWithValue("@PackageId", booking.PackageId);
        cmd.Parameters.AddWithValue("@CustomerName", booking.CustomerName);
        cmd.Parameters.AddWithValue("@CustomerEmail", booking.CustomerEmail);
        cmd.Parameters.AddWithValue("@NumberOfTravelers", booking.NumberOfTravelers);
        cmd.Parameters.AddWithValue("@TotalAmount", booking.TotalAmount);
        cmd.Parameters.AddWithValue("@BookingDate", booking.BookingDate);
        cmd.Parameters.AddWithValue("@Status", booking.Status);
        int rowsAffected = cmd.ExecuteNonQuery();
        connection.Close();
        return rowsAffected > 0;
    }

    public List<Booking> GetUserBookings(string userId)
    {
        if (string.IsNullOrEmpty(userId))
        {
            return new List<Booking>();
        }
        connection.Open();
        string query = "SELECT * FROM Bookings WHERE UserId = @UserId ORDER BY BookingDate DESC";
        SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@UserId", userId);
        List<Booking> bookings = new List<Booking>();
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            Booking booking = new Booking
            {
                BookingId = (int)reader["BookingId"],
                UserId = (string)reader["UserId"],
                PackageId = (int)reader["PackageId"],
                CustomerName = (string)reader["CustomerName"],
                CustomerEmail = (string)reader["CustomerEmail"],
                NumberOfTravelers = (int)reader["NumberOfTravelers"],
                TotalAmount = (decimal)reader["TotalAmount"],
                BookingDate = (DateTime)reader["BookingDate"],
                Status = (string)reader["Status"]
            };
            bookings.Add(booking);
        }
        connection.Close();
        return bookings;
    }
    public Booking GetBookingById(int bookingId, string userId)
    {
        if (bookingId <= 0 || string.IsNullOrEmpty(userId))
        {
            return new Booking();
        }
        connection.Open();
        string query = "SELECT * FROM Bookings WHERE BookingId = @BookingId AND UserId = @UserId";
        SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@BookingId", bookingId);
        cmd.Parameters.AddWithValue("@UserId", userId);
        Booking booking = new Booking();
        SqlDataReader reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            booking.BookingId = (int)reader["BookingId"];
            booking.UserId = (string)reader["UserId"];
            booking.PackageId = (int)reader["PackageId"];
            booking.CustomerName = (string)reader["CustomerName"];
            booking.CustomerEmail = (string)reader["CustomerEmail"];
            booking.NumberOfTravelers = (int)reader["NumberOfTravelers"];
            booking.TotalAmount = (decimal)reader["TotalAmount"];
            booking.BookingDate = (DateTime)reader["BookingDate"];
            booking.Status = (string)reader["Status"];
        }
        connection.Close();
        return booking;
    }
    public bool UpdateBookingStatus(int bookingId, string userId, string status)
    {
        if (bookingId <= 0 || string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(status))
        {
            return false;
        }
        connection.Open();
        string query = "UPDATE Bookings SET Status = @Status WHERE BookingId = @BookingId AND UserId = @UserId";
        SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@Status", status);
        cmd.Parameters.AddWithValue("@BookingId", bookingId);
        cmd.Parameters.AddWithValue("@UserId", userId);
        int rowsAffected = cmd.ExecuteNonQuery();
        connection.Close();
        return rowsAffected > 0;
    }

    public List<TravelPackage> GetAllPackages()
    {
        // Open database connection
        connection.Open();
        // SQL query to select all packages
        string query = "SELECT * FROM TravelPackages";
        SqlCommand cmd = new SqlCommand(query, connection);
        // List to hold results
        List<TravelPackage> packages = new List<TravelPackage>();
        // Execute reader and map each row to a TravelPackage
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            TravelPackage package = new TravelPackage
            {
                PackageId = (int)reader["PackageId"],
                PackageName = (string)reader["PackageName"],
                Destination = (string)reader["Destination"],
                Duration = (int)reader["Duration"],
                Price = (decimal)reader["Price"],
                AvailableSeats = (int)reader["AvailableSeats"],
                DepartureDate = (DateTime)reader["DepartureDate"]
            };
            packages.Add(package);
        }
        // Close connection and return results
        connection.Close();
        return packages;
    }

    // Get a single travel package by its ID
    public TravelPackage GetPackageById(int id)
    {
        // Validate id early
        if (id <= 0)
        {
            return new TravelPackage();
        }
        // Open connection and prepare parameterized query to avoid SQL injection
        connection.Open();
        string query = "SELECT * FROM TravelPackages WHERE PackageId = @PackageId";
        SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@PackageId", id);

        // Execute reader and map the first matching row
        SqlDataReader reader = cmd.ExecuteReader();
        TravelPackage package = new TravelPackage();
        if (reader.Read())
        {
            package.PackageId = (int)reader["PackageId"];
            package.PackageName = (string)reader["PackageName"];
            package.Destination = (string)reader["Destination"];
            package.Duration = (int)reader["Duration"];
            package.Price = (decimal)reader["Price"];
            package.AvailableSeats = (int)reader["AvailableSeats"];
            package.DepartureDate = (DateTime)reader["DepartureDate"];
        }
        // Close connection and return the package (empty if not found)
        connection.Close();
        return package;
    }

    // Get user bookings with package details
    public List<BookingWithPackage> GetUserBookingsWithDetails(string userId)
    {
        if (string.IsNullOrEmpty(userId))
        {
            return new List<BookingWithPackage>();
        }
        connection.Open();
        string query = "SELECT b.*, p.PackageName, p.Destination FROM Bookings b INNER JOIN TravelPackages p ON b.PackageId = p.PackageId WHERE b.UserId = @UserId ORDER BY b.BookingDate DESC";
        SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@UserId", userId);
        List<BookingWithPackage> bookings = new List<BookingWithPackage>();
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            var booking = new BookingWithPackage
            {
                BookingId = (int)reader["BookingId"],
                UserId = (string)reader["UserId"],
                PackageId = (int)reader["PackageId"],
                PackageName = (string)reader["PackageName"],
                Destination = (string)reader["Destination"],
                CustomerName = (string)reader["CustomerName"],
                CustomerEmail = (string)reader["CustomerEmail"],
                NumberOfTravelers = (int)reader["NumberOfTravelers"],
                TotalAmount = (decimal)reader["TotalAmount"],
                BookingDate = (DateTime)reader["BookingDate"],
                Status = (string)reader["Status"]
            };
            bookings.Add(booking);
        }
        connection.Close();
        return bookings;
    }
}