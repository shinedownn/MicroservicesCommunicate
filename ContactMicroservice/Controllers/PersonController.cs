 using ContactMicroservice.DataAccess.Abstract;
using ContactMicroservice.Entities.Concrete; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace ContactMicroservice.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class PersonController : Controller
    {
        private IPersonRepository _personRepository;

        public PersonController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            var contact = await _personRepository.GetAsync(x => x.PersonId == id);
            if (contact != null) return Ok(contact);
            return BadRequest(contact);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var persons = await _personRepository.GetListAsync();
            return Ok(persons);
        }
        [HttpPost]
        public async Task<IActionResult> Add(Person person)
        { 
            _personRepository.Add(person);  
            var result = await _personRepository.SaveChangesAsync();
            if (result > 0) return Ok("success");
            return BadRequest("failed"); 
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var person = await _personRepository.GetAsync(x => x.PersonId == id);
            _personRepository.Delete(person);
            var result = await _personRepository.SaveChangesAsync();
            if (result > 0) return Ok("success");
            return BadRequest("failed");
        }
        [HttpPut]
        public async Task<IActionResult> Update(Person person)
        {
            _personRepository.Update(person);
            var result = await _personRepository.SaveChangesAsync();
            if (result > 0) return Ok("success");
            return BadRequest("failed");
        }
    }
}
