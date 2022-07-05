using ContactMicroservice.DataAccess.Abstract;
using ContactMicroservice.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ContactMicroservice.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ContactController : Controller
    {
        private IContactRepository _contactRepository; 

        public ContactController(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }
        
        [HttpPost]
        public async Task<IActionResult> Add(Contact contact)
        {
            _contactRepository.Add(contact);
            var result=await _contactRepository.SaveChangesAsync();
            if (result > 0) return Ok("success");
            return BadRequest("failed");
        }
        [HttpGet]
        public async Task<IActionResult> GetById(Guid contactId)
        {
            var contacts=await _contactRepository.GetListAsync(x => x.ContactId == contactId);
            return Ok(contacts);
        }
        [HttpPut]
        public async Task<IActionResult> Update(Contact contact)
        {
            var c = await _contactRepository.GetAsync(x => x.ContactId == contact.ContactId);
            c.Email = contact.Email;
            c.Location = contact.Location;
            c.Phone = contact.Phone;
            c.PersonId = contact.PersonId;
            _contactRepository.Update(c);
            var result = await _contactRepository.SaveChangesAsync();
            if (result > 0) return Ok("success");
            return BadRequest("failed");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Contact contact)
        {
            _contactRepository.Delete(contact);
            var result= await _contactRepository.SaveChangesAsync();
            if (result > 0) return Ok("success");
            return BadRequest("failed");
        }
    }
}
