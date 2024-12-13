using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillageRentals.Components.Models
{
    public class Equipment
    {
        private int _id;
        private string _name;
        private string _description;
        private decimal _dailyRate;

        public Equipment(int id, string name, string description, decimal dailyRate)
        {
            _id = id;
            _name = name;
            _description = description;
            _dailyRate = dailyRate;
        }


        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public decimal DailyRate
        {
            get { return _dailyRate; }
            set { _dailyRate = value; }
        }
    }
}
