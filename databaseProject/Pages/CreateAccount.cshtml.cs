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
        //[BindProperty]
        //public NewACC NewUser { get; set; }

        private readonly string _connectionString;

        public CreateAccountModel(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public void OnGet()
        {
            // This method runs when the page is first loaded
        }

        public IActionResult OnPostCreate([FromBody]NewACC newACC)
        {
            if (newACC == null)
            {
                return BadRequest(new { success = false, message = "Received data is null" });
            }

            // Validate that the passwords match
            if (newACC.Password != newACC.CPassword)
            {
                return BadRequest(new { success = false, message= "Passwords do not match." });
            }

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                
                string query = "INSERT INTO MEMBERS (Member_ID,member_type,phone_Number,member_account,member_password) VALUES (@Id,\"Member\",@Phone,@Email,@Password); " +
                    "INSERT INTO CART (Cart_ID,Member_ID) VALUES(@Id,@Id);";
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
            return new JsonResult(new { success = true, message= "sign up success" });
        }
    }
}
