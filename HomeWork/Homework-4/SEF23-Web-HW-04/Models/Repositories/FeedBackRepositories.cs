using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;

namespace SEF23_Web_HW_04.Models.Repositories // Feedback Repository for accessing database
{
    // Repository class for handling feedback-related database operations
    public class FeedBackRepositories
    {
        // Connection string for the local SQL Server database
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FeedBacks;Integrated Security=True;";
        SqlConnection connection;

        // Constructor initializes the SQL connection
        public FeedBackRepositories()
        {
            connection = new SqlConnection(connectionString);
        }

        // Adds a feedback entry to the database
        public void GiveFeedback(FeedBack feedback)
        {
            // Return if feedback is null
            if (feedback == null)
            {
                return;
            }

            // SQL query to insert feedback data
            string query = "INSERT INTO Feedbacks (StudentName, Course, Comments, Rating, DateSubmitted) VALUES (@StudentName, @Course, @Comments, @Rating, @DateSubmitted)";
            SqlCommand cmd = new SqlCommand(query, connection);

            // Handle StudentName: set to "Anonymous" if null, empty, or whitespace
            if (string.IsNullOrEmpty(feedback.StudentName) || string.IsNullOrWhiteSpace(feedback.StudentName))
            {
                feedback.StudentName = "Anonymous";
            }

            // Trim and add StudentName parameter
            cmd.Parameters.AddWithValue("@StudentName", (object)feedback.StudentName.Trim());

            // Handle Course: set to "Not Specified" if null, empty, or whitespace
            if (string.IsNullOrEmpty(feedback.Course) || string.IsNullOrWhiteSpace(feedback.Course))
            {
                feedback.Course = "Not Specified";
            }

            // Trim and add Course parameter
            cmd.Parameters.AddWithValue("@Course", (object)feedback.Course.Trim());

            // If comments are empty or whitespace, set default value
            if (string.IsNullOrEmpty(feedback.Comments) || string.IsNullOrWhiteSpace(feedback.Comments))
            {
                feedback.Comments = "No Comments";
            }
            // Add Comments parameter
            cmd.Parameters.AddWithValue("@Comments", (object)feedback.Comments);

            // Add Rating parameter
            cmd.Parameters.AddWithValue("@Rating", feedback.Rating);

            // Add DateSubmitted parameter
            cmd.Parameters.AddWithValue("@DateSubmitted", feedback.DateSubmitted);

            // Open connection, execute query, then close connection
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
            return;
        }

        // Retrieves all feedback entries from the database
        public List<FeedBack> AllFeedbacks()
        {
            List<FeedBack> feedbacks = new List<FeedBack>();
            // SQL query to select all feedback records
            string qurey = "Select * from Feedbacks";
            SqlCommand cmd = new SqlCommand(qurey, connection);

            // Open connection and execute reader
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            // Read each record and map to FeedBack object
            while (reader.Read())
            {
                FeedBack feedback = new FeedBack
                (
                    (int)reader["Id"], // Feedback ID
                    (string)reader["StudentName"], // Student Name
                    (string)reader["Course"], // Course Name
                    (string)reader["Comments"], // Comments
                    (int)reader["Rating"], // Rating
                    (DateTime)reader["DateSubmitted"] // Date Submitted
                );
                feedbacks.Add(feedback);
            }
            // Close connection
            connection.Close();
            return feedbacks;
        }
    }
}