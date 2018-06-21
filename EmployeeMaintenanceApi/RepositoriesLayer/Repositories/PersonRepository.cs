using System.Collections.Generic;
using System.Linq;
using DataAccessLayer;
using DataAccessLayer.Models;
using RepositoriesLayer.Interfaces;

namespace RepositoriesLayer.Repositories
{
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        public PersonRepository(DBContext context) : base(context)
        {
        }
        public DBContext DbContext => Context as DBContext; //Casting the context inherited from the generic repisotory cls=ass and custing that to DBContext
        public IEnumerable<Person> GetAllPaged(int pageIndex, int pageSize)
        {
            return DbContext.Persons
                .OrderBy(p => p.PersonId)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public IEnumerable<Person> GetAllActivePaged(int pageIndex, int pageSize)
        {
            return DbContext.Persons
                .Where(p => p.Active)
                .OrderBy(p => p.PersonId)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

    }
}