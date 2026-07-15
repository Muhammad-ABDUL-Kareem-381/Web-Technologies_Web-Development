using Microsoft.AspNetCore.Mvc;
using SEF23_Web_HW_04.Models;
using SEF23_Web_HW_04.Models.Repositories;

namespace SEF23_Web_HW_04.Controllers
{
    // Controller for handling feedback-related actions
    public class FeedbackController : Controller
    {
        // Default constructor
        public FeedbackController() 
        {
            // No initialization required
        }

        // Displays the default feedback index page
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // Displays the feedback form to the user
        [HttpGet]
        public IActionResult GiveFeedback()
        {
            return View();
        }

        // Handles submission of the feedback form
        [HttpPost]
        public IActionResult GiveFeedback(FeedBack feedBack)
        {
            // Create repository instance and save feedback
            FeedBackRepositories feedBackRepositories = new FeedBackRepositories();
            feedBackRepositories.GiveFeedback(feedBack);

            // Redirect to summary page with submitted feedback
            return RedirectToAction("FeedbackSummary", feedBack);
        }

        // Displays a summary of the submitted feedback
        [HttpGet]
        public IActionResult FeedbackSummary(FeedBack feedBack)
        {
            return View(feedBack);
        }

        // Displays a list of all feedback entries
        [HttpGet]
        public IActionResult AllFeedbacks()
        {
            // Retrieve all feedbacks from the repository
            FeedBackRepositories feedBackRepositories = new FeedBackRepositories();
            List<FeedBack> feedbacks = feedBackRepositories.AllFeedbacks();
            return View(feedbacks);
        }
    }
}
