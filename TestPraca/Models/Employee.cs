//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestPraca.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Employee
    {
        public System.Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public System.DateTime DataOfBirth { get; set; }
        public string JobTitle { get; set; }
        public System.Guid CompanyId { get; set; }
    
        public virtual Company Company { get; set; }
    }
}
