using System;
using System.Collections.Generic;
using AutoGarage_Applicatie.Classes;

namespace AutoGarage_Applicatie
{
    public class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Data Source=.;Initial Catalog=GarageManager;Integrated Security=true";
            GarageManager garage = new GarageManager(connectionString);
            bool flag = true;

            while (flag)
            {
                Console.Clear();
                List<string> options = new List<string> {
                    "Add Owner",
                    "Add Vehicle",
                    "Modify License Plate",
                    "Remove Vehicle",
                    "View Owners and Vehicles",
                    "Exit"
                };

                int choice = DisplayMenuOptions(options, "Garage Main Menu - select an option");

                switch (choice)
                {
                    case 1:
                        AddOwner(garage);
                        break;
                    case 2:
                        AddVehicle(garage);
                        break;
                    case 3:
                        ModifyLicensePlate(garage);
                        break;
                    case 4:
                        RemoveVehicle(garage);
                        break;
                    case 5:
                        ViewOwnersAndVehicles(garage);
                        break;
                    case 6:
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
        }

        public static int DisplayMenuOptions(List<string> options, string title)
        {
            Console.WriteLine(title);
            for (int i = 0; i < options.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {options[i]}");
            }

            Console.Write("\nEnter your choice: ");
            int choice;
            if (int.TryParse(Console.ReadLine(), out choice) && choice >= 1 && choice <= options.Count)
            {
                return choice;
            }
            else
            {
                Console.WriteLine("Invalid input, please enter a number between 1 and " + options.Count);
                return DisplayMenuOptions(options, title);
            }
        }

        public static void AddOwner(GarageManager garage)
        {
            Console.Write("Enter owner name: ");
            string name = Console.ReadLine();
            garage.AddOwner(name);
            Console.WriteLine($"Owner '{name}' added.");
            Pause();
        }

        public static void AddVehicle(GarageManager garage)
        {
            Console.WriteLine("Existing Owners:");
            garage.ViewOwners();

            Console.Write("Enter owner ID: ");
            if (int.TryParse(Console.ReadLine(), out int ownerId))
            {
                Console.Write("Enter vehicle license plate: ");
                string licensePlate = Console.ReadLine();

                Console.Write("Enter vehicle description: ");
                string description = Console.ReadLine();

                Console.Write("Is this a commercial vehicle? (yes/no): ");
                bool isCommercial = Console.ReadLine().ToLower() == "yes";
                int? towingCapacity = null;

                if (isCommercial)
                {
                    Console.Write("Enter towing capacity (kg): ");
                    if (int.TryParse(Console.ReadLine(), out int capacity))
                    {
                        towingCapacity = capacity;
                    }
                    else
                    {
                        Console.WriteLine("Invalid towing capacity, setting to 0.");
                        towingCapacity = 0;
                    }
                }

                garage.AddVehicle(ownerId, licensePlate, description, isCommercial, towingCapacity);
                Console.WriteLine($"Vehicle '{licensePlate}' added to owner ID: {ownerId}");
            }
            else
            {
                Console.WriteLine("Invalid owner ID.");
            }
            Pause();
        }

        public static void ModifyLicensePlate(GarageManager garage)
        {
            Console.WriteLine("List of Owners and Their Vehicles:");
            garage.ViewOwnersAndVehicles();

            Console.Write("Enter the vehicle ID for which you want to modify the license plate: ");
            if (int.TryParse(Console.ReadLine(), out int vehicleId))
            {
                Console.Write("Enter new license plate: ");
                string newLicensePlate = Console.ReadLine();

                garage.UpdateLicensePlate(vehicleId, newLicensePlate);
                Console.WriteLine("License plate updated successfully.");
            }
            else
            {
                Console.WriteLine("Invalid vehicle ID.");
            }
            Pause();
        }

        public static void RemoveVehicle(GarageManager garage)
        {
            Console.WriteLine("List of Owners and Their Vehicles:");
            garage.ViewOwnersAndVehicles();

            Console.Write("Enter the vehicle ID you want to remove: ");
            if (int.TryParse(Console.ReadLine(), out int vehicleId))
            {
                garage.RemoveVehicle(vehicleId);
                Console.WriteLine("Vehicle removed successfully.");
                Console.WriteLine("Updated list of Owners and their Vehicles:");
                garage.ViewOwnersAndVehicles();
            }
            else
            {
                Console.WriteLine("Invalid vehicle ID.");
            }
            Pause();
        }

        public static void ViewOwnersAndVehicles(GarageManager garage)
        {
            Console.WriteLine("List of Owners and their Vehicles:");
            garage.ViewOwnersAndVehicles();
            Pause();
        }

        public static void Pause()
        {
            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey();
        }
    }
}