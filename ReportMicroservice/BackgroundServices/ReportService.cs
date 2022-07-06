using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ReportMicroservice.Utilities.MessageBrokers.RabbitMq;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReportMicroservice.BackgroundServices
{
    public class ReportService : BackgroundService
    {
        private readonly IMessageConsumer _messageConsumer;
        private IConnection _connection;
        private IModel _channel;
        private readonly IConfiguration _configuration;
        private readonly MessageBrokerOptions _brokerOptions;
        public ReportService(IMessageConsumer messageConsumer, IConfiguration configuration)
        {
            _configuration = configuration;
            _brokerOptions = _configuration.GetSection("MessageBrokerOptions").Get<MessageBrokerOptions>();
            var factory = new ConnectionFactory()
            {
                HostName = _brokerOptions.HostName,
                UserName = _brokerOptions.UserName,
                Password = _brokerOptions.Password
            };
            _messageConsumer = messageConsumer;
            
            
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        } 
        
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var factory = new ConnectionFactory()
            {
                HostName = _brokerOptions.HostName,
                UserName = _brokerOptions.UserName,
                Password = _brokerOptions.Password
            };


            _channel.QueueDeclare(queue: "report",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (model, mq) =>
            {
                var body = mq.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Debug.WriteLine(message); 
            };

            _channel.BasicConsume(queue: "report",
                                 autoAck: true,
                                 consumer: consumer);
            return Task.CompletedTask;

        }
    }
}
