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
using CompanyMng.Repositories;

namespace CompanyMng.Controllers
{
    public class DepartmentController : Controller
    {
        //  private CompanyModel db = new CompanyModel();

        private IRepository<Department, int> _repoDep;
        private IRepository<Employee, int> _repository;

        public DepartmentController(IRepository<Department,int> _repo, IRepository<Employee, int> _reposi)
        {
            _repoDep = _repo;
            _repository = _reposi;
        }

        // GET: Department
        public ActionResult Index()
        {
            return View(_repoDep.Get());
           // return View(db.Departments.ToList());
        }

        // GET: Department/Details/5
        public ActionResult Details(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Department department = db.Departments.Find(id);
            Department department = _repoDep.Get(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // GET: Department/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Department/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DepartmentID,DepartmentName,MaxEmployess")] Department department)
        {
            if (ModelState.IsValid)
            {
                _repoDep.Add(department);
               // db.Departments.Add(department);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(department);
        }

        // GET: Department/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //  Department department = db.Departments.Find(id);
            Department department = _repoDep.Get(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: Department/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DepartmentID,DepartmentName,MaxEmployess")] Department department)
        {
            if (ModelState.IsValid)
            {
              //  db.Entry(department).State = EntityState.Modified;

                var empNumber = _repository.Get().Where(x => x.DepartmentID == department.DepartmentID).ToList().Count;
                if (department.MaxEmployess < empNumber)
                {
                    TempData["Msg"] = "Max employess number must me equals or greater of the current employess in this department";
                    return RedirectToAction("Edit");
                }
                else
                {
                    //  db.SaveChanges();
                    _repoDep.Update(department);
                    return RedirectToAction("Index");
                }
            }
            return View(department);
        }

        // GET: Department/Delete/5
        public ActionResult Delete(int id)
        {

            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Department department = db.Departments.Find(id);
            Department department = _repoDep.Get(id);
            if (department == null)
            {
                return HttpNotFound();
            }



            return View(department);
        }

        // POST: Department/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //check if can delete department
            var empNumper = _repository.Get().Where(x => x.DepartmentID == id).ToList().Count;
            if (empNumper != 0)
            {
                TempData["Msg"] = "You can not delete department with employees";
                return RedirectToAction("Delete");
            }
            else
            {
                //Department department = db.Departments.Find(id);
                Department department = _repoDep.Get(id);
                _repoDep.Remove(department);
                //db.Departments.Remove(department);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
              //  db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
