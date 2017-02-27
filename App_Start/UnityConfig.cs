using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using CompanyMng.Repositories;
using CompanyMng.Models;

namespace CompanyMng
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IRepository<Employee, int>, EmployeeInfoRepository>();
            container.RegisterType<IRepository<Department, int>, DepartmentInfoRepository>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}