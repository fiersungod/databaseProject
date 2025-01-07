using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using static databaseProject.Pages.MemberOrdersModel;

namespace databaseProject.Pages
{
    public class MemberOrdersModel : PageModel
    {
        private readonly string _connectionString;

        public MemberOrdersModel(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Order> Orders { get; set; } = new List<Order>();

        public void OnGet(string memberId)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                // 抓出所有order
                string query = @"
                SELECT Order_ID,order_time,order_states
                FROM ORDERS
                WHERE Member_ID = @member_id";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("member_id", memberId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Orders.Add(new Order
                            {
                                OrderId = reader.GetString("Order_ID"),
                                OrderTime = reader.GetTimeSpan("order_time"),
                                OrderStates = reader.GetString("order_states"),
                                InOrders = new List<InOrder>()
                            });
                        }
                    }
                }
                // 抓出order資訊
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
            }
        }
        public class InOrder
        {
            public string ProductName { get; set; }
            public decimal Amount { get; set; }
            public decimal Price { get; set; }
        }

        public class Order
        {
            public string OrderId { get; set; }
            public TimeSpan OrderTime { get; set; }
            public string OrderStates { get; set; }
            public List<InOrder> InOrders { get; set; }
        }

    }
}