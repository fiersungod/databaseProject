using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using Mysqlx.Crud;

namespace databaseProject.Pages
{
    public class NewACC
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string CPassword { get; set; }
    }
    public class CreateAccountModel : PageModel
    {
        [BindProperty]
        public NewACC NewUser { get; set; }
        private readonly string _connectionString;
        public string ErrorMessage { get; set; }

        public CreateAccountModel(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public void OnGet()
        {
            // This method runs when the page is first loaded
        }

        public IActionResult OnPostAcc([FromBody]NewACC newACC)
        {
            // Basic validation for empty fields
            if (string.IsNullOrWhiteSpace(newACC.Name) || string.IsNullOrWhiteSpace(newACC.Email) || string.IsNullOrWhiteSpace(newACC.Phone) ||
                string.IsNullOrWhiteSpace(newACC.Password) || string.IsNullOrWhiteSpace(newACC.CPassword))
            {
                ErrorMessage = "All fields are required.";
                return BadRequest(new { success = false });
            }

            // Validate that the passwords match
            if (newACC.Password != newACC.CPassword)
            {
                ErrorMessage = "Passwords do not match.";
                return BadRequest(new { success = false });
            }

            // Email Validation (regex pattern to check a valid email format)
            if (!true)
            {
                ErrorMessage = "Please enter a valid email address.";
                return Page();
            }

            // Password Validation
            if (!true)
            {
                ErrorMessage = "Password must be at least 8 characters long, contain one uppercase letter, one lowercase letter, one number, and one special character.";
                return Page();
            }

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                
                string query = "INSERT INTO MEMBERS (Member_ID,member_type,phone_Number,member_account,member_password) VALUES (@Id,\"Member\",@Phone,@Email,@Password);";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", newACC.Name);
                    command.Parameters.AddWithValue("@Phone", newACC.Phone);
                    command.Parameters.AddWithValue("@Email", newACC.Email);
                    command.Parameters.AddWithValue("@Password", newACC.Password);
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }

            // If validation passes, simulate user creation and redirect to login page
            return new JsonResult(new { success = true });
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
