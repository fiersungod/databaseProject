using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace databaseProject.Pages
{
    public class Change
    {
        public string Order_ID { get; set; }
        public string value { get; set; }
    }
    public class BusinessOrdersModel : PageModel
    {
        private readonly string _connectionString;

        public BusinessOrdersModel(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Order> Orders { get; set; } = new List<Order>();

        public void OnGet(string memberId)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string q = @"
                SELECT Business_ID
                FROM BUSINESS
                WHERE Member_ID = @Member_ID ";
                var businessId = "";
                using (var command = new MySqlCommand(q, connection))
                {
                    command.Parameters.AddWithValue("Member_ID", memberId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            businessId = reader.GetString("Business_ID");
                        }
                    }
                }
                string query = @"
                SELECT Order_ID,Member_ID,phone_Number,order_time,order_states
                FROM Order_Who
                WHERE Business_ID = @business_id ";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("business_id", businessId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Orders.Add(new Order
                            {
                                OrderId = reader.GetString("Order_ID"),
                                MemberId = reader.GetString("Member_ID"),
                                PhoneNumber = reader.GetString("phone_Number"),
                                OrderTime = reader.GetTimeSpan("order_time"),
                                OrderStates = reader.GetString("order_states"),
                                TotalPrice = 0,
                                InOrders = new List<InOrder>()
                            });
                        }
                    }
                }

                string query_2 = @"
                SELECT product_name, amount, price
                FROM Order_Product
                WHERE Order_ID = @order_id";
                foreach (var order in Orders)
                {
                    using (var command = new MySqlCommand(query_2, connection))
                    {
                        command.Parameters.AddWithValue("order_id", order.OrderId);
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                order.InOrders.Add(new InOrder
                                {
                                    ProductName = reader.GetString("product_name"),
                                    Amount = reader.GetInt32("amount"),
                                    Price = reader.GetInt32("price")
                                });
                            }
                        }
                    }
                }
                connection.Close();
                foreach (var order in Orders)
                {
                    foreach (var inOrder in order.InOrders)
                    {
                        order.TotalPrice += inOrder.Price;
                    }
                }
            }
        }
        public IActionResult OnPostChange([FromBody] Change change)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
            connection.Open();
            string q = @"
                UPDATE ORDERS
                SET order_states = @Change
                WHERE Order_ID = @order_id";
            using (var command = new MySqlCommand(q, connection))
            {
                command.Parameters.AddWithValue("Change", change.value); 
                command.Parameters.AddWithValue("order_id", change.Order_ID);
                command.ExecuteScalar();
            }
            connection.Close();
            }
            return new JsonResult(new { success = true });
        }
        public class InOrder
        {
            public string ProductName { get; set; }
            public decimal Amount { get; set; }
            public int Price { get; set; }
        }

        public class Order
        {
            public string OrderId { get; set; }
            public string MemberId { get; set; }
            public string PhoneNumber { get; set; }
            public TimeSpan OrderTime { get; set; }
            public string OrderStates { get; set; }
            public int TotalPrice { get; set; }
            public List<InOrder> InOrders { get; set; }
        }
    }
}