using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGarage_Applicatie.Classes
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public string LicensePlate { get; set; }

        public Vehicle(int id, string licensePlate, string? description = null)
        {
            Id = id;
            LicensePlate = licensePlate;
            Description = description;
        }
    }
}