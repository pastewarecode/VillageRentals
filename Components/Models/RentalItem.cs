using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillageRentals.Components.Models
{
    public class RentalItem
    {
        private Equipment _item;
        private DateTime? _rentalDate;
        private DateTime? _returnDate;
        private decimal _cost;

        public RentalItem(Equipment item, DateTime rentalDate, DateTime? returnDate, decimal cost)
        {
            _item = item;
            _rentalDate = rentalDate;
            _returnDate = returnDate;
            _cost = cost;
        }

        public Equipment Item
        {
            get { return _item; }
            set { _item = value; }
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
