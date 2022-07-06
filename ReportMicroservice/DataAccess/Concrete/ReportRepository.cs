using ReportMicroservice.Contexts;
using ReportMicroservice.DataAccess.Abstract;
using ReportMicroservice.Entities.Concrete;

namespace ReportMicroservice.DataAccess.Concrete
{
    public class ReportRepository : EfEntityRepositoryBase<Report, ReportMicroserviceContext>, IReportRepository
    {
        public ReportRepository(ReportMicroserviceContext context) : base(context)
        {
        }
    }
}
