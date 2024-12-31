using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace databaseProject.Pages
{
    public class CheckMem
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class MemInfo
    {
        public string Id { get; set; }
        public string CartId { get; set; }
        public string MemberType { get; set; }
    }
    public class LoginModel : PageModel
    {
        public string ErrorMessage { get; set; }

        public void OnGet()
        {
            // This method runs when the page is first loaded
        }

        public IActionResult OnPost()
        {
            // Basic validation example
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Please fill in both fields.";
                return Page();
            }

            // Replace this with actual authentication logic
            if (Email == "test@example.com" && Password == "password123")
            {
                // Redirect to a members-only area (e.g., a dashboard)
                return RedirectToPage("/Dashboard");
            }
            else
            {
                ErrorMessage = "Invalid email or password.";
                return Page();
            }
        }
    }
}
