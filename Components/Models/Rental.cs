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
        private Equipment _equipment;
        private DateTime? _rentalDate;
        private DateTime? _returnDate;
        private decimal _cost;

        public List<RentalItem> rentalItems { get; set; }

        public Rental(int id, DateTime date, Customer customer, DateTime? returnDate = null)
        {
            _id = id;
            _date = date;
            _customer = customer;
            _returnDate = returnDate;
            rentalItems = new List<RentalItem>();
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

        public Equipment Equipment
        {
            get { return _equipment; }
            set { _equipment = value; }
        }

        public DateTime? RentalDate
        {
            get { return _rentalDate; }
            set { _rentalDate = value; }
        }

        public DateTime? ReturnDate
        {
            get { return _returnDate; }
            set { _returnDate = value; }
        }

        public decimal Cost
        {
            get { return _cost; }
            set { _cost = value; }
        }
    }
}
