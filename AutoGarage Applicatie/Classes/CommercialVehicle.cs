using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGarage_Applicatie.Classes
{
    public class CommercialVehicle : Vehicle
    {
        public int TowingWeight { get; set; }

        public CommercialVehicle(int id, string licensePlate, int towingWeight, string? description = null)
            : base(id, licensePlate, description)
        {
            TowingWeight = towingWeight;
        }
    }
}