using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.Controller.Interface;
using DataAccessLayer.Models;
using RepositoriesLayer;
using RepositoriesLayer.Interfaces;

namespace BusinessLogicLayer.Controller.Repository
{
    public class EmployeeLogicRepository : IEmployeeLogicRepository
    {
        private IEmployeeMaintenanceRepository _employeeMaintenanceRepository;

        private static readonly log4net.ILog Log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public EmployeeLogicRepository(IEmployeeMaintenanceRepository employeeMaintenanceRepository)
        {
            _employeeMaintenanceRepository = employeeMaintenanceRepository;
        }


        public Employee CreateEmployee(Employee employee)
        {
            try
            {
                //Set Datetime and Active
                employee.CreateDate = DateTime.Now;
                employee.Active = true;

                //Add Employee to context 
                _employeeMaintenanceRepository.Employees.Add(employee);

                //Save Person
                _employeeMaintenanceRepository.Complete();

                //Return created person with PersonId
                return employee;
            }
            catch (Exception e)
            {
                Log.Info("________________________________________________________________" +
                         "\nClass: EmployeeLogic \n Method: CreateEmployee \n Expetion: " + e.InnerException?.Message);
                throw new Exception(e.InnerException?.Message);
            }
        }


        public Employee UpdateEmployee(Employee employee)
        {
            try
            {
                //get employee by employeeId
                var foundEmployee = _employeeMaintenanceRepository.Employees.SingleOrDefault(e => e.EmployeeId == employee.EmployeeId);

                //Update employee fields
                foundEmployee.Active = employee.Active;
                foundEmployee.EmployedDate = employee.EmployedDate;
                foundEmployee.EmployeeNum = employee.EmployeeNum;
                foundEmployee.TerminatedDate = employee.TerminatedDate;
                foundEmployee.CreateDate = DateTime.Now;

                _employeeMaintenanceRepository.Complete(); //Save the updated employee
                return foundEmployee;
            }
            catch (Exception e)
            {
                Log.Info("________________________________________________________________" +
                         "\n Class: EmployeeLogic \n Method: UpdateEmployee \n Expetion: " + e.InnerException?.Message);
                throw new Exception(e.InnerException?.Message);
            }
        }

        public Employee GetEmployeeById(int id)
        {
            try
            {
                Employee foundEmployee = _employeeMaintenanceRepository.Employees.SingleOrDefault(e => e.EmployeeId == id);

                return foundEmployee;
            }
            catch (Exception e)
            {
                Log.Info("________________________________________________________________" +
                         "\n Class: EmployeeLogic \n Method: UpdateEmployee \n Expetion: " + e.InnerException?.Message);
                throw new Exception(e.InnerException?.Message);
            }
        }

        public List<Employee> GetAllEmployee()
        {
            try
            {
                //get all employee
                IEnumerable<Employee> employees = _employeeMaintenanceRepository.Employees.GetAll();

                //custing persons to a list  
                return (List<Employee>)employees;
            }
            catch (Exception e)
            {
                Log.Info("________________________________________________________________" +
                         "\n Class: EmployeeLogic \n Method: EmployeeLogic \n Expetion: " + e.InnerException?.Message);
                throw new Exception(e.InnerException?.Message);
            }
        }
    }
}
