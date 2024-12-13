using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VillageRentals.Components.Models;

namespace VillageRentals.Components.Controller
{
    public class RentalController
    {
        private List<Rental> rentalList = new List<Rental>();

        public void ProcessRental(Customer customer, Equipment item, DateTime rentalDate, DateTime returnDate, decimal cost)
        {
            var rentalItem = new RentalItem(item, rentalDate, returnDate, cost);
            var rental = new Rental(GenerateNewId(), DateTime.Now, customer, returnDate);


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
    }
}