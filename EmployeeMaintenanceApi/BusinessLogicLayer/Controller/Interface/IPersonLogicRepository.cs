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
    public interface IPersonLogicRepository 
    {
        Person CreatePerson(Person person); //Create a person

        Person UpdatePerson(Person person); //Update a person 

        Person GetPersonById(int id); //get person by id

        List<Person> GetAllPerson(); //Get all person
    }
}
