using ClosedXML.Excel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ReportMicroservice.DataAccess.Abstract;
using ReportMicroservice.Utilities.MessageBrokers.RabbitMq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
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
        private readonly IWebHostEnvironment _environment;
        private readonly IReportRepository _reportRepository;
        public ReportService(IMessageConsumer messageConsumer, IConfiguration configuration, IWebHostEnvironment environment, IReportRepository reportRepository)
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
            _environment = environment;
            _reportRepository = reportRepository;
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
                var report = JsonConvert.DeserializeObject<List<Report>>(message);

                DataTable MethodResult = null;

                DataTable dt = new DataTable();
                dt.Columns.Add("Location");
                dt.Columns.Add("PersonCount");
                dt.Columns.Add("PhoneCount");

                foreach (var s in report)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = s.Location;
                    dr[1] = s.PersonCount;
                    dr[2] = s.PhoneCount;
                    dt.Rows.Add(dr); 
                }

                dt.AcceptChanges();

                MethodResult = dt; 

                XLWorkbook wb = new XLWorkbook();

                wb.Worksheets.Add(dt, "Report"); 

                var filename = Guid.NewGuid() + ".xlsx"; 

                string path = Path.Combine(_environment.ContentRootPath, "ReportFiles",filename);
                wb.SaveAs(path);

                var reportDb = _reportRepository.GetAsync(x => x.Status == "Pending").Result;
                reportDb.FilePath = path;
                reportDb.Status = "Success";
                _reportRepository.SaveChanges();

                Debug.WriteLine(message); 
            };

            _channel.BasicConsume(queue: "report",
                                 autoAck: true,
                                 consumer: consumer);
            return Task.CompletedTask; 
        }
    } 
    public class Report
    {
        public string Location { get; set; }
        public int PersonCount { get; set; }
        public int PhoneCount { get; set; }
    }
}
