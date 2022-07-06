using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReportMicroservice.DataAccess.Abstract;
using System.Net.Http;
using System.Threading.Tasks;

namespace ReportMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportRepository _reportRepository;
        public ReportController(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        [HttpGet("GetReport")]
        public async Task<string> GetReport()
        {
            _reportRepository.Add(new Entities.Concrete.Report() {
             ReportId=System.Guid.NewGuid(),
             RequestDate=System.DateTime.Now,
             Status="Pending"
            });
            await _reportRepository.SaveChangesAsync();

            HttpClient client = new HttpClient();
            var result= await client.GetAsync("http://host.docker.internal:5000/Report/SendReport");
            var content = result.Content;
            return await content.ReadAsStringAsync(); 
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var reports = await _reportRepository.GetListAsync(null);
            return Ok(reports);
        }
    }
}
