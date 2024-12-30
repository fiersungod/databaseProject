using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.RegularExpressions;

namespace databaseProject.Pages.Account
{
    public class CreateAccountModel : PageModel
    {
        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public string ConfirmPassword { get; set; }

        public string ErrorMessage { get; set; }

        public void OnGet()
        {
            // This method runs when the page is first loaded
        }

        public IActionResult OnPost()
        {
            // Basic validation for empty fields
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Email) ||
                string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                ErrorMessage = "All fields are required.";
                return Page();
            }

            // Validate that the passwords match
            if (Password != ConfirmPassword)
            {
                ErrorMessage = "Passwords do not match.";
                return Page();
            }

            // Email Validation (regex pattern to check a valid email format)
            if (!IsValidEmail(Email))
            {
                ErrorMessage = "Please enter a valid email address.";
                return Page();
            }

            // Password Validation
            if (!IsValidPassword(Password))
            {
                ErrorMessage = "Password must be at least 8 characters long, contain one uppercase letter, one lowercase letter, one number, and one special character.";
                return Page();
            }

            // If validation passes, simulate user creation and redirect to login page
            TempData["SuccessMessage"] = "Account created successfully! Please log in.";
            return RedirectToPage("/Account/Login");
        }

        // Helper method for email validation
        private bool IsValidEmail(string email)
        {
            // Simple regex to validate email format
            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return emailRegex.IsMatch(email);
        }

        // Helper method for password validation
        private bool IsValidPassword(string password)
        {
            // Password must have at least 8 characters, 1 uppercase, 1 lowercase, 1 number, and 1 special character
            var passwordRegex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
            return passwordRegex.IsMatch(password);
        }
    }
}
