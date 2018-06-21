using System.Web.Http;
using BusinessLogicLayer.Controller;
using BusinessLogicLayer.Controller.Interface;
using BusinessLogicLayer.Controller.Repository;
using RepositoriesLayer;
using Unity;
using Unity.WebApi;

namespace EmployeeMaintenanceApi
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            //Regiseter the Application Action Interface to the Dependency
            container.RegisterType<IEmployeeMaintenanceRepository, EmployeeMaintenanceRepository>();
            container.RegisterType<IPersonLogicRepository, PersonLogic>();
            container.RegisterType<IEmployeeLogicRepository, EmployeeLogicRepository>();
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}