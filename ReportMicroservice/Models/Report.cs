using ReportMicroservice.DataAccess;
using System;

namespace ReportMicroservice.Models
{
    public class Report: IEntity
    {
        public Guid ReportId { get; set; }
        public DateTime RequestDate { get; set; }
        public string Status { get; set; }
    }
}
