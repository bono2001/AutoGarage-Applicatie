using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGarage_Applicatie.Classes
{
    public class GarageManager
    {
        private readonly DAL dal;

        public GarageManager(string connectionString)
        {
            dal = new DAL(connectionString);
        }

        public void AddOwner(string name)
        {
            dal.AddOwner(name);
        }

        public void AddVehicle(int ownerId, string licensePlate, string description, bool isCommercial, int? towingCapacity)
        {
            if (IsValidLicensePlate(licensePlate, isCommercial))
            {
                dal.AddVehicle(licensePlate, description, isCommercial, towingCapacity, ownerId);
            }
            else
            {
                Console.WriteLine("Invalid license plate format.");
            }
        }

        public void UpdateLicensePlate(int vehicleId, string newLicensePlate)
        {
            // Validate the new license plate
            bool isCommercial = IsCommercialVehicle(vehicleId);
            if (IsValidLicensePlate(newLicensePlate, isCommercial))
            {
                dal.UpdateLicensePlate(vehicleId, newLicensePlate);
            }
            else
            {
                Console.WriteLine("Invalid license plate format.");
            }
        }

        public void RemoveVehicle(int vehicleId)
        {
            dal.DeleteVehicle(vehicleId);
        }

        public void ViewOwners()
        {
            DataTable owners = dal.GetOwners();
            foreach (DataRow row in owners.Rows)
            {
                Console.WriteLine($"Owner ID: {row["OwnerID"]}, Name: {row["Name"]}");
            }
        }

        public void ViewOwnersAndVehicles()
        {
            DataTable owners = dal.GetOwners();
            foreach (DataRow ownerRow in owners.Rows)
            {
                int ownerId = (int)ownerRow["OwnerID"];
                Console.WriteLine($"Owner: {ownerRow["Name"]} (ID: {ownerId})");

                DataTable vehicles = dal.GetOwnerVehicles(ownerId);
                foreach (DataRow vehicleRow in vehicles.Rows)
                {
                    string vehicleInfo = $" - Vehicle ID: {vehicleRow["VehicleID"]}, License Plate: {vehicleRow["LicensePlate"]}, Description: {vehicleRow["Description"]}";
                    Console.WriteLine(vehicleInfo);
                }
                Console.WriteLine();
            }
        }

        private bool IsCommercialVehicle(int vehicleId)
        {
            DataTable vehicle = dal.GetVehicles();
            DataRow vehicleRow = vehicle.Select($"VehicleID = {vehicleId}").FirstOrDefault();
            return vehicleRow != null && (bool)vehicleRow["IsCommercial"];
        }

        public static bool IsValidLicensePlate(string licensePlate, bool isCommercial)
        {
            // Check if the license plate is 8 characters long
            if (licensePlate.Length != 8)
            {
                Console.WriteLine("Error: The license plate must be exactly 8 characters long.");
                return false;
            }

            // Check if the license plate contains exactly 2 hyphens
            int hyphenCount = licensePlate.Count(c => c == '-');
            if (hyphenCount != 2)
            {
                Console.WriteLine("Error: The license plate must contain exactly 2 hyphens.");
                return false;
            }

            // Check if the license plate starts with "V" for commercial vehicles
            if (isCommercial && !licensePlate.StartsWith("V", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Error: The license plate for a commercial vehicle must start with the letter 'V'.");
                return false;
            }

            return true;
        }
    }
}