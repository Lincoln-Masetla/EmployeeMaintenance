using System;
using System.Collections;
using System.Collections.Generic;
using DataAccessLayer.Models;

namespace RepositoriesLayer.Interfaces
{
    public interface IPersonRepository : IRepository<Person>
    {
        
        IEnumerable<Person> GetAllPaged(int pageIndex, int pageSize);//Returns all People
        
        IEnumerable<Person> GetAllActivePaged(int pageIndex, int pageSize);//Returns all Active People

    }
}