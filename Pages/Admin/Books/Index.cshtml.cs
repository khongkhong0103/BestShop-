using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace BestShop.Pages.Admin.Books
{
    public class IndexModel : PageModel
    {
        public List<BookInfo> listBooks = new List<BookInfo>();

        public void OnGet()
        {
            try
            {
				string connectionString = "Data Source=DESKTOP-3CSJ4C0\\MSSQL;Initial Catalog=bestshop;Integrated Security=True";
			    using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM books ORDER BY id DESC";

                    using(SqlCommand command = new SqlCommand(sql, connection))
                    {
                       using(SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                BookInfo bookInfo = new BookInfo();
                                bookInfo.Id = reader.GetInt32(0);
                                bookInfo.Title = reader.GetString(1);
                                bookInfo.Authors = reader.GetString(2);
                                bookInfo.Isbn= reader.GetString(3);
                                bookInfo.NumPage = reader.GetInt32(4);
                                bookInfo.Price = reader.GetDecimal(5);
                                bookInfo.Category= reader.GetString(6);
                                bookInfo.Description = reader.GetString(7);
                                bookInfo.ImageFileName = reader.GetString(8);
                                bookInfo.CreatedAt = reader.GetDateTime(9).ToString("MM/dd/yyyy");

                                listBooks.Add(bookInfo);
                            }
                        }
                    }
                }
            }
			catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
    public class BookInfo
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Authors { get; set; } = "";
        public string Isbn { get; set; } = "";
        public int NumPage { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; } = "";
        public string Description { get; set; } = "";
        public string ImageFileName { get; set; } = "";
        public string CreatedAt { get; set; } = "";
        
    }
}
