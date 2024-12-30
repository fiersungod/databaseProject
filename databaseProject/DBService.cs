using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;

namespace databaseProject
{
    public class UserService
    {
        private readonly string _connectionString;

        public UserService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // 讀取 SQL 檔案並執行
        public void ExecuteSqlFile()
        {
            string sql = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "db_script", "test.sql");
            string initConnect = "Server=localhost;User ID=root;Password=rebecca86;";
            try
            {
                // 讀取 SQL 檔案內容
                string sqlContent = File.ReadAllText(sql);

                // 分割 SQL 語句 (假設每個語句以分號 ";" 結尾)
                string[] sqlCommands = sqlContent.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                using (MySqlConnection connection = new MySqlConnection(initConnect))
                {
                    connection.Open();

                    foreach (var commandText in sqlCommands)
                    {
                        string trimmedCommand = commandText.Trim();
                        if (!string.IsNullOrEmpty(trimmedCommand))
                        {
                            using (MySqlCommand command = new MySqlCommand(trimmedCommand, connection))
                            {
                                command.ExecuteNonQuery();
                                Console.WriteLine($"執行成功: {trimmedCommand}");
                            }
                        }
                    }

                    connection.Close();
                }

                Console.WriteLine("所有語句執行完成！");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"發生錯誤: {ex.Message}");
            }
        }

        // 從資料庫提取使用者資料
        /*public List<User> GetUsers()
        {
            var users = new List<User>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT Id, FullName, Email FROM Users";
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            users.Add(new User
                            {
                                Id = reader.GetInt32("Id"),
                                Name = reader.GetString("FullName"),
                                Email = reader.GetString("Email")
                            });
                        }
                    }
                }
                connection.Close();
            }

            return users;
        }*/

        // 新增使用者到資料庫

        /*public void AddUser(User user)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string query = "INSERT INTO Users (FullName, Email) VALUES (@Name, @Email)";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", user.Name);
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }*/
    }
}
