using RepositoriesLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.Controller.Interface;
using DataAccessLayer.Models;
using RepositoriesLayer;

namespace BusinessLogicLayer.Controller
{
    public class PersonLogic : IPersonLogicRepository
    {
        private IEmployeeMaintenanceRepository _employeeMaintenanceRepository;

        private static readonly log4net.ILog Log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public PersonLogic(IEmployeeMaintenanceRepository employeeMaintenanceRepository)
        {
            _employeeMaintenanceRepository = employeeMaintenanceRepository;
        }

        public Person CreatePerson(Person person)
        {
            try
            {
                //Set Datetime and Active
                person.CreateDate = DateTime.Now;
                person.Active = true;

                //Add Person to context 
                _employeeMaintenanceRepository.Persons.Add(person);

                //Save Person
                _employeeMaintenanceRepository.Complete();

                //Return created person with PersonId
                return person;
            }
            catch (Exception e)
                {
                Log.Info("________________________________________________________________" +
                         "\nClass: PersonLogic \n Method: Update \n Expetion: " + e.InnerException?.Message);
                throw new Exception(e.InnerException?.Message);
            } 
        }
        

        public Person UpdatePerson(Person person)
        {
            try
            {
                //get person by personId
                 var foundPerson = _employeeMaintenanceRepository.Persons.SingleOrDefault(p => p.PersonId == person.PersonId);

                //Update person fiels
                foundPerson.Active = person.Active;
                foundPerson.BirthDate = person.BirthDate;
                foundPerson.FirstName = person.FirstName;
                foundPerson.LastName = person.LastName;
                foundPerson.CreateDate = DateTime.Now;

                _employeeMaintenanceRepository.Complete(); //Save the updated Person
                return foundPerson;
            }
            catch (Exception e)
            {
                Log.Info("________________________________________________________________" +
                         "\n Class: PersonLogic \n Method: UpdatePerson \n Expetion: " + e.InnerException?.Message);
                throw new Exception(e.InnerException?.Message);
            }
        }

        public Person GetPersonById(int id)
        {
            try
            {
                Person foundPerson = _employeeMaintenanceRepository.Persons.SingleOrDefault(p => p.PersonId == id);

                return foundPerson;
            }
            catch (Exception e)
            {
                Log.Info("________________________________________________________________" +
                         "\n Class: PersonLogic \n Method: UpdatePerson \n Expetion: " + e.InnerException?.Message);
                throw new Exception(e.InnerException?.Message);
            }
        }

        public List<Person> GetAllPerson()
        {
            try
            {
                //get all person
                IEnumerable<Person> persons = _employeeMaintenanceRepository.Persons.GetAll();

                //custing persons to a list  
                return (List<Person>) persons;
            }
            catch (Exception e)
            {
                Log.Info("________________________________________________________________" +
                         "\n Class: PersonLogic \n Method: UpdatePerson \n Expetion: " + e.InnerException?.Message);
                throw new Exception(e.InnerException?.Message);
            }
        }
    }
}
