using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using VillageRentals.Components.Models;

namespace VillageRentals.Components.Controller
{
    public class InventoryController
    {
        private List<Equipment> equipmentList = new List<Equipment>();
        private const string filePath = "equipment.json";

        public InventoryController()
        {
            LoadEquipmentFromFile();
        }

        public void AddEquipment(string name, string description, decimal dailyRentalCost)
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

        //serialize equipment data and also deserialize data to be saved in a file to be used.
        private void SaveEquipmentToFile()
        {
            var json = JsonSerializer.Serialize(equipmentList);
            File.WriteAllText(filePath, json);
        }

        private void LoadEquipmentFromFile()
        {
            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                equipmentList = JsonSerializer.Deserialize<List<Equipment>>(json) ?? new List<Equipment>();
            }
        }
    }
}
