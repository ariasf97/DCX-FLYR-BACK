using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Journey
    {
        public Journey(List<Flight> currentPath, string origin, string destination, double totalPrice)
        {
            CurrentPath = currentPath;
            Origin = origin;
            Destination = destination;
            TotalPrice = totalPrice;
        }

        public string Origin { get; set; }
        public string Destination { get; set; }
        public double Price { get; set; }

        public List<Flight> Flights { get; set; }
        public List<Flight> CurrentPath { get; }
        public double TotalPrice { get; }
    }
}

