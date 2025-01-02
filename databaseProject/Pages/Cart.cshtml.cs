using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace databaseProject.Pages
{
    public class CartModel : PageModel
    {
        private readonly string _connectionString;
        // A list to hold the cart items
        public List<CartItem> CartItems { get; set; }

        // Total price of the cart
        public int TotalPrice { get; set; }

        public CartModel(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public void OnGet()
        {
            CartItems = new List<CartItem>();
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT IN_CART_ID,product_name, amount, price, total FROM CART_Product WHERE CART_ID = @cart;";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@cart", databaseProject.User.CartId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CartItems.Add(new CartItem
                            {
                                Id = reader.GetString("IN_CART_ID"),
                                Product = reader.GetString("product_name"),
                                Price = reader.GetInt32("price"),
                                Quantity = reader.GetInt32("amount"),
                                Total = reader.GetInt32("total")
                            });
                        }
                    }
                }
                connection.Close();
            }
            // Calculate the total price of the cart
            CalculateCartTotal();
        }

        // Calculate the total price of the cart based on quantities and prices
        private void CalculateCartTotal()
        {
            TotalPrice = 0;
            foreach (var item in CartItems)
            {
                TotalPrice += item.Total;
            }
        }
    }

    // CartItem class to represent an item in the cart
    public class CartItem
    {
        public string Id { get; set; }
        public string Product { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public int Total { get; set; }
    }
}
