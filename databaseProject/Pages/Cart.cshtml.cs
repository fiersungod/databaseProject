using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace databaseProject.Pages
{
    public class CartModel : PageModel
    {
        private readonly string _connectionString;
        public List<CartItem> CartItems { get; set; }
        public int TotalPrice { get; set; }

        public CartModel(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public void OnGet()
        {
            LoadCartItems();
        }
        [HttpPost]
        public IActionResult OnPostRemove([FromBody]DleteItem item)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string deleteQuery = "DELETE FROM IN_CART WHERE IN_CART_ID = @itemId;";
                using (var command = new MySqlCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@itemId", item.Id);
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }

            LoadCartItems(); // Refresh the cart
            return new JsonResult(new { success = true });
        }

        private void LoadCartItems()
        {
            CartItems = new List<CartItem>();
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT IN_CART_ID, product_name, amount, price, total FROM CART_Product WHERE CART_ID = @cart;";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@cart", databaseProject.User.CartId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CartItems.Add(new CartItem
                            {
                                Id = reader.GetInt32("IN_CART_ID"),
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

            CalculateCartTotal();
        }

        private void CalculateCartTotal()
        {
            TotalPrice = 0;
            foreach (var item in CartItems)
            {
                TotalPrice += item.Total;
            }
        }
    }
    public class DleteItem
    {
        public int Id { get; set; }
    }
    public class CartItem
    {
        public int Id { get; set; }
        public string Product { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public int Total { get; set; }
    }
}
