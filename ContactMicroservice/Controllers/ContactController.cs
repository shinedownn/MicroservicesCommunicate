using ContactMicroservice.DataAccess.Abstract;
using ContactMicroservice.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ContactMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : Controller
    {
        private IContactRepository _contactRepository; 

        public ContactController(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }
        
        [HttpPost]
        public async Task<IActionResult> AddContactToPerson(Contact contact)
        {
            _contactRepository.Add(contact);
            var result=await _contactRepository.SaveChangesAsync();
            if (result > 0) return Ok("success");
            return BadRequest("failed");
        }
        [HttpGet]
        public async Task<IActionResult> GetContactByContactId(Guid personId)
        {
            var contacts=await _contactRepository.GetListAsync(x => x.Personid == personId);
            return Ok(contacts);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteContact(Contact contact)
        {
            _contactRepository.Delete(contact);
            var result= await _contactRepository.SaveChangesAsync();
            if (result > 0) return Ok("success");
            return BadRequest("failed");
        }
    }
}
