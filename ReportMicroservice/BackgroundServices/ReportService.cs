using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace ReportMicroservice.BackgroundServices
{
    public class ReportService : IHostedService
    {
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Debug.WriteLine("report debug start");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Debug.WriteLine("report debug stop");
            return Task.CompletedTask;
        }
    }
}
