using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BestShop.Pages.Auth
{
    [BindProperties]
    public class RegisterModel : PageModel
    {
        [Required(ErrorMessage ="The First Name is required")]
        public string Firstname { get; set; } = "";
        [Required(ErrorMessage = "The Last Name is required")]
        public string Lastname { get; set; } = "";
        [Required(ErrorMessage ="The Email is required"), EmailAddress]
        public string Email { get; set; } = "";
        public string? Phone { get; set; } = "";
        [Required(ErrorMessage ="The Address is required")]
        public string Address { get; set; } = "";
        [Required(ErrorMessage ="Password is required")]
        [StringLength(50, ErrorMessage ="Password must be between 5 and 50 character ", MinimumLength =5)]
        public string Password { get; set; } = "";

        [Required(ErrorMessage = "Confirm passwork is required")]
        [Compare("Password", ErrorMessage = "Password and Confirm Password do not match")]
        public string ConfirmPassword { get; set; } = "";
        public string errorMessage = "";
        public string successMessage = "";
      
        public void OnGet()
        {
        }
        public void OnPost()
        {
            if (!ModelState.IsValid)
            {
                errorMessage = "Data validation failed";
                return;
            }
            // successfull data validation 
            if (Phone == null) Phone = "";
            //add the user details to the database 

            //send  confirmation email to the user 

            //initialize the authenticated session => add the user details to the session data 

            successMessage = " Account created  successfully ";
        }
    }
}
