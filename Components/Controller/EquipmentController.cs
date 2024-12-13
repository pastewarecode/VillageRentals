using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VillageRentals.Components.Models;

namespace VillageRentals.Components.Controller
{
    public class EquipmentController
    {
        private List<Equipment> equipmentList = new List<Equipment>();

        public void AddEquipment(string name, string description, decimal dailyRentalCost, string category)
        {
            var equipment = new Equipment(GenerateNewId(), name, description, dailyRentalCost);
            equipmentList.Add(equipment);
        }

        public bool DeleteEquipment(int equipmentId)
        {
            var equipment = equipmentList.FirstOrDefault(e => e.Id == equipmentId);
            if (equipment != null)
            {
                equipmentList.Remove(equipment);
                return true;
            }
            return false;
        }

        public List<Equipment> GetAllEquipment()
        {
            return equipmentList;
        }

        private int GenerateNewId()
        {
            return equipmentList.Count > 0 ? equipmentList.Max(e => e.Id) + 1 : 1;
        }
    }
}
