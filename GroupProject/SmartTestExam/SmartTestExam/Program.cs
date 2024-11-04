using System;
using Npgsql;

namespace SmartTestExam
{
    public class Data
    {
        private string connectionString;

        public Data(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void DisplayUsers()
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT * FROM users";

                using (var command = new NpgsqlCommand(sql, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader["id"]}, First Name: {reader["first_name"]}, Last Name: {reader["last_name"]}, Email: {reader["email"]}, Role: {reader["role"]}");
                    }
                }
            }
        }

        public void DisplayTests()
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT * FROM tests";

                using (var command = new NpgsqlCommand(sql, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader["id"]}, Title: {reader["title"]}, Code: {reader["code"]}, Author ID: {reader["author_id"]}, Created At: {reader["created_at"]}");
                    }
                }
            }
        }

        public void DisplayQuestions()
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT * FROM questions";

                using (var command = new NpgsqlCommand(sql, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader["id"]}, Test ID: {reader["test_id"]}, Question Text: {reader["question_text"]}, Question Type: {reader["question_type"]}");
                    }
                }
            }
        }

        public void DisplayAnswers()
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT * FROM answers";

                using (var command = new NpgsqlCommand(sql, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader["id"]}, Question ID: {reader["question_id"]}, Answer Text: {reader["answer_text"]}, Is Correct: {reader["is_correct"]}");
                    }
                }
            }
        }
        public void DeleteUser(int userId)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string sql = "DELETE FROM users WHERE id = @UserId";

                using (var command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("UserId", userId);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine($"Користувача з ID {userId} успішно видалено.");
                    }
                    else
                    {
                        Console.WriteLine($"Користувача з ID {userId} не знайдено.");
                    }
                }
            }
        }

        public void DisplayTestResults()
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT * FROM test_results";

                using (var command = new NpgsqlCommand(sql, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader["id"]}, Test ID: {reader["test_id"]}, User ID: {reader["user_id"]}, Score: {reader["score"]}, Completed At: {reader["completed_at"]}");
                    }
                }
            }
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password='12345';Database=SmartTestExam";

            var dbHelper = new Data(connectionString);

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\nОберіть дію:");
                Console.WriteLine("1. Вивести Users");
                Console.WriteLine("2. Вивести Tests");
                Console.WriteLine("3. Вивести Questions");
                Console.WriteLine("4. Вивести Answers");
                Console.WriteLine("5. Вивести Test Results");
                Console.WriteLine("6. Видалити користувача за ID");
                Console.WriteLine("0. Вийти");

                Console.Write("Ваш вибір: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Users in the database:");
                        dbHelper.DisplayUsers();
                        break;
                    case "2":
                        Console.WriteLine("Tests in the database:");
                        dbHelper.DisplayTests();
                        break;
                    case "3":
                        Console.WriteLine("Questions in the database:");
                        dbHelper.DisplayQuestions();
                        break;
                    case "4":
                        Console.WriteLine("Answers in the database:");
                        dbHelper.DisplayAnswers();
                        break;
                    case "5":
                        Console.WriteLine("Test Results in the database:");
                        dbHelper.DisplayTestResults();
                        break;
                    case "6":
                        Console.Write("Введіть ID користувача для видалення: ");
                        if (int.TryParse(Console.ReadLine(), out int userId))
                        {
                            dbHelper.DeleteUser(userId);
                        }
                        else
                        {
                            Console.WriteLine("Невірний формат ID.");
                        }
                        break;
                    case "0":
                        exit = true;
                        Console.WriteLine("Вихід з програми.");
                        break;
                    default:
                        Console.WriteLine("Невірний вибір, спробуйте ще раз.");
                        break;
                }
            }
        }
    }
}
