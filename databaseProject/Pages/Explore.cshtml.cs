using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace databaseProject.Pages
{
    public class ExploreModel : PageModel
    {
        private readonly string _connectionString;

        public ExploreModel(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Shop> Shops { get; set; } = new List<Shop>();

        public void OnGet(string searchQuery)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT Shop_ID, image, location, describition, business_time FROM SHOP";
                if (!string.IsNullOrEmpty(searchQuery))
                {
                    query += " WHERE location LIKE @SearchQuery OR describition LIKE @SearchQuery";
                }

                using (var command = new MySqlCommand(query, connection))
                {
                    if (!string.IsNullOrEmpty(searchQuery))
                    {
                        command.Parameters.AddWithValue("@SearchQuery", "%" + searchQuery + "%");
                    }

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

        public class Shop
    {
        public string Shop_ID { get; set; }
        public string Image { get; set; }
        public string Location { get; set; }
        public string Describition { get; set; }
        public string Business_Time { get; set; }
    }
    
}

