using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CompanyMng;
using CompanyMng.Models;

namespace CompanyMng.Controllers
{
    public class EmployeeController : Controller
    {
        private CompanyModel db = new CompanyModel();

        // GET: Employee
        public ActionResult Index()
        {
            var employees = db.Employees.Include(e => e.Department);
            return View(employees.ToList());
        }

        // GET: Employee/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            // return View(employee);
            return PartialView("Details",employee);
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepartmentName");
            return PartialView("Create");
           // return View();
        }

        // POST: Employee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeID,FirstName,LastName,Email,BirthDate,DepartmentID")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                int maxEmpNumber = db.Departments.Where(x => x.DepartmentID == employee.DepartmentID).SingleOrDefault().MaxEmployess;
                int employessInDept = db.Employees.Where(x => x.DepartmentID == employee.DepartmentID).ToList().Count;

                //confirmation of numner of employess
                if (maxEmpNumber == employessInDept)
                {
                    TempData["Msg"] = "You have already insert the max number of employees in this departmnet";
                    return RedirectToAction("Create");
                }
                else
                {
                    db.Employees.Add(employee);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }               
            }

            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepartmentName", employee.DepartmentID);
            return View(employee);
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepartmentName", employee.DepartmentID);
             //return View(employee);
            return PartialView("Edit", employee);
        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeID,FirstName,LastName,Email,BirthDate,DepartmentID")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;

                int maxEmpNumber = db.Departments.Where(x => x.DepartmentID == employee.DepartmentID).SingleOrDefault().MaxEmployess;
                int employessInDept = db.Employees.Where(x => x.DepartmentID == employee.DepartmentID).ToList().Count;

                //confirmation of number of employess if an employess is changing department
                if (maxEmpNumber == employessInDept)
                {
                    TempData["Msg"] = "You have already insert the max number of employees in this department";
                    return RedirectToAction("Edit");
                }

                else
                {
                    //TempData["Msg"] = "success";
                    db.SaveChanges();
                     return RedirectToAction("Index");
                   // return PartialView("Details", employee);
                }
            }
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepartmentName", employee.DepartmentID);
           
                return PartialView("Edit", employee);
                    
           // return View(employee);
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
