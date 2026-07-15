using Microsoft.AspNetCore.Mvc.Rendering;

namespace MediBookClinic.Models.ViewModels.Shared
{
    // Helper class for creating SelectListItem collections for dropdowns
    public static class SelectListHelper
    {
        // Blood groups for patient registration
        public static List<SelectListItem> BloodGroups => new List<SelectListItem>
        {
            new SelectListItem { Value = "", Text = "Select Blood Group" },
            new SelectListItem { Value = "A+", Text = "A+" },
            new SelectListItem { Value = "A-", Text = "A-" },
            new SelectListItem { Value = "B+", Text = "B+" },
            new SelectListItem { Value = "B-", Text = "B-" },
            new SelectListItem { Value = "AB+", Text = "AB+" },
            new SelectListItem { Value = "AB-", Text = "AB-" },
            new SelectListItem { Value = "O+", Text = "O+" },
            new SelectListItem { Value = "O-", Text = "O-" }
        };

        // Gender options
        public static List<SelectListItem> Genders => new List<SelectListItem>
        {
            new SelectListItem { Value = "", Text = "Select Gender" },
            new SelectListItem { Value = "Male", Text = "Male" },
            new SelectListItem { Value = "Female", Text = "Female" },
            new SelectListItem { Value = "Other", Text = "Other" },
            new SelectListItem { Value = "Prefer not to say", Text = "Prefer not to say" }
        };

        // Doctor specializations
        public static List<SelectListItem> Specializations => new List<SelectListItem>
        {
            new SelectListItem { Value = "", Text = "All Specializations" },
            new SelectListItem { Value = "Cardiology", Text = "Cardiology" },
            new SelectListItem { Value = "Dermatology", Text = "Dermatology" },
            new SelectListItem { Value = "Endocrinology", Text = "Endocrinology" },
            new SelectListItem { Value = "Gastroenterology", Text = "Gastroenterology" },
            new SelectListItem { Value = "General Practice", Text = "General Practice" },
            new SelectListItem { Value = "Gynecology", Text = "Gynecology" },
            new SelectListItem { Value = "Neurology", Text = "Neurology" },
            new SelectListItem { Value = "Oncology", Text = "Oncology" },
            new SelectListItem { Value = "Ophthalmology", Text = "Ophthalmology" },
            new SelectListItem { Value = "Orthopedics", Text = "Orthopedics" },
            new SelectListItem { Value = "Pediatrics", Text = "Pediatrics" },
            new SelectListItem { Value = "Psychiatry", Text = "Psychiatry" },
            new SelectListItem { Value = "Radiology", Text = "Radiology" },
            new SelectListItem { Value = "Surgery", Text = "Surgery" },
            new SelectListItem { Value = "Urology", Text = "Urology" }
        };

        // Days of week for availability
        public static List<SelectListItem> DaysOfWeek => new List<SelectListItem>
        {
            new SelectListItem { Value = "0", Text = "Sunday" },
            new SelectListItem { Value = "1", Text = "Monday" },
            new SelectListItem { Value = "2", Text = "Tuesday" },
            new SelectListItem { Value = "3", Text = "Wednesday" },
            new SelectListItem { Value = "4", Text = "Thursday" },
            new SelectListItem { Value = "5", Text = "Friday" },
            new SelectListItem { Value = "6", Text = "Saturday" }
        };

        // Appointment status options
        public static List<SelectListItem> AppointmentStatuses => new List<SelectListItem>
        {
            new SelectListItem { Value = "", Text = "All Statuses" },
            new SelectListItem { Value = "Pending", Text = "Pending" },
            new SelectListItem { Value = "Confirmed", Text = "Confirmed" },
            new SelectListItem { Value = "InProgress", Text = "In Progress" },
            new SelectListItem { Value = "Completed", Text = "Completed" },
            new SelectListItem { Value = "Cancelled", Text = "Cancelled" },
            new SelectListItem { Value = "NoShow", Text = "No Show" }
        };

        // Time of day preferences
        public static List<SelectListItem> TimeOfDayPreferences => new List<SelectListItem>
        {
            new SelectListItem { Value = "", Text = "Any Time" },
            new SelectListItem { Value = "Morning", Text = "Morning (8 AM - 12 PM)" },
            new SelectListItem { Value = "Afternoon", Text = "Afternoon (12 PM - 5 PM)" },
            new SelectListItem { Value = "Evening", Text = "Evening (5 PM - 9 PM)" }
        };

        // Slot duration options (in minutes)
        public static List<SelectListItem> SlotDurations => new List<SelectListItem>
        {
            new SelectListItem { Value = "15", Text = "15 minutes" },
            new SelectListItem { Value = "30", Text = "30 minutes" },
            new SelectListItem { Value = "45", Text = "45 minutes" },
            new SelectListItem { Value = "60", Text = "1 hour" }
        };

        // Rating options (1-5 stars)
        public static List<SelectListItem> Ratings => new List<SelectListItem>
        {
            new SelectListItem { Value = "5", Text = "5 Stars - Excellent" },
            new SelectListItem { Value = "4", Text = "4 Stars - Very Good" },
            new SelectListItem { Value = "3", Text = "3 Stars - Good" },
            new SelectListItem { Value = "2", Text = "2 Stars - Fair" },
            new SelectListItem { Value = "1", Text = "1 Star - Poor" }
        };

        // Page size options for pagination
        public static List<SelectListItem> PageSizes => new List<SelectListItem>
        {
            new SelectListItem { Value = "10", Text = "10 per page" },
            new SelectListItem { Value = "25", Text = "25 per page" },
            new SelectListItem { Value = "50", Text = "50 per page" },
            new SelectListItem { Value = "100", Text = "100 per page" }
        };

        // Sort options for lists
        public static List<SelectListItem> SortOptions => new List<SelectListItem>
        {
            new SelectListItem { Value = "name_asc", Text = "Name (A-Z)" },
            new SelectListItem { Value = "name_desc", Text = "Name (Z-A)" },
            new SelectListItem { Value = "date_asc", Text = "Date (Oldest First)" },
            new SelectListItem { Value = "date_desc", Text = "Date (Newest First)" },
            new SelectListItem { Value = "rating_desc", Text = "Rating (High to Low)" },
            new SelectListItem { Value = "rating_asc", Text = "Rating (Low to High)" }
        };

        // Language preferences
        public static List<SelectListItem> Languages => new List<SelectListItem>
        {
            new SelectListItem { Value = "en-US", Text = "English" },
            new SelectListItem { Value = "es-ES", Text = "Spanish" },
            new SelectListItem { Value = "fr-FR", Text = "French" },
            new SelectListItem { Value = "de-DE", Text = "German" },
            new SelectListItem { Value = "ar-SA", Text = "Arabic" },
            new SelectListItem { Value = "ur-PK", Text = "Urdu" }
        };

        // Theme preferences
        public static List<SelectListItem> Themes => new List<SelectListItem>
        {
            new SelectListItem { Value = "light", Text = "Light Mode" },
            new SelectListItem { Value = "dark", Text = "Dark Mode" },
            new SelectListItem { Value = "auto", Text = "Auto (System)" }
        };

        // Create SelectListItem collection from enum
        public static List<SelectListItem> FromEnum<TEnum>() where TEnum : Enum
        {
            return Enum.GetValues(typeof(TEnum))
                .Cast<TEnum>()
                .Select(e => new SelectListItem
                {
                    Value = e.ToString(),
                    Text = e.ToString()
                })
                .ToList();
        }

        // Create SelectListItem collection from dictionary
        public static List<SelectListItem> FromDictionary(Dictionary<string, string> items, bool includeEmpty = false)
        {
            var list = items.Select(kvp => new SelectListItem
            {
                Value = kvp.Key,
                Text = kvp.Value
            }).ToList();

            if (includeEmpty)
            {
                list.Insert(0, new SelectListItem { Value = "", Text = "-- Select --" });
            }

            return list;
        }

        // Create SelectListItem collection from list of objects
        public static List<SelectListItem> FromList<T>(IEnumerable<T> items, Func<T, string> valueSelector, Func<T, string> textSelector, string? selectedValue = null, bool includeEmpty = false)
        {
            var list = items.Select(item => new SelectListItem
            {
                Value = valueSelector(item),
                Text = textSelector(item),
                Selected = selectedValue != null && valueSelector(item) == selectedValue
            }).ToList();

            if (includeEmpty)
            {
                list.Insert(0, new SelectListItem { Value = "", Text = "-- Select --" });
            }

            return list;
        }
    }
}