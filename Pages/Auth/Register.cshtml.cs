using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

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
			string connectionString = "Data Source=DESKTOP-3CSJ4C0\\MSSQL;Initial Catalog=bestshop;Integrated Security=True";
            try
            {
                using (SqlConnection  connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO users"+
                        "(firstname, lastname, email, phone, address, password, role) VALUES"+
                        "(@firstname, @lastname, @email, @phone, @address, @password, 'client');";

                    var passwordHasher = new PasswordHasher<IdentityUser>();
                    string hashedPassword = passwordHasher.HashPassword(new IdentityUser(), Password);

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@firstname", Firstname);
                        command.Parameters.AddWithValue("@lastname", Lastname);
                        command.Parameters.AddWithValue("@email", Email);
                        command.Parameters.AddWithValue("@phone", Phone);
                        command.Parameters.AddWithValue("@address", Address);
                        command.Parameters.AddWithValue("@password", hashedPassword);

                        command.ExecuteNonQuery();
                    };
                }
            }catch (Exception ex)
            {
                if (ex.Message.Contains(Email)) {
                    errorMessage = "Email address already used";
                }else
                {
					errorMessage = ex.Message;
				}
				return; 
            }
            //send  confirmation email to the user 

			//initialize the authenticated session => add the user details to the session data 

			successMessage = " Account created  successfully ";
        }
    }
}
