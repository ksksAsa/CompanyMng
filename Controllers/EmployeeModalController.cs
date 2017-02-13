using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CompanyMng.Models;
using System.Data.Entity;

namespace CompanyMng.Controllers
{
    public class EmployeeModalController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            CompanyModel db = new CompanyModel();
            List<Department> list = db.Departments.ToList();
            ViewBag.DepartmentList = new SelectList(list, "DepartmentID", "DepartmentName");

            // List<Employee> listEmp = db.Employees.Select(x => new Employee { FirstName = x.FirstName,  LastName = x.LastName,Email=x.Email, BirthDate=x.BirthDate, Department= x.Department,EmployeeID = x.EmployeeID }).ToList();
            var listEmp = db.Employees.Include(e => e.Department);
            ViewBag.EmployeeList = listEmp;

            return View();
        }


        [HttpPost]
        public ActionResult Index(Employee model)
        {
            CompanyModel db = new CompanyModel();
            try
            {
                List<Department> list = db.Departments.ToList();
                ViewBag.DepartmentList = new SelectList(list, "DepartmentID", "DepartmentName");

                

                    if (model.EmployeeID > 0)
                    {
                        //update
                        Employee emp = db.Employees.SingleOrDefault(x => x.EmployeeID == model.EmployeeID);

                        emp.DepartmentID = model.DepartmentID;
                        emp.FirstName = model.FirstName;
                        emp.LastName = model.LastName;
                        emp.Email = model.Email;
                        emp.BirthDate = model.BirthDate;



                        db.SaveChanges();


                    }
                    else
                    {
                        //Insert




                        Employee emp = new Employee();
                        emp.FirstName = model.FirstName;
                        emp.LastName = model.LastName;
                        emp.DepartmentID = model.DepartmentID;
                        emp.Email = model.Email;
                        emp.BirthDate = model.BirthDate;
                        db.Employees.Add(emp);
                        db.SaveChanges();


                    }
                    return View(model);
                

            }
            catch
            {

                TempData["Msg"] = "An error occured. Please try again";
                return RedirectToAction("Index");
            }

        }

        public ActionResult AddEditEmployee(int EmployeeId)
        {
            CompanyModel db = new CompanyModel();
            List<Department> list = db.Departments.ToList();
            ViewBag.DepartmentList = new SelectList(list, "DepartmentID", "DepartmentName");

            Employee model = new Employee();

            if (EmployeeId > 0)
            {

                Employee emp = db.Employees.SingleOrDefault(x => x.EmployeeID == EmployeeId);
                model.EmployeeID = emp.EmployeeID;
                model.DepartmentID = emp.DepartmentID;
                model.FirstName = emp.FirstName;
                model.LastName = emp.LastName;
                model.BirthDate = emp.BirthDate;

            }
            return PartialView("Partial2", model);
        }

        public JsonResult DeleteEmployee(int EmployeeId)
        {
            CompanyModel db = new CompanyModel();

            bool result = false;
            Employee emp = db.Employees.SingleOrDefault(x=>x.EmployeeID == EmployeeId);
            if (emp != null)
            {
                db.SaveChanges();
                result = true;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ShowEmployee(int EmployeeId)
        {
            CompanyModel db = new CompanyModel();

            Employee listEmp = db.Employees.Find(EmployeeId);

            //ViewBag.EmployeeList = listEmp;

            return PartialView("_Show",listEmp);
        }
    }
}