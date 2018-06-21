using System.Collections.Generic;
using System.Linq;
using DataAccessLayer;
using DataAccessLayer.Models;
using RepositoriesLayer.Interfaces;

namespace RepositoriesLayer.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(DBContext context) : base(context)
        {
        }

        public DBContext DbContext => Context as DBContext; //Casting the context inherited from the generic repisotory cls=ass and custing that to DBContext

        public IEnumerable<Employee> GetAllPaged(int pageIndex, int pageSize)
        {
            return DbContext.Employees
                .OrderBy(e => e.PersonId)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public IEnumerable<Employee> GetAllActivePaged(int pageIndex, int pageSize)
        {
            return DbContext.Employees
                .Where(e => e.Active)
                .OrderBy(e => e.PersonId)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }
    }
}