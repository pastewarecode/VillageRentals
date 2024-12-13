using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillageRentals.Components.Models
{
    public class Rental
    {
        private int _id;
        private DateTime _date;
        private Customer _customer;
        private DateTime? _returnDate;

        public Rental(int id, DateTime date, Customer customer, DateTime? returnDate = null)
        {
            _id = id;
            _date = date;
            _customer = customer;
            _returnDate = returnDate;
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public Customer Customer
        {
            get { return _customer; }
            set { _customer = value; }
        }

        public DateTime? ReturnDate
        {
            get { return _returnDate; }
            set { _returnDate = value; }
        }
    }
}
