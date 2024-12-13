using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillageRentals.Components.Models
{
    public class Customer : Person
    {
        private string _note;

        public Customer(int id, string firstName, string lastName, string phone, string email, string note)
            : base(id, firstName, lastName, phone, email)
        {
            _note = note;
        }


        public string Note
        {
            get { return _note; }
            set { _note = value; }
        }
    }
}
