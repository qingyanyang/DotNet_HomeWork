using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using NET_5_Assignment.IService;
using NET_5_Assignment.Models;
using NET_5_Assignment.Config;
using NET_5_Assignment.Common.Enums;

namespace NET_5_Assignment.Service
{
    public class UserService : IUserService
    {
        private readonly string _connectionString;

        public UserService(IOptions<DbConnectConfig> opt)
        {
            _connectionString = opt.Value.ConnectionString;
        }
        public bool Insert(UserCreateInput user)
        {
            try {
                using (MySqlConnection mySqlConnection = new MySqlConnection(_connectionString))
                {
                    mySqlConnection.Open();
                    string insertSql = "INSERT INTO user(username,password,email,age,gender,active,address) VALUES(@username,@password,@email,@age,@gender,@active,@address)";
                    using (MySqlCommand mySqlCommand = mySqlConnection.CreateCommand())
                    {
                        mySqlCommand.CommandText = insertSql;
                        mySqlCommand.Parameters.AddWithValue("@username", user.UserName);
                        mySqlCommand.Parameters.AddWithValue("@password", user.Password);
                        mySqlCommand.Parameters.AddWithValue("@email", user.Email);
                        mySqlCommand.Parameters.AddWithValue("@age", user.Age);
                        mySqlCommand.Parameters.AddWithValue("@gender", user.Gender);
                        mySqlCommand.Parameters.AddWithValue("@active", user.Active);
                        mySqlCommand.Parameters.AddWithValue("@address", user.Address);
                        
                        var count = mySqlCommand.ExecuteNonQuery();
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }
        public bool Update(UserUpdateInput user, int id)
        {
            try {
                using (MySqlConnection mySqlConnection = new MySqlConnection(_connectionString))
                {
                    mySqlConnection.Open();
                    string updateString = "UPDATE user SET username=@username,age=@age,gender=@gender,address=@address) WHERE id = @id";
                    using (MySqlCommand mySqlCommand = mySqlConnection.CreateCommand())
                    {
                        mySqlCommand.CommandText = updateString;
                        mySqlCommand.Parameters.AddWithValue("@username", user.UserName);                 
   
                        mySqlCommand.Parameters.AddWithValue("@age", user.Age);
                        mySqlCommand.Parameters.AddWithValue("@gender", user.Gender);
                        mySqlCommand.Parameters.AddWithValue("@address", user.Address);
                        mySqlCommand.Parameters.AddWithValue("@id", id);
                        
                        int count = mySqlCommand.ExecuteNonQuery();
                        return count > 0;
                    }
                }
            }
            catch (Exception ex) {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }
        public UserResponse Search(int id)
        {
            try {
                using (MySqlConnection mySqlConnection = new MySqlConnection(_connectionString))
                {
                    mySqlConnection.Open();

                    string searchString = "SELECT * FROM user WHERE id=@id";
                    using (MySqlCommand mySqlCommand = new MySqlCommand())
                    {
                        mySqlCommand.CommandText = searchString;
                        mySqlCommand.Parameters.AddWithValue("@id",id);
                        using (MySqlDataReader reader = mySqlCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                UserResponse user = new UserResponse
                                {
                                    Id = reader.GetGuid("id"),
                                    UserName = reader.GetString("username"),
                                    Email = reader.GetString("email"),
                                    Address = reader.GetString("address"),
                                    Age = reader.GetInt32("age"),
                                    Gender = (GenderEnum)reader.GetInt16("gender"),
                                    Active = reader.GetBoolean("active")
                                };
                                return user;
                            }
                            reader.Close();    
                        }
                    }
                }
            }
            catch (Exception ex) {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            return null;
        }
        public bool Delete(int id)
        {
            try {
                using (MySqlConnection mySqlConnection = new MySqlConnection(_connectionString))
                {
                    mySqlConnection.Open();
                    string deleteString = "DELETE FROM user WHERE id=@id";
                    using (MySqlCommand mySqlCommand = new MySqlCommand())
                    {
                        mySqlCommand.CommandText= deleteString;
                        mySqlCommand.Parameters.AddWithValue("id",id);
                        int count = mySqlCommand.ExecuteNonQuery();
                        return count > 0;
                    }
                }
            } 
            catch (Exception ex) { 
                Console.WriteLine($"An error occurred: {ex.Message}"); 
                return false;
            }
        }
    }
}
