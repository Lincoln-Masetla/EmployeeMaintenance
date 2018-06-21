using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoriesLayer.Interfaces;

namespace RepositoriesLayer
{
    public interface IEmployeeMaintenanceRepository : IDisposable
    {
        IPersonRepository Persons { get; }
        IEmployeeRepository Employees { get; }
        int Complete();
    }
}
