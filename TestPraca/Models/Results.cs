using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestPraca.Models
{
    public class Results
    {

        public List<Company> Companies { get; set; }

        public Results()
        {
            Companies = new List<Company>();
        }
    }
}