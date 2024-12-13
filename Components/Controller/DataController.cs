using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VillageRentals.Components.Models;
using ClosedXML.Excel;

namespace VillageRentals.Components.Controller
{
    public class ExcelService
    {
        private string filePath = "data-sample.xlsx";


        //-----EQUIPMENT-----
        //reads equipment data
        public List<Equipment> ReadEquipment()
        {
            var equipmentList = new List<Equipment>();
            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheet("Rental Equipment");
                foreach (var row in worksheet.RowsUsed().Skip(1))
                {
                    var equipmentId = int.Parse(row.Cell(1).Value.ToString());
                    var name = row.Cell(3).Value.ToString();
                    var description = row.Cell(4).Value.ToString();
                    var dailyRate = decimal.Parse(row.Cell(5).Value.ToString());

                    var equipment = new Equipment(equipmentId, name, description, dailyRate);

                    equipmentList.Add(equipment);
                }
            }
            return equipmentList;
        }

        //writes equipment data
        public void WriteEquipment(List<Equipment> equipmentList)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.AddWorksheet("Rental Equipment");
                for (int i = 0; i < equipmentList.Count; i++)
                {
                    var equipment = equipmentList[i];
                    worksheet.Cell(i + 2, 1).Value = equipment.Id;
                    worksheet.Cell(i + 2, 3).Value = equipment.Name;
                    worksheet.Cell(i + 2, 4).Value = equipment.Description;
                    worksheet.Cell(i + 2, 5).Value = equipment.DailyRate;
                }
                workbook.SaveAs(filePath);
            }
        }


        //-----CUSTOMER-----
        //reads customers data
        public List<Customer> ReadCustomers()
        {
            var customers = new List<Customer>();
            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheet("Customer Information");
                foreach (var row in worksheet.RowsUsed().Skip(1))
                {
                    var customerId = int.Parse(row.Cell(1).Value.ToString());
                    var lastName = row.Cell(2).Value.ToString();
                    var firstName = row.Cell(3).Value.ToString();
                    var contactPhone = row.Cell(4).Value.ToString();
                    var email = row.Cell(5).Value.ToString();

                    var customer = new Customer(customerId, firstName, lastName, contactPhone, email, string.Empty);
                    customers.Add(customer);
                }
            }
            return customers;
        }

        //writes customers data to spreadsheet
        public void WriteCustomers(List<Customer> customers)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.AddWorksheet("Customer Information");
                for (int i = 0; i < customers.Count; i++)
                {
                    var customer = customers[i];
                    worksheet.Cell(i + 2, 1).Value = customer.Id;
                    worksheet.Cell(i + 2, 2).Value = customer.LastName;
                    worksheet.Cell(i + 2, 3).Value = customer.FirstName;
                    worksheet.Cell(i + 2, 4).Value = customer.Phone;
                    worksheet.Cell(i + 2, 5).Value = customer.Email;
                }
                workbook.SaveAs(filePath);
            }
        }


        //-----RENTAL-----
        //reads rental information
        public List<Rental> ReadRentals(List<Customer> customers, List<Equipment> equipmentList)
        {
            var rentals = new List<Rental>();
            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheet("Rental Information");
                foreach (var row in worksheet.RowsUsed().Skip(1))
                {
                    var rentalId = int.Parse(row.Cell(1).Value.ToString());
                    var date = DateTime.Parse(row.Cell(2).Value.ToString());
                    var customerId = int.Parse(row.Cell(3).Value.ToString());
                    var equipmentId = int.Parse(row.Cell(4).Value.ToString());
                    var rentalDate = DateTime.Parse(row.Cell(5).Value.ToString());
                    var returnDate = DateTime.Parse(row.Cell(6).Value.ToString());
                    var cost = decimal.Parse(row.Cell(7).Value.ToString());

                    var customer = customers.FirstOrDefault(c => c.Id == customerId);
                    var equipment = equipmentList.FirstOrDefault(e => e.Id == equipmentId);

                    var rental = new Rental(rentalId, date, customer, returnDate);
                    rentals.Add(rental);
                }
            }
            return rentals;
        }


        //writes rental information to the spreadsheet
        public void WriteRentals(List<Rental> rentals)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.AddWorksheet("Rental Information");
                //writes headers in corresponding rows
                worksheet.Cell(1, 1).Value = "Rental ID";
                worksheet.Cell(1, 2).Value = "Date";
                worksheet.Cell(1, 3).Value = "Customer ID";
                worksheet.Cell(1, 4).Value = "Equipment ID";
                worksheet.Cell(1, 5).Value = "Rental Date";
                worksheet.Cell(1, 6).Value = "Return Date";
                worksheet.Cell(1, 7).Value = "Cost";

                //Writes rental data, starts at row 2
                for (int i = 0; i < rentals.Count; i++)
                {
                    var rental = rentals[i];
                    worksheet.Cell(i + 2, 1).Value = rental.Id;
                    worksheet.Cell(i + 2, 2).Value = rental.Date.ToString("yyyy-MM-dd");
                    worksheet.Cell(i + 2, 3).Value = rental.Customer.Id;
                    worksheet.Cell(i + 2, 4).Value = rental.Equipment.Id;
                    worksheet.Cell(i + 2, 5).Value = rental.Date.ToString("yyyy-MM-dd");
                    worksheet.Cell(i + 2, 6).Value = rental.ReturnDate?.ToString("yyyy-MM-dd");
                    worksheet.Cell(i + 2, 7).Value = rental.Cost;
                }

                //save information 
                workbook.SaveAs(filePath);
            }
        }
    }
}
