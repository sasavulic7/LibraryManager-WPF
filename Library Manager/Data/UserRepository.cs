using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using System.Data.SqlClient;
using Library_Manager.Models;

namespace Library_Manager.Data
{
    internal class UserRepository
    {
        private readonly string connectionString =
            ConfigurationManager.ConnectionStrings["LibraryDB"].ConnectionString;
   
        public  List<User> GetAllUsers()
        {
             List<User> list = new List<User>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Users";

                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new User
                        {
                            UserId = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            IdNumber = reader.GetString(2),
                            IsAdmin = reader.GetBoolean(3),
                            Password = reader.GetString(4)
                        });
                    }
                    return list;
                }
            }
        }

        public User GetUser(string name, string password)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT * FROM Users 
                         WHERE Name = @Name AND Password = @Password";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Password", password);

                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new User
                        {
                            UserId = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            IdNumber = reader.GetString(2),
                            IsAdmin = reader.GetBoolean(3),
                            Password = reader.GetString(4)
                        };
                    }
                }
            }

            return null;
        }

        public void AddUser(User user)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO Users
                                (Name, IDNumber, IsAdmin, Password)
                                VALUES
                                (@Name, @IdNumber, @IsAdmin, @Password)";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@Name", user.Name);
                cmd.Parameters.AddWithValue("@IdNumber", user.IdNumber);
                cmd.Parameters.AddWithValue("@IsAdmin", user.IsAdmin);
                cmd.Parameters.AddWithValue("@Password", user.Password);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteUser(int Id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"DELETE FROM Users WHERE UserId=@Id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", Id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateUser(User user)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                {
                    string query = @"UPDATE Users 
                                   SET Name = @Name,
                                   IDNumber = @IdNumber,
                                   IsAdmin = @IsAdmin,
                                   Password = @Password
                                   WHERE UserId = @UserId";

                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@UserId", user.UserId);
                    cmd.Parameters.AddWithValue("@Name", user.Name);
                    cmd.Parameters.AddWithValue("@IDNumber", user.IdNumber);
                    cmd.Parameters.AddWithValue("@IsAdmin", user.IsAdmin);
                    cmd.Parameters.AddWithValue("@Password", user.Password);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
