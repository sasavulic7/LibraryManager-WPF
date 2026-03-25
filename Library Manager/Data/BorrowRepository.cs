using Library_Manager.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Manager.Data
{
    internal class BorrowRepository
    {
        private readonly string connectionString =
           ConfigurationManager.ConnectionStrings["LibraryDB"].ConnectionString;

        public void AddBorrowing(Borrowing borrowing)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO Borrowings (UserId, BookId, BorrowDate)
                                 VALUES (@UserId, @BookId, @BorrowDate)";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@UserId", borrowing.UserId);
                cmd.Parameters.AddWithValue("@BookId", borrowing.BookId);
                cmd.Parameters.AddWithValue("@BorrowDate", borrowing.BorrowDate);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Borrowing> GetBorrowingsByUser(int userId)
        {
            List<Borrowing> list = new List<Borrowing>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT * FROM Borrowings 
                                 WHERE UserId = @UserId AND ReturnDate IS NULL";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserId", userId);

                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DateTime? returnDate = null;
                        if (!reader.IsDBNull(4))
                        {
                            returnDate = reader.GetDateTime(4);
                        }
                        list.Add(new Borrowing
                        {
                            Id = reader.GetInt32(0),
                            UserId = reader.GetInt32(1),
                            BookId = reader.GetInt32(2),
                            BorrowDate = reader.GetDateTime(3),
                            ReturnDate = returnDate  
                        });
                    }
                }
            }

            return list;
        }
    }
}
