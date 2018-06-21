using System.Data.Entity;
using DataAccessLayer.Models;

namespace DataAccessLayer
{
    public class DBContext : DbContext
    {
        public DBContext() : base("name=EmployeeMaintenance") { }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Person> Persons { get; set; }
    }
}