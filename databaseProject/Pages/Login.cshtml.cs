using MySql.Data.MySqlClient;
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
        private readonly string _connectionString;
        public List<MemInfo> memInfo { get; set; }
        public string ErrorMessage { get; set; }

        public LoginModel(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public void OnGet()
        {
            // This method runs when the page is first loaded
        }

        public IActionResult OnPostLogin([FromBody] CheckMem login)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT Member_ID, Cart_ID, member_type FROM MEMBER_CART WHERE member_account = @Email AND member_password = @Password;";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", login.Email);
                    command.Parameters.AddWithValue("@Password", login.Password);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            memInfo.Add(new MemInfo
                            {
                                Id = reader.GetString("Member_ID"), 
                                CartId = reader.GetString("Cart_ID"),
                                MemberType = reader.GetString("member_type")
                            });
                        }
                    }
                }
                connection.Close();
            }

            if (memInfo.Count>0)
            {
                return new JsonResult(new { success = true });
            }
            else
            {
                ErrorMessage = "Invalid email or password.";
                return BadRequest(new { success = false });
            }
        }
    }
}
