using ReportMicroservice.Contexts;
using ReportMicroservice.DataAccess.Abstract;
using ReportMicroservice.Models;

namespace ReportMicroservice.DataAccess.Concrete
{
    public class ReportRepository : EfEntityRepositoryBase<Report, ReportDbContext>, IReportRepository
    {
        public ReportRepository(ReportDbContext context) : base(context)
        {
        }
    }
}
