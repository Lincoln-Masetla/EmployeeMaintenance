using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using DataAccessLayer.Models;

namespace RepositoriesLayer.Interfaces
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        IEnumerable<Employee> GetAllPaged(int pageIndex, int pageSize);//Returns all Employees

        IEnumerable<Employee> GetAllActivePaged(int pageIndex, int pageSize);//Returns all Active Employees
    }
}