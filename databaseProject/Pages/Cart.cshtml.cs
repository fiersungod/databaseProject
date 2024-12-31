using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace databaseProject.Pages
{
    public class CartModel : PageModel
    {
        // A list to hold the cart items
        public List<CartItem> CartItems { get; set; }

        // Total price of the cart
        public decimal CartTotal { get; set; }

        public void OnGet()
        {
            // Initialize the cart items (this would be dynamic in a real scenario)
            CartItems = new List<CartItem>
            {
                new CartItem { Shop = new Shop { Shop_ID = "001", Describition = "Onion Pancake Shop"}, Quantity = 2 },
                new CartItem { Shop = new Shop { Shop_ID = "002", Describition = "Xuan Fang"}, Quantity = 1 }
            };

            // Calculate the total price of the cart
            CalculateCartTotal();
        }

        // Calculate the total price of the cart based on quantities and prices
        private void CalculateCartTotal()
        {
            CartTotal = 0;
            foreach (var item in CartItems)
            {
                CartTotal += item.Quantity;
            }
        }
    }

    // CartItem class to represent an item in the cart
    public class CartItem
    {
        public Shop Shop { get; set; }
        public int Quantity { get; set; }
    }
}
