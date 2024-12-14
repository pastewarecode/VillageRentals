using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VillageRentals.Components.Controller;
using MySql.Data.MySqlClient;

namespace VillageRentals.Components.Controller
{
    public class Database
    {
        //variables
        private MySqlConnection connection;
        private string server = "localhost";
        private string database = "VillageRentals";
        private string uid = "root";
        private string password = "";

        //Constructor
        public Database()
        {
            InitializeConnection();
        }

        //Initialize the database connection
        private void InitializeConnection()
        {
            string connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";
            connection = new MySqlConnection(connectionString);
        }

        //method to load data into the database
        public void LoadData()
        {
            try
            {
                connection.Open();

                //Insert data
                InsertCategory(10, "Power tools");
                InsertCategory(20, "Yard equipment");
                InsertCategory(30, "Compressors");
                InsertCategory(40, "Generators");
                InsertCategory(50, "Air Tools");

                InsertEquipment(101, 10, "Hammer drill", "Powerful drill for concrete and masonry", 25.99m);
                InsertEquipment(201, 20, "Chainsaw", "Gas-powered chainsaw for cutting wood", 49.99m);
                InsertEquipment(202, 20, "Lawn mower", "Self-propelled lawn mower with mulching function", 19.99m);
                InsertEquipment(301, 30, "Small Compressor", "5 Gallon Compressor-Portable", 14.99m);
                InsertEquipment(501, 50, "Brad Nailer", "Brad Nailer. Requires 3/4 to 1 1/2 Brad Nails", 10.99m);

                InsertCustomer(1001, "Doe", "John", "(555) 555-1212", "jd@sample.net");
                InsertCustomer(1002, "Smith", "Jane", "(555) 555-3434", "js@live.com");
                InsertCustomer(1003, "Lee", "Michael", "(555) 555-5656", "ml@sample.net");

                InsertRental(1000, "2024-02-15", 1001, 201, "2024-02-20", "2024-02-23", 149.97m);
                InsertRental(1001, "2024-02-16", 1002, 501, "2024-02-21", "2024-02-25", 43.96m);

                Console.WriteLine("Data loaded successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
        }

        private void InsertCategory(int categoryId, string name)
        {
            string query = "INSERT IGNORE INTO Categories (CategoryId, Name) VALUES (@CategoryId, @Name)";
            ExecuteQuery(query, cmd =>
            {
                cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                cmd.Parameters.AddWithValue("@Name", name);
            });
        }

        private void InsertEquipment(int equipmentId, int categoryId, string name, string description, decimal dailyRate)
        {
            string query = @"INSERT IGNORE INTO Equipment 
                             (EquipmentId, CategoryId, Name, Description, DailyRate) 
                             VALUES (@EquipmentId, @CategoryId, @Name, @Description, @DailyRate)";
            ExecuteQuery(query, cmd =>
            {
                cmd.Parameters.AddWithValue("@EquipmentId", equipmentId);
                cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Description", description);
                cmd.Parameters.AddWithValue("@DailyRate", dailyRate);
            });
        }

        private void InsertCustomer(int customerId, string lastName, string firstName, string contactPhone, string email)
        {
            string query = @"INSERT IGNORE INTO Customers 
                             (CustomerId, LastName, FirstName, ContactPhone, Email) 
                             VALUES (@CustomerId, @LastName, @FirstName, @ContactPhone, @Email)";
            ExecuteQuery(query, cmd =>
            {
                cmd.Parameters.AddWithValue("@CustomerId", customerId);
                cmd.Parameters.AddWithValue("@LastName", lastName);
                cmd.Parameters.AddWithValue("@FirstName", firstName);
                cmd.Parameters.AddWithValue("@ContactPhone", contactPhone);
                cmd.Parameters.AddWithValue("@Email", email);
            });
        }

        private void InsertRental(int rentalId, string date, int customerId, int equipmentId, string rentalDate, string returnDate, decimal cost)
        {
            string query = @"INSERT IGNORE INTO Rentals 
                             (RentalId, Date, CustomerId, EquipmentId, RentalDate, ReturnDate, Cost) 
                             VALUES (@RentalId, @Date, @CustomerId, @EquipmentId, @RentalDate, @ReturnDate, @Cost)";
            ExecuteQuery(query, cmd =>
            {
                cmd.Parameters.AddWithValue("@RentalId", rentalId);
                cmd.Parameters.AddWithValue("@Date", DateTime.Parse(date));
                cmd.Parameters.AddWithValue("@CustomerId", customerId);
                cmd.Parameters.AddWithValue("@EquipmentId", equipmentId);
                cmd.Parameters.AddWithValue("@RentalDate", DateTime.Parse(rentalDate));
                cmd.Parameters.AddWithValue("@ReturnDate", DateTime.Parse(returnDate));
                cmd.Parameters.AddWithValue("@Cost", cost);
            });
        }

        private void ExecuteQuery(string query, Action<MySqlCommand> parameterize)
        {
            try
            {
                using (var cmd = new MySqlCommand(query, connection))
                {
                    parameterize(cmd);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing query: {ex.Message}");
            }
        }
    }
}
