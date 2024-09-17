using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGarage_Applicatie.Classes
{
    public class CarOwner
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Vehicle> Vehicles { get; set; } = new List<Vehicle>();

        public CarOwner(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
