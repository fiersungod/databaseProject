using Microsoft.AspNetCore.Mvc.RazorPages;

namespace databaseProject.Pages
{
    public class ExploreModel : PageModel
    {
        // Define a property to hold the list of shops (even if it's empty for now)
        public List<Shop> Shops { get; set; }

        public void OnGet()
        {
            // Initialize the Shops list, or leave it empty for now
            Shops = new List<Shop>();
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

