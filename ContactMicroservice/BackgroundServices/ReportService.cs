using Microsoft.Extensions.Hosting;
using ContactMicroservice.Utilities.MessageBrokers.RabbitMq;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace ContactMicroservice.BackgroundServices
{
    public class ReportService : BackgroundService
    {
        private readonly IMessageConsumer _messageConsumer;
        public ReportService(IMessageConsumer messageConsumer)
        {
            _messageConsumer = messageConsumer;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _messageConsumer.GetQueue();
            return Task.CompletedTask;
        }

        //public Task StartAsync(CancellationToken cancellationToken)
        //{
        //    _messageConsumer.GetQueue();

        //    Debug.WriteLine("report debug start");
        //    return Task.CompletedTask;
        //}

        //public Task StopAsync(CancellationToken cancellationToken)
        //{
        //    Debug.WriteLine("report debug stop");
        //    return Task.CompletedTask;
        //}
    }
}
