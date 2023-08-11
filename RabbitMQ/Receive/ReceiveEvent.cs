using Gamidas.Utils.RabbitMQ.Constants;
using Gamidas.Utils.RabbitMQ.Model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Net.Mail;
using System.Net;

namespace Gamidas.Utils.RabbitMQ.Receive;

public class ReceiveEvent : IReceiveEvent
{
    private readonly IConfiguration _configuration;
    private IModel _channel;
    private IConnection _connection;

    public ReceiveEvent(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    private void OpenConnection()
    {
        ConnectionFactory factory = new()
        {
            HostName = _configuration["RabbitMQ:HostName"],
            Port = int.Parse(_configuration["RabbitMQ:Port"]),
            UserName = _configuration["RabbitMQ:UserName"],
            Password = _configuration["RabbitMQ:Password"],
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
    }

    private void CloseConnection()
    {
        _channel.Close();
        _connection.Close();
    }

    public void ReceiveEmail()
    {
        OpenConnection();

        EventingBasicConsumer consumer = new(_channel);

        consumer.Received += (model, ea) =>
        {
            byte[] body = ea.Body.ToArray();

            string json = Encoding.UTF8.GetString(body);

            EmailModel email = JsonConvert.DeserializeObject<EmailModel>(json);

            SmtpConfiguration smtpConfiguration = new(_configuration["SmtpConfiguration:Server"], Int32.Parse(_configuration["SmtpConfiguration:Port"]), _configuration["SmtpConfiguration:Username"], _configuration["SmtpConfiguration:AppPassword"]);

            SmtpClient smtpClient = new(smtpConfiguration.Server, smtpConfiguration.Port)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(smtpConfiguration.Username, smtpConfiguration.AppPassword)
            };

            MailMessage message = new(smtpConfiguration.Username, email.Recipient)
            {
                Subject = email.Subject,
                Body = email.Body
            };

            smtpClient.Send(message);
        };

        _channel.BasicConsume(autoAck: true, queue: QueueName.EmailQueue, consumer: consumer);
    }

    class SmtpConfiguration
    {
        public string Server;
        public int Port;
        public string Username;
        public string AppPassword;

        public SmtpConfiguration(string server, int port, string username, string appPassword)
        {
            this.Server = server;
            this.Port = port;
            this.Username = username;
            this.AppPassword = appPassword;
        }
    }
}
