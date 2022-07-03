using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ReportMicroservice.Contexts
{
    public class ReportDbContext : DbContext
    {
        protected IConfiguration Configuration { get; }
        public ReportDbContext()
        {

        }
        protected ReportDbContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }
    }
}
