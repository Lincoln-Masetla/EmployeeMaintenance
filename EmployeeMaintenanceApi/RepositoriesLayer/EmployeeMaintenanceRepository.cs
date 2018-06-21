using DataAccessLayer;
using RepositoriesLayer.Interfaces;
using RepositoriesLayer.Repositories;

namespace RepositoriesLayer
{
    public class EmployeeMaintenanceRepository: IEmployeeMaintenanceRepository
    {
        private readonly DBContext _context;
        public IPersonRepository Persons { get; }
        public IEmployeeRepository Employees { get; }

        public EmployeeMaintenanceRepository(DBContext context)
        {
            _context = context;
            Persons = new PersonRepository(_context);
            Employees = new EmployeeRepository(_context);
        }

       
        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}