using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLogicLayer.Controller;
using BusinessLogicLayer.Controller.Interface;
using DataAccessLayer.Models;
using EmployeeMaintenanceApi.Models;

namespace EmployeeMaintenanceApi.Controllers
{
    [RoutePrefix("api/employee")]
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeLogicRepository _iEmployeeLogicRepository;

        private readonly IPersonLogicRepository _iPersonLogicRepository;

        private static readonly log4net.ILog Log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public EmployeeController(IEmployeeLogicRepository iEmployeeLogicRepository, IPersonLogicRepository iPersonLogicRepository)
        {
            _iEmployeeLogicRepository = iEmployeeLogicRepository;
            _iPersonLogicRepository = iPersonLogicRepository;
        }

        [HttpPost]
        [Route("Add")]
        public HttpResponseMessage Add([FromBody]EmployeeModel employeeModel)
        {
            //Validate the object recived against the database model constraints 
            if (ModelState.IsValid)
            {
                try
                {
                    //Map EmployeeModel To Person Object
                    Person person = MapEmployeeModelToPerson(employeeModel);

                    //call the businessLogic Implimentation repo to create Person
                    Person createdPerson = _iPersonLogicRepository.CreatePerson(person);

                    //Map EmployeeModel To Employee Object
                    Employee employee = MapEmployeeModelToEmployee(employeeModel);

                    //add created person to the employee object
                    employee.PersonId = createdPerson.PersonId;

                    //call the businessLogic Implimentation repo to create Employee
                    Employee createdEmployee = _iEmployeeLogicRepository.CreateEmployee(employee);

                    //Save the model and return the model with Id 
                    return Request.CreateResponse(HttpStatusCode.Created, createdEmployee);
                }
                catch (Exception e)
                {
                    Log.Info("________________________________________________________________" +
                             "\nClass: EmployeeController \n Method: Add \n Expetion: " + e.InnerException?.Message);
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Something Went Wrong When Creating A New Employee!!");
                }

            }

            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        private Employee MapEmployeeModelToEmployee(EmployeeModel employeeModel)
        {
            Employee employee = new Employee
            {
                EmployeeId = employeeModel.EmployeeId,
                EmployedDate = employeeModel.EmployedDate,
                EmployeeNum = employeeModel.EmployeeNum,
                TerminatedDate = employeeModel.TerminatedDate
            };
            return employee;
        }

        private Person MapEmployeeModelToPerson(EmployeeModel employeeModel)
        {
            Person person = new Person
            {
                Active = employeeModel.Active,
                FirstName = employeeModel.FirstName,
                LastName = employeeModel.LastName,
                BirthDate = employeeModel.BirthDate
            };
            return person;
        }

        [HttpPost]
        [Route("Update")]
        public HttpResponseMessage Update([FromBody] EmployeeModel employeeModel)
        {
            //Validate the object recived against the database model constraints 
            if (ModelState.IsValid)
            {
                try
                {
                    //Map EmployeeModel To Person Object
                    Person person = MapEmployeeModelToPerson(employeeModel);

                    //Map EmployeeModel To Employee Object
                    Employee employee = MapEmployeeModelToEmployee(employeeModel);

                    //call the businessLogic Implimentation repo to update Employee
                    Employee updateEmployee = _iEmployeeLogicRepository.UpdateEmployee(employee);

                    person.PersonId = updateEmployee.PersonId;

                    //call the businessLogic Implimentation repo to update Person
                    Person updatePerson = _iPersonLogicRepository.UpdatePerson(person);

                    //Save the model and return the model with Id 
                    return Request.CreateResponse(HttpStatusCode.OK, updateEmployee);
                }
                catch (Exception e)
                {
                    Log.Info("________________________________________________________________" +
                             "\nClass: EmployeeController \n Method: Update \n Expetion: " + e.InnerException?.Message);
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Something Went Wrong When Updating A Employee!!");
                }


            }

            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public HttpResponseMessage GetById(int id)
        {
            try
            {
                
                //call the businessLogic Implimentation repo to get Employee
                Employee foundEmployee = _iEmployeeLogicRepository.GetEmployeeById(id);

                Person foundPerson = _iPersonLogicRepository.GetPersonById(foundEmployee.PersonId);

                EmployeeModel employeeModel = CreateEmployeeModelFromEmployeeAndPerson(foundPerson, foundEmployee);

                return Request.CreateResponse(HttpStatusCode.Found, employeeModel);
            }
            catch (Exception e)
            {
                Log.Info("________________________________________________________________" +
                         "\nClass: EmployeeController \n Method: GetById \n Expetion: " + e.InnerException?.Message);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Something Went Wrong When Gettting A Person By Id!!");

            }
        }

        private EmployeeModel CreateEmployeeModelFromEmployeeAndPerson(Person foundPerson, Employee foundEmployee)
        {
            EmployeeModel employeeModel = new EmployeeModel();
            employeeModel.PersonId = foundPerson.PersonId;
            employeeModel.Active = foundPerson.Active;
            employeeModel.FirstName = foundPerson.FirstName;
            employeeModel.LastName = foundPerson.LastName;
            employeeModel.BirthDate = foundPerson.BirthDate;
            employeeModel.EmployedDate = foundEmployee.EmployedDate;
            employeeModel.EmployeeNum = foundEmployee.EmployeeNum;
            employeeModel.TerminatedDate = foundEmployee.TerminatedDate;
            employeeModel.EmployeeId = foundEmployee.EmployeeId;

            return employeeModel;

        }

        [HttpGet]
        [Route("GetAll")]
        public HttpResponseMessage GetAll()
        {
            try
            {
                List<EmployeeModel> employeeModelList = new List<EmployeeModel>();

                //call the businessLogic Implimentation repo to get all Employees
                List<Employee> employees = _iEmployeeLogicRepository.GetAllEmployee();

                foreach (var employee in employees)
                {
                    //get the employee and person then map the object to EmployeeModel
                    Employee foundEmployee = _iEmployeeLogicRepository.GetEmployeeById(employee.EmployeeId);
                    Person foundPerson = _iPersonLogicRepository.GetPersonById(employee.PersonId);
                    EmployeeModel employeeModel = CreateEmployeeModelFromEmployeeAndPerson(foundPerson, foundEmployee);
                    employeeModelList.Add(employeeModel);
                }
                return Request.CreateResponse(HttpStatusCode.OK, employeeModelList);
            }
            catch (Exception e)
            {
                Log.Info("________________________________________________________________" +
                         "\nClass: EmployeeController \n Method: GetAll \n Expetion: " + e.InnerException?.Message);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Something Went Wrong When A list of Person!!");
            }
        }
    }
}
