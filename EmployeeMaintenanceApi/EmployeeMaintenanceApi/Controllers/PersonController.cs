using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLogicLayer.Controller;
using BusinessLogicLayer.Controller.Interface;
using DataAccessLayer.Models;
using RepositoriesLayer;
using RepositoriesLayer.Interfaces;

namespace EmployeeMaintenanceApi.Controllers
{
    [RoutePrefix("api/person")]
    public class PersonController : ApiController
    {
        
        private readonly IPersonLogicRepository _iPersonLogicRepository;

        private static readonly log4net.ILog Log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public PersonController(IPersonLogicRepository iPersonLogicRepository)
        {
           _iPersonLogicRepository = iPersonLogicRepository;
        }
    
        [HttpPost]
        [Route("Add")]
        public HttpResponseMessage Add([FromBody]Person person)
        {
            //Validate the object recived against the database model constraints 
            if (ModelState.IsValid)
            {
                try
                {
                    //call the businessLogic Implimentation repo to create Person
                    Person createdPerson = _iPersonLogicRepository.CreatePerson(person);

                    //Save the model and return the model with Id 
                    return Request.CreateResponse(HttpStatusCode.Created, createdPerson);
                }
                catch (Exception e)
                {
                    Log.Info("________________________________________________________________" +
                             "\nClass: PersonController \n Method: Add \n Expetion: " + e.InnerException?.Message);
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Something Went Wrong When Creating A New Person!!");
                }
               
            }

            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [HttpPost]
        [Route("Update")]
        public HttpResponseMessage Update([FromBody] Person person)
        {
            //Validate the object recived against the database model constraints 
            if (ModelState.IsValid)
            {
                try
                {
                    //call the businessLogic Implimentation repo to create Person
                    Person updatePerson = _iPersonLogicRepository.UpdatePerson(person);

                    //Save the model and return the model with Id 
                    return Request.CreateResponse(HttpStatusCode.OK, updatePerson);
                }
                catch (Exception e)
                {
                    Log.Info("________________________________________________________________" +
                             "\nClass: PersonController \n Method: Update \n Expetion: " + e.InnerException?.Message);
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Something Went Wrong When Updating A Person!!");
                }


            }

            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public HttpResponseMessage GetById(int id)
        {
            try
            {
                //call the businessLogic Implimentation repo to get Person
                Person foundPerson = _iPersonLogicRepository.GetPersonById(id);

                if(foundPerson == null) return Request.CreateResponse(HttpStatusCode.NotFound, "Person not found");
                return Request.CreateResponse(HttpStatusCode.Found, foundPerson);
            }
            catch (Exception e)
            {
                Log.Info("________________________________________________________________" +
                         "\nClass: PersonController \n Method: GetById \n Expetion: " + e.InnerException?.Message);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Something Went Wrong When Gettting A Person By Id!!");
               
            }
        }

        [HttpGet]
        [Route("GetAll")]
        public HttpResponseMessage GetAll()
        {
            try
            {
                //call the businessLogic Implimentation repo to get all Persons
                List<Person> persons = _iPersonLogicRepository.GetAllPerson();

                return Request.CreateResponse(HttpStatusCode.OK, persons);
            }
            catch (Exception e)
            {
                Log.Info("________________________________________________________________" +
                         "\nClass: PersonController \n Method: GetAll \n Expetion: " + e.InnerException?.Message);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Something Went Wrong When A list of Person!!");
            }
        }
    }
}
