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
        private List<Customer> customerList = new List<Customer>();
        private string filePath;

        //call on startup
        public ClientController()
        {
            string appDataDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "VillageRentals"); //get the AppData folder to write the file into
            Directory.CreateDirectory(appDataDirectory);    //create the AppDataDirectory to ensure it exists

            filePath = Path.Combine(appDataDirectory, "clients.json"); //assigns 'filePath' as path to 'clients.json"

            LoadClientsFromFile();
        }

        //serialize client data and also deserialize data to be saved in a file to be used.
        private void SaveClientsToFile()
        {
            //serializes to customerList. Then writes down the pata to the clients.json file.
            var json = JsonSerializer.Serialize(customerList);
            File.WriteAllText(filePath, json);
        }
        //deserialize data and put it into 'customerList'
        private void LoadClientsFromFile()
        {
            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                customerList = JsonSerializer.Deserialize<List<Customer>>(json);
            }
        }


        public void AddClient(string firstName, string lastName, string phone, string email, string note = "")
        {
            var customer = new Customer(GenerateNewId(), firstName, lastName, phone, email, note);
            customerList.Add(customer);

            //after AddClient is called, we run SaveClientsToFile() to serialize the data into the clients.json
            SaveClientsToFile();
        }

        public List<Customer> GetAllClients()
        {
            return customerList;
        }

        private int GenerateNewId()
        {
            return customerList.Count > 0 ? customerList.Max(c => c.Id) + 1 : 1;
        }

    }
}
