using ContactMicroservice;
using ContactMicroservice.Controllers;
using ContactMicroservice.DataAccess.Abstract;
using ContactMicroservice.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace Test
{
    public class ContactMicroserviceTest
    {
        Mock<IPersonRepository> _personRepository; 

        [SetUp]
        public void Setup()
        {
            _personRepository = new Mock<IPersonRepository>();
        }

        List<Person> persons = new List<Person>() { 
          new Person(){ Name="name1", Surname="surname1", Company="company1", PersonId=new Guid("070a17de-bb06-4a73-9826-d676e22e92f3") },
          new Person(){ Name="name2", Surname="surname2", Company="company2", PersonId=new Guid("7c8ac444-d06c-4b6b-8ffe-5b8e728808f7") }, 
        };
         
        [Test]
        public void GetAllPersons()
        {
            _personRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Person, bool>>>())).ReturnsAsync((Expression<Func<Person, bool>> expression) =>
            {
                return expression ==null ? persons: persons.Where(expression.Compile());
            }); 

            var personContoller = new PersonController(_personRepository.Object);
            IActionResult response= personContoller.GetAll().GetAwaiter().GetResult();
            var result = ((ObjectResult)response).Value as List<Person>;

            Assert.IsNotNull(result); 
            Assert.AreEqual(2, result.Count); 
        }
        [Test]
        public async Task GetPersonById()
        {
            var guid = new Guid("7c8ac444-d06c-4b6b-8ffe-5b8e728808f7");
            _personRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Person,bool>>>(),new Expression<Func<Person, object>>[0])).ReturnsAsync(persons.FirstOrDefault(p=>p.PersonId==guid));

            var personContoller = new PersonController(_personRepository.Object);
            
            IActionResult response = personContoller.GetById(guid).GetAwaiter().GetResult();
            var result = ((ObjectResult)response).Value as Person; 
            Assert.IsNotNull(result);
            Assert.AreEqual("name2", result.Name);   


        }
    }
}