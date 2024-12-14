using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using VillageRentals.Components.Models;

namespace VillageRentals.Components.Controller
{
    public class RentalController
    {
        private List<Rental> rentalList = new List<Rental>();
        private string filePath;

        public RentalController()
        {
            string appDataDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "VillageRentals"); //get the AppData folder to write the file into
            Directory.CreateDirectory(appDataDirectory);    //create the AppDataDirectory to ensure it exists

            filePath = Path.Combine(appDataDirectory, "rentals.json"); //assigns 'filePath' as path to 'rentals.json"

            LoadRentalsFromFile(); //deserializes file and loads rentals on startup
        }

        //serialize Rental data and also deserialize data to be saved in a file to be used.
        private void SaveRentalsToFile()
        {
            var json = JsonSerializer.Serialize(rentalList);
            File.WriteAllText(filePath, json);
        }

        private void LoadRentalsFromFile()
        {
            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                rentalList = JsonSerializer.Deserialize<List<Rental>>(json) ?? new List<Rental>();
            }
        }


        public void ProcessRental(Customer customer, Equipment item, DateTime rentalDate, DateTime returnDate, decimal cost)
        {
            //create both rental item and the rental
            var rentalItem = new RentalItem(item, rentalDate, returnDate, cost);
            var rental = new Rental(GenerateNewId(), DateTime.Now, customer, returnDate);

            //add both into a list
            rental.rentalItems.Add(rentalItem);
            rentalList.Add(rental);

            SaveRentalsToFile(); //once a rental is processed and the new rental objects are inserted into a list, we will call this method to serialize 'rentalList' then write it to the rile 'rentals.json'
        }

        public List<Rental> GetAllRentals()
        {
            return rentalList;
        }

        private int GenerateNewId()
        {
            return rentalList.Count > 0 ? rentalList.Max(r => r.Id) + 1 : 1;
        }
    }
}