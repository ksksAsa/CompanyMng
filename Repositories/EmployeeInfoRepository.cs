using CompanyMng.Models;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompanyMng.Repositories
{
    public class EmployeeInfoRepository:IRepository<Employee,int>
    {
        [Dependency]
        public CompanyModel context { get; set; }

        public IEnumerable<Employee> Get()
        {
            return context.Employees.ToList();
        }

        public Employee Get(int id)
        {
            return context.Employees.Find(id);
        }

        public void Add(Employee entity)
        {
            context.Employees.Add(entity);
            context.SaveChanges();
        }

        public void Update(Employee entity)
        {
            var obj = context.Employees.Find(entity.EmployeeID);
            obj.FirstName = entity.FirstName;
            obj.LastName = entity.LastName;
            obj.Email = entity.Email;
            obj.DepartmentID = entity.DepartmentID;
            obj.BirthDate = entity.BirthDate;
            context.SaveChanges();
        }

        public void Remove(Employee entity)
        {
            var obj = context.Employees.Find(entity.EmployeeID);
            context.Employees.Remove(obj);
            context.SaveChanges();
        }
    }
}