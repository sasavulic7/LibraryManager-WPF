using Library_Manager.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;



namespace Library_Manager.Data
{
    internal class BookRepository
    {
        private readonly string connectionString = 
            ConfigurationManager.ConnectionStrings["LibraryDB"].ConnectionString;

        public List<Book> GetAllBooks() 
        {
            List<Book> list = new List<Book>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            { 
            string query = "SELECT * FROM Books";

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Book
                        {
                            BookId = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            Author = reader.GetString(2),
                            Genre = reader.GetString(3),
                            Quantity = reader.GetInt32(4)
                        });
                    }
                    return list;
                }
            }
        }

        public void AddBook(Book book)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Books (Title, Author, Genre, Quantity) VALUES (@Title, @Author, @Genre, @Quantity)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Title", book.Title);
                cmd.Parameters.AddWithValue("@Author", book.Author);
                cmd.Parameters.AddWithValue("@Genre", book.Genre);
                cmd.Parameters.AddWithValue("@Quantity", book.Quantity);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteBook(int bookId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Books WHERE BookId = @BookId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@BookId", bookId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

            public void UpdateBook(Book book)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Books SET Title = @Title, Author = @Author, Genre = @Genre, Quantity = @Quantity WHERE BookId = @BookId";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Title", book.Title);
                    cmd.Parameters.AddWithValue("@Author", book.Author);
                    cmd.Parameters.AddWithValue("@Genre", book.Genre);
                    cmd.Parameters.AddWithValue("@Quantity", book.Quantity);
                    cmd.Parameters.AddWithValue("@BookId", book.BookId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
        }


    }
}
