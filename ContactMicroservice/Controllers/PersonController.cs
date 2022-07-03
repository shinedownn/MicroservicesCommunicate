using ContactMicroservice.DataAccess.Abstract;
using ContactMicroservice.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ContactMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : Controller
    {
        private IPersonRepository _personRepository;
        public PersonController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetPersonId(Guid id)
        {
            var contact = await _personRepository.GetAsync(x => x.Personid == id);
            if (contact != null) return Ok(contact);
            return BadRequest(contact);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPersons()
        {
            var persons = await _personRepository.GetListAsync();
            return Ok(persons);
        }
        [HttpPost]
        public async Task<IActionResult> AddPerson(Person person)
        {
            _personRepository.Add(person);
            var result = await _personRepository.SaveChangesAsync();
            if (result > 0) return Ok("success");
            return BadRequest("failed");
        }
        [HttpDelete]
        public async Task<IActionResult> DeletePerson(Guid id)
        {
            var person = await _personRepository.GetAsync(x => x.Personid == id);
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
