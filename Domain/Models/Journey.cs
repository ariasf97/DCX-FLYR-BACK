using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Journey
    {
       
        public Journey(List<Flight> flights, string origin, string destination, double price)
        {
            Flights = flights;
            Origin = origin;
            Destination = destination;
            Price = price;
        }



        public string Origin { get; set; }
        public string Destination { get; set; }
        public double Price { get; set; }

        public List<Flight> Flights { get; set; }

    }
}

