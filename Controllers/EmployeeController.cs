using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRMS.DA;
using HRMS.Models;
using PagedList;

namespace HRMS.Controllers
{
    public class EmployeeController : Controller
    {
        DBAccess dba = new DBAccess();
        // GET: Employee
        //public ActionResult Index()
        //{
        //    return View();
        //}
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var employees = dba.GetListOfEmployees();
            if (!String.IsNullOrEmpty(searchString))
            {
                employees = employees.Where(s => s.LastName.Contains(searchString)
                                       || s.FirstName.Contains(searchString)).ToList();
            }
            switch (sortOrder)
            {
                case "name_desc":
                    employees = employees.OrderByDescending(s => s.FirstName).ToList();
                    break;
                case "Date":
                    employees = employees.OrderBy(e => e.DateJoined).ToList();
                    break;
                case "date_desc":
                    employees = employees.OrderByDescending(e => e.DateJoined).ToList();
                    break;
                default:  // Name ascending 
                    employees = employees.OrderBy(e => e.FirstName).ToList();
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(employees.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult AddEmployee()
        {
            Employee employee = new Employee();
            employee.Roles = dba.GetListOfRoles();
            return View(employee);
        }
       
        public ActionResult Delete(int id)
        {
            dba.DeleteEmployee(id);

            return RedirectToAction("Index");
        }
    }
}