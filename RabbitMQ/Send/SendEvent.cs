using Gamidas.Utils.RabbitMQ.Constants;
using Gamidas.Utils.RabbitMQ.Model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Gamidas.Utils.RabbitMQ.Send;

public class SendEvent : ISendEvent
{
    private readonly IConfiguration _configuration;
    private IModel _channel;
    private IConnection _connection;

    public SendEvent(IConfiguration configuration)
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

    public void SendEmail(EmailModel email)
    {
        OpenConnection();

        string json = JsonConvert.SerializeObject(email);
        byte[] body = Encoding.UTF8.GetBytes(json);

        _channel.BasicPublish(exchange: "", routingKey: QueueName.EmailQueue, body: body);

        CloseConnection();
    }
}
