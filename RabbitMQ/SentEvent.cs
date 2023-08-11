using Gamidas.Utils.RabbitMQ.Constants;
using Gamidas.Utils.RabbitMQ.Model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Gamidas.Utils.RabbitMQ;

public class SentEvent : ISentEvent
{
    private readonly IConfiguration _configuration;
    private IModel _channel;
    private IConnection _connection;

    public SentEvent(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    private void OpenConnection()
    {
        ConnectionFactory factory = new()
        {
            HostName = _configuration["RabbitMQ:HostName"],
            Port = Int32.Parse(_configuration["RabbitMQ:Port"]),
            UserName = _configuration["RabbitMQ:UserName"],
            Password = _configuration["RabbitMQ:Password"],
        };

        this._connection = factory.CreateConnection();
        this._channel = this._connection.CreateModel();
    }

    private void CloseConnection()
    {
        this._channel.Close();
        this._connection.Close();
    }

    public void SentEmail(EmailModel email)
    {
        this.OpenConnection();

        string json = JsonConvert.SerializeObject(email);
        byte[] body = Encoding.UTF8.GetBytes(json);

        this._channel.BasicPublish(exchange: "", routingKey: QueueName.EmailQueue, body: body);

        this.CloseConnection();
    }
}
