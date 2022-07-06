using ReportMicroservice.DataAccess;
using System;

namespace ReportMicroservice.Entities.Concrete
{
    public partial class Report : IEntity
    {
        public Guid ReportId { get; set; }
        public string FilePath { get; set; }
        public DateTime RequestDate { get; set; }
        public string Status { get; set; }
    }
}
