using CompanyMng.Models;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompanyMng.Repositories
{
    public class DepartmentInfoRepository: IRepository<Department, int>
    {
        [Dependency]
        public CompanyModel context { get; set; }

        public IEnumerable<Department> Get()
        {
            return context.Departments.ToList();
        }

        public Department Get(int id)
        {
            return context.Departments.Find(id);
        }

        public void Add(Department entity)
        {
            context.Departments.Add(entity);
            context.SaveChanges();
        }

        public void Update(Department entity)
        {
            var obj = context.Departments.Find(entity.DepartmentID);
            obj.DepartmentName = entity.DepartmentName;
            obj.MaxEmployess = entity.MaxEmployess;
        }

        public void Remove(Department entity)
        {
            var obj = context.Departments.Find(entity.DepartmentID);
            context.Departments.Remove(obj);
            context.SaveChanges();
        }
    }
}