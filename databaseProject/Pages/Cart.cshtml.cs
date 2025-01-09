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

        [HttpPost]
        public IActionResult OnPostBuy()
        {
            string memberId = databaseProject.User.UserId;
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string q = @"
                SELECT Business_ID
                FROM Who_Product
                WHERE Product_ID = @productId;";
                var businessId = "";
                using (var command = new MySqlCommand(q, connection))
                {
                    command.Parameters.AddWithValue("@ProductId", CartItems[0].ProductId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            businessId = reader.GetString("Business_ID");
                        }
                    }
                }
                string orderId = $@"{memberId}{DateTime.Now.Hour}{DateTime.Now.Minute}{DateTime.Now.Second}";
                string orderQuery = "INSERT INTO ORDERS(Order_ID,Member_ID,Business_ID,order_time,order_states) VALUES (@orderId,@memberId,@businessId,CURRENT_TIME(),\"Checking\");";
                using (var command = new MySqlCommand(orderQuery, connection))
                {
                    command.Parameters.AddWithValue("@orderId", orderId);
                    command.Parameters.AddWithValue("@memberId", memberId);
                    command.Parameters.AddWithValue("@businessId", businessId);

                    command.ExecuteNonQuery();
                }
                int i = 0;
                foreach (var item in CartItems)
                {
                    i++;
                    string inOrderQuery = "INSERT INTO IN_ORDER(InOrder_ID,Product_ID,Order_ID,amount) VALUES(@inOrderId,@productId,@orderId,@amount);";
                    using (var command = new MySqlCommand(inOrderQuery, connection))
                    {
                        command.Parameters.AddWithValue("@inOrderId", orderId + i.ToString());
                        command.Parameters.AddWithValue("@productId", item.Id);
                        command.Parameters.AddWithValue("@orderId", orderId);
                        command.Parameters.AddWithValue("@amount", item.Quantity);

                        command.ExecuteNonQuery();
                    }
                }


                string deleteQuery = "DELETE FROM IN_CART WHERE Cart_ID = @cartId;";
                using (var command = new MySqlCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@cartId", databaseProject.User.CartId);
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

                string query = "SELECT IN_CART_ID, Product_ID, product_name, amount, price, total FROM CART_Product WHERE CART_ID = @cart;";
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
                                ProductId = reader.GetString("Product_ID"),
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
        public string ProductId { get; set; }
        public string Product { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public int Total { get; set; }
    }
}
