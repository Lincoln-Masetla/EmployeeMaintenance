using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;
using System.Web.Script.Serialization;
using BusinessLogicLayer.Controller.Interface;
using DataAccessLayer.Models;
using EmployeeMaintenanceApi.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EmployeeMaintenanceApi.Tests.Contoller
{
    [TestClass]
    public class PersonControllerTests
    {
        private readonly IPersonLogicRepository _iPersonLogicRepository;
        private static readonly HttpClient _client = new HttpClient();


        public PersonControllerTests() { }
        public PersonControllerTests(IPersonLogicRepository iPersonLogicRepository)
        {
            _iPersonLogicRepository = iPersonLogicRepository;
        }

        private static void SetupControllerForTests(ApiController controller)
        {
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost/api/person");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "person" } });

            controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            controller.Request = request;
            controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
        }

        [TestMethod]
        public void AddTest()
        {
            PersonController controller = new PersonController(_iPersonLogicRepository);

            SetupControllerForTests(controller);
            Person person = new Person
            {
                Active = true,
                BirthDate = DateTime.Now.AddYears(-19),
                FirstName = "TestFirstName",
                LastName = "TestLastName"
            };

            var result = controller.Add(person);

            Assert.Equals(HttpStatusCode.Created, result.StatusCode);
        }
    }
}
