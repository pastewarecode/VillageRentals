using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using VillageRentals.Components.Models;


namespace VillageRentals.Components.Controller
{
    public class ClientController
    {
            private readonly string connectionString;

            public ClientController(string connectionString)
            {
                this.connectionString = connectionString;
            }

            // Fetch clients from the database
            public List<Customer> GetAllClients()
            {
                var clients = new List<Customer>();
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Customers";
                    using (var cmd = new MySqlCommand(query, connection))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var client = new Customer
                                {
                                    Id = reader.GetInt32("CustomerId"),
                                    FirstName = reader.GetString("FirstName"),
                                    LastName = reader.GetString("LastName"),
                                    Phone = reader.GetString("ContactPhone"),
                                    Email = reader.GetString("Email"),
                                    Note = reader.IsDBNull(reader.GetOrdinal("Note")) ? "" : reader.GetString("Note")
                                };
                                clients.Add(client);
                            }
                        }
                    }
                }
                return clients;
            }

            // Add a new client to the database
            public void AddClient(string firstName, string lastName, string phone, string email, string note = "")
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Customers (FirstName, LastName, ContactPhone, Email, Note) " +
                                   "VALUES (@FirstName, @LastName, @ContactPhone, @Email, @Note)";
                    using (var cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@FirstName", firstName);
                        cmd.Parameters.AddWithValue("@LastName", lastName);
                        cmd.Parameters.AddWithValue("@ContactPhone", phone);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Note", note);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
    }
