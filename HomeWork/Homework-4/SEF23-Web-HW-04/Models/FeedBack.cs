using System;

namespace SEF23_Web_HW_04.Models
{
    public class FeedBack
    {
        // Fields
        public int Id { get; set; }  // Database ID 
        public string? StudentName { get; set; } // Name of the student providing feedback
        public string? Course { get; set; } // Course for which feedback is given
        public string? Comments { get; set; } // Feedback comments
        public int Rating { get; set; } // Rating out of 5
        public readonly DateTime DateSubmitted = DateTime.Now; // Date when feedback was submitted

        // Constructor
        public FeedBack() 
        {
            // Default constructor
        }

        public FeedBack(string studentName, string course, string comments, int rating) // Parameterized constructor
        {
            StudentName = studentName;
            Course = course;
            Comments = comments;
            Rating = rating;
        }

        public FeedBack(int id, string studentName, string course, string comments, int rating, DateTime dateSubmitted) // Parameterized constructor with ID and Date
        {
            Id = id;
            StudentName = studentName;
            Course = course;
            Comments = comments;
            Rating = rating;
            DateSubmitted = dateSubmitted;
        }
    }
}
