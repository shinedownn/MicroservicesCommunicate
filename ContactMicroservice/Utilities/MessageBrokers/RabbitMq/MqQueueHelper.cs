using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace ContactMicroservice.Utilities.MessageBrokers.RabbitMq
{
    public class MqQueueHelper : IMessageBrokerHelper
    {
        private readonly MessageBrokerOptions _brokerOptions;

        public MqQueueHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            _brokerOptions = Configuration.GetSection("MessageBrokerOptions").Get<MessageBrokerOptions>();
        }

        public IConfiguration Configuration { get; }

        public void QueueMessage(string messageText)
        {
            var factory = new ConnectionFactory
            {
                HostName = _brokerOptions.HostName,
                UserName = _brokerOptions.UserName,
                Password = _brokerOptions.Password
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(
                        queue: "report",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                //var message = JsonConvert.SerializeObject(messageText);
                var body = Encoding.UTF8.GetBytes(messageText);

                channel.BasicPublish(exchange: string.Empty, routingKey: "report", basicProperties: null, body: body);
            }
        }
    }
}
