using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace databaseProject.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public IndexModel(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        // Property to hold the categories displayed on the home page
        public List<Category> Categories { get; set; }

        public void OnGet()
        {
            // Initialize the categories for display
            Categories = new List<Category>
            {
                new Category
                {
                    Name = "Fruits",
                    ImagePath = "/images/fruits.jpg"
                },
                new Category
                {
                    Name = "Vegetables",
                    ImagePath = "/images/vegetables.jpg"
                },
                new Category
                {
                    Name = "Snacks",
                    ImagePath = "/images/snacks.jpg"
                }
            };
        }
    }

    // Class to represent a category
    public class Category
    {
        public string Name { get; set; }        // Category name (e.g., "Fruits")
        public string ImagePath { get; set; }  // Path to the category image
    }
}
