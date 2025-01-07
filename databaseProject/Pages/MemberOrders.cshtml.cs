using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

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

                string query = @"
                    SELECT ORDERS.Order_ID, ORDERS.order_time, ORDERS.order_states, SUM(PRODUCT.price * PRODUCT.discount * IN_ORDER.amount) AS TotalAmount, BUSINESS.Business_ID
                    FROM ORDERS
                    JOIN IN_ORDER ON ORDERS.Order_ID = IN_ORDER.Order_ID
                    JOIN PRODUCT ON IN_ORDER.Product_ID = PRODUCT.Product_ID
                    JOIN BUSINESS ON ORDERS.Business_ID = BUSINESS.Business_ID
                    WHERE ORDERS.Member_ID = @memberId
                    GROUP BY ORDERS.Order_ID, ORDERS.order_time, ORDERS.order_states, BUSINESS.Business_ID";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@memberId", memberId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Orders.Add(new Order
                            {
                                OrderId = reader.GetString("Order_ID"),
                                OrderTime = reader.GetTimeSpan("order_time"),
                                OrderStates = reader.GetString("order_states"),
                                TotalAmount = reader.GetDecimal("TotalAmount"),
                                BusinessId = reader.GetString("Business_ID")
                            });
                        }
                    }
                }

                connection.Close();
            }
        }

        public class Order
        {
            public string OrderId { get; set; }
            public TimeSpan OrderTime { get; set; }
            public string OrderStates { get; set; }
            public decimal TotalAmount { get; set; }
            public string BusinessId { get; set; }
        }
    }
}