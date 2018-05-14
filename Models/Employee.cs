using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList.Mvc;

namespace HRMS.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public int EmployeeNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateJoined { get; set; }
        public int? Extension { get; set; }
        public Role Role { get; set; }
        public virtual List<Role> Roles { get; set; }
    }
}