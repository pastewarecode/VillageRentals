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
        private string filePath;

        public InventoryController()
        {
            string appDataDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "VillageRentals"); //get the AppData folder to write the file into
            Directory.CreateDirectory(appDataDirectory);    //create the AppDataDirectory to ensure it exists

            filePath = Path.Combine(appDataDirectory, "inventory.json"); //assigns 'filePath' as path to 'inventory.json"

            LoadEquipmentFromFile(); //deserializes file and puts it into 'equipmentList on startup
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


        public void AddEquipment(string name, string description, decimal dailyRentalCost)
        {
            var equipment = new Equipment(GenerateNewId(), name, description, dailyRentalCost);
            equipmentList.Add(equipment);

            SaveEquipmentToFile(); //saves equipment everytime a new item is added
        }

        public bool DeleteEquipment(int equipmentId)
        {
            var equipment = equipmentList.FirstOrDefault(e => e.Id == equipmentId);
            if (equipment != null)
            {
                equipmentList.Remove(equipment);

                SaveEquipmentToFile(); //also saves equipment everytime an item is removed. Updates the file with the new list


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
