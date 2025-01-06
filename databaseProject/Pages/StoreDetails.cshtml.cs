using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace databaseProject.Pages
{
    public class StoreDetailsModel : PageModel
    {
        private readonly string _connectionString;

        public Shop ShopDetails { get; set; } = new Shop();
        public List<Product> Products { get; set; } = new List<Product>();

        public StoreDetailsModel(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // GET method for displaying shop details and products
        public IActionResult OnGet(string shopId)
        {
            if (string.IsNullOrEmpty(shopId))
            {
                //Console.WriteLine("No shopId provided in query string.");
                return RedirectToPage("/Explore");
            }

            //Console.WriteLine($"Received shopId: {shopId}");

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                // Get shop details
                string shopQuery = "SELECT * FROM SHOP WHERE Shop_ID = @ShopId;";
                Console.WriteLine($"Executing query: {shopQuery} with ShopId: {shopId}");

                using (var command = new MySqlCommand(shopQuery, connection))
                {
                    command.Parameters.AddWithValue("@ShopId", shopId);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            ShopDetails = new Shop
                            {
                                Shop_ID = reader["Shop_ID"] as string ?? string.Empty,
                                Image = !reader.IsDBNull(reader.GetOrdinal("image"))
                                    ? reader.GetString("image")
                                    : "default-image.jpg", // Provide a default image
                                Location = !reader.IsDBNull(reader.GetOrdinal("location"))
                                    ? reader.GetString("location")
                                    : "Location not available",
                                Describition = !reader.IsDBNull(reader.GetOrdinal("describition"))
                                    ? reader.GetString("describition")
                                    : "Description not available",
                                Business_Time = !reader.IsDBNull(reader.GetOrdinal("business_time"))
                                    ? reader.GetString("business_time")
                                    : "Business hours not available"
                            };
                        }
                        else
                        {
                            Console.WriteLine("No shop found with the given shopId.");
                            return RedirectToPage("/Explore");
                        }
                    }
                }

                // Fetch products for the shop
                string productQuery = "SELECT * FROM PRODUCT WHERE Shop_ID = @ShopId;";
                

                Products = new List<Product>();
                using (var command = new MySqlCommand(productQuery, connection))
                {
                    command.Parameters.AddWithValue("@ShopId", shopId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Products.Add(new Product
                            {
                                Product_ID = reader["Product_ID"] as string ?? string.Empty,
                                Product_Name = !reader.IsDBNull(reader.GetOrdinal("product_name"))
                                    ? reader.GetString("product_name")
                                    : "Unknown Product",
                                Discount = !reader.IsDBNull(reader.GetOrdinal("discount"))
                                    ? reader.GetDouble("discount")
                                    : 1.0,
                                Image = !reader.IsDBNull(reader.GetOrdinal("image"))
                                    ? reader.GetString("image")
                                    : "default-product.jpg", // Provide a default product image
                                Price = !reader.IsDBNull(reader.GetOrdinal("price"))
                                    ? reader.GetInt32("price")
                                    : 0,
                                Stock = !reader.IsDBNull(reader.GetOrdinal("stock"))
                                    ? reader.GetInt32("stock")
                                    : 0
                            });
                        }
                    }
                }

                connection.Close();
            }

            return Page();
        }

        // POST method to add product to the cart
        public IActionResult OnPostAddToCart(string ProductId, string ProductName, int Quantity)
        {
            // Assume user is logged in, and you have access to Member_ID in session or via authentication
            string memberId = HttpContext.Session.GetString("Member_ID");

            if (string.IsNullOrEmpty(memberId))
            {
                // If Member_ID is not found, redirect to login page (or handle as necessary)
                return RedirectToPage("/Login");
            }

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                // Check if user already has a cart
                string cartQuery = "SELECT Cart_ID FROM CART WHERE Member_ID = @MemberId;";
                string cartId = string.Empty;
                using (var command = new MySqlCommand(cartQuery, connection))
                {
                    command.Parameters.AddWithValue("@MemberId", memberId);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            cartId = reader.GetString("Cart_ID");
                        }
                    }
                }

                if (string.IsNullOrEmpty(cartId))
                {
                    // Create a new cart if one doesn't exist
                    string insertCartQuery = "INSERT INTO CART (Member_ID) VALUES (@MemberId);";
                    using (var command = new MySqlCommand(insertCartQuery, connection))
                    {
                        command.Parameters.AddWithValue("@MemberId", memberId);
                        command.ExecuteNonQuery();
                    }

                    // Retrieve the new cart ID
                    cartId = "new_cart_id"; // Get the new Cart_ID (or select it again)
                }

                // Add product to the cart
                string insertCartItemQuery = "INSERT INTO IN_CART (Cart_ID, Product_ID, Quantity) VALUES (@CartId, @ProductId, @Quantity);";
                using (var command = new MySqlCommand(insertCartItemQuery, connection))
                {
                    command.Parameters.AddWithValue("@CartId", cartId);
                    command.Parameters.AddWithValue("@ProductId", ProductId);
                    command.Parameters.AddWithValue("@Quantity", Quantity);
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }

            // Redirect back to the store details page
            return RedirectToPage("/StoreDetails", new { shopId = ProductId });
        }
    }
}
