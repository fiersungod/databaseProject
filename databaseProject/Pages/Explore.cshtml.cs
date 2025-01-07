using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace databaseProject.Pages
{
    public class ExploreModel : PageModel
    {
        private readonly string _connectionString;
        // Define a property to hold the list of shops (even if it's empty for now)
        public List<Shop> Shops { get; set; } = new List<Shop>();

        public ExploreModel(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public void OnGet()
        {
            // Initialize the Shops list, or leave it empty for now
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM SHOP;";
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Shops.Add(new Shop
                            {
                                Shop_ID = reader.GetString("Shop_ID"),
                                Image = reader.GetString("image"),
                                Location = reader.GetString("location"),
                                Describition = reader.GetString("describition"),
                                Business_Time = reader.GetString("business_time")
                            });
                        }
                    }
                }
                connection.Close();
            }
        }
    }

    // Create a basic Shop class for now (you can expand it later)
    public class Shop
    {
        public string Shop_ID { get; set; }
        public string Image { get; set; }
        public string Location { get; set; }
        public string Describition { get; set; }
        public string Business_Time { get; set; }
    }
}

