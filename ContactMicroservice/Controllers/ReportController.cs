using ContactMicroservice.Utilities.MessageBrokers.RabbitMq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ContactMicroservice.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ReportController : Controller
    {
        private readonly IMessageBrokerHelper _messageBrokerHelper;
        public ReportController(IMessageBrokerHelper messageBrokerHelper)
        {
            _messageBrokerHelper = messageBrokerHelper;
        }
        [HttpGet]
        public async Task<IActionResult> SendReport()
        {
            _messageBrokerHelper.QueueMessage("sendreport"+DateTime.Now); 

            return Ok("report"); 
        }
    }
}
