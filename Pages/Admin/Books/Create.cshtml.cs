using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BestShop.Pages.Admin.Books
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "The Title is required")]
        [MaxLength(100, ErrorMessage ="The Title cannot exceed 100 characters")]
        public string Title { get; set; } = "";

        [BindProperty]
		[Required(ErrorMessage = "The Authors is required")]
		[MaxLength(255, ErrorMessage = "The Authors cannot exceed 255 characters")]
		public string Authors { get; set; } = "";

        [BindProperty]
		[Required(ErrorMessage = "The ISBN is required")]
		[MaxLength(20, ErrorMessage = "The Title cannot exceed 20 characters")]
		public string ISBN { get; set; } = "";

        [BindProperty]
		[Required(ErrorMessage = "The Num of page is required")]
        [Range(1, 1000, ErrorMessage ="The Number of page must be in the range from 1 to 1000")]
		public int NumPages {  get; set; }

        [BindProperty]
		[Required(ErrorMessage = "The Price is required")]
		public decimal Price { get; set; }

        [BindProperty, Required]
        public string Category { get; set; } = "";

        [BindProperty]
        [MaxLength(1000, ErrorMessage ="The Description cannot exceed 1000 characters")]
        public string? Description { get; set; } = "";

        [BindProperty]
		[Required(ErrorMessage = "The Image File is required")]
		public IFormFile ImageFile { get; set; }

        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }
        public void OnPost()
        {
            if(!ModelState.IsValid) {
                errorMessage = "Data validation failed";
                return; }

            //successfull data validation
            if (Description == null) Description = "";

            // save the image file on the server

            // save the new book in tha database

            successMessage = "Data saved correctly";
        }
    }
}
