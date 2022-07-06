using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace ReportMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        [HttpGet("GetReport")]
        public async Task<string> GetReport()
        {
            HttpClient client = new HttpClient();
            var result= await client.GetAsync("http://host.docker.internal:5000/Report/SendReport");
            var content = result.Content;
            return await content.ReadAsStringAsync(); 
        }
    }
}
