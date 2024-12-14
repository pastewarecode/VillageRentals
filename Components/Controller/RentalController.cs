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
        private const string filePath = "rentals.json";

        public RentalController()
        {
            LoadRentalsFromFile();
        }

        public void ProcessRental(Customer customer, Equipment item, DateTime rentalDate, DateTime returnDate, decimal cost)
        {
            //create both rental item and the rental
            var rentalItem = new RentalItem(item, rentalDate, returnDate, cost);
            var rental = new Rental(GenerateNewId(), DateTime.Now, customer, returnDate);

            //add both into a list
            rental.rentalItems.Add(rentalItem);
            rentalList.Add(rental);
        }


        public List<Rental> GetAllRentals()
        {
            return rentalList;
        }

        private int GenerateNewId()
        {
            return rentalList.Count > 0 ? rentalList.Max(r => r.Id) + 1 : 1;
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
    }
}