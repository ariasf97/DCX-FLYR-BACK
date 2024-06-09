using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Filter
    {
        public string Origin { get; set; }
        public string Destination { get; set; }

        public CurrencyType CurrencyType { get; set; }
 
    }
}
