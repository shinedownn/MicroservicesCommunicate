using ContactMicroservice.DataAccess.Abstract;
using ContactMicroservice.Utilities.MessageBrokers.RabbitMq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ContactMicroservice.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ReportController : Controller
    {
        private readonly IMessageBrokerHelper _messageBrokerHelper;
        private readonly IPersonRepository _personRepository;
        private readonly IContactRepository _contactRepository;
        public ReportController(IMessageBrokerHelper messageBrokerHelper, IPersonRepository personRepository, IContactRepository contactRepository)
        {
            _messageBrokerHelper = messageBrokerHelper;
            _personRepository = personRepository;
            _contactRepository = contactRepository;
        }
        [HttpGet]
        public async Task<IActionResult> SendReport()
        {
            var allpersons = _personRepository.GetListAsync().GetAwaiter().GetResult().ToList();
            var allcontacts = _contactRepository.GetListAsync().GetAwaiter().GetResult().ToList();

            var persons = (from p in allpersons
                           join c in allcontacts on p.PersonId equals c.PersonId
                           group c by c.Location into locations
                           select new Report
                           {
                               Location = locations.Key,
                               PersonCount = locations.Count(t=>t.Person!=null),
                               PhoneCount = locations.Count(t=>t.Phone!="")
                           }
                           ).ToList();



            //var person = await _personRepository.GetAsync(x => x.Name == "emre");
            var json = JsonConvert.SerializeObject(persons,Formatting.Indented,new JsonSerializerSettings()
            {
                ReferenceLoopHandling=ReferenceLoopHandling.Ignore
            });
            _messageBrokerHelper.QueueMessage(json); 

            return Ok("report"); 
        }
    }
    public class Report
    { 
        public string Location { get; set; }
        public int PersonCount { get; set; }
        public int PhoneCount { get; set; }
    }
}
