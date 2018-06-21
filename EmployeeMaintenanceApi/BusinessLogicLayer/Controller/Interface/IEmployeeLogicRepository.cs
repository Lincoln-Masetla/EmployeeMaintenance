using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;
using RepositoriesLayer;
using RepositoriesLayer.Interfaces;

namespace BusinessLogicLayer.Controller.Interface
{
    public interface IEmployeeLogicRepository 
    {
        Employee CreateEmployee(Employee person); //Create a employee

        Employee UpdateEmployee(Employee person); //Update a employee 

        Employee GetEmployeeById(int id); //get employee by id

        List<Employee> GetAllEmployee(); //Get all employee
    }
}
