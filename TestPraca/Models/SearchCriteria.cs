using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestPraca.Models
{
    public class SearchCriteria
    {
        public string Keyword { get; set; }
        public DateTime EmployeeDateOfBirthFrom { get; set; }
        public DateTime EmployeeDateOfBirthTo { get; set; }
        public string[] EmployeeJobTitles { get; set; }

        public SearchCriteria()
        {
            EmployeeJobTitles = new string[5];
        }
    }
}