using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VillageRentals.Components.Models;

namespace VillageRentals.Components.Controller
{
    public class ClientController
    {
        private List<Customer> customerList = new List<Customer>();

        public void AddClient(string firstName, string lastName, string phone, string email, string note = "")
        {
            var customer = new Customer(GenerateNewId(), firstName, lastName, phone, email, note);
            customerList.Add(customer);
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
