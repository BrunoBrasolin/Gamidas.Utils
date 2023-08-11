using Gamidas.Utils.RabbitMQ.Model;

namespace Gamidas.Utils.RabbitMQ.Send;

public interface ISendEvent
{
    public void SendEmail(EmailModel email);
}
