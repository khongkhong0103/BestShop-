using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace BestShop.Pages.Admin.Books
{
    public class IndexModel : PageModel
    {
        public List<BookInfo> listBooks = new List<BookInfo>();
        public string search = "";

        public int page = 1; // the current html pages
        public int totalPages = 0;
        private readonly int pageSize = 5; // books per page 
        
        public void OnGet()
        {
            search  = Request.Query["search"];
            if (search == null) search = "";

            page = 1; 
            string requestPage = Request.Query["page"];
            if (requestPage != null)
                {
                try
                {
                    page = int.Parse(requestPage);
                }catch (Exception ex)
                {
                    page = 1;
                    Console.WriteLine(ex.Message);
                }
                }
            try
            {
				string connectionString = "Data Source=DESKTOP-3CSJ4C0\\MSSQL;Initial Catalog=bestshop;Integrated Security=True";
			    using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sqlCount = "SELECT COUNT(*) FROM books";
                    if(search.Length > 0)
                    {
                        sqlCount += " WHERE title LIKE @search OR authors LIKE @search";
                    }
                    using(SqlCommand command = new SqlCommand(sqlCount, connection))
                    {
                        command.Parameters.AddWithValue("@search","%" +search+ "%");

                        decimal count = (int)command.ExecuteScalar();
                        totalPages = (int)Math.Ceiling(count/pageSize);
                    }
                    string sql = "SELECT * FROM books";
                    if(search.Length > 0)
                    {
                        sql += " WHERE title LIKE @search OR authors LIKE @search";
                    }
                    sql += " ORDER BY id DESC";
                    sql += " OFFSET @skip ROWS FETCH NEXT @pageSize ROWS ONLY";

					using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@search", "%" + search + "%");
                        command.Parameters.AddWithValue("@skip", (page - 1) * pageSize);
                        command.Parameters.AddWithValue("@pageSize", pageSize);

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
