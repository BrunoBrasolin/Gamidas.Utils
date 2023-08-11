using Gamidas.Utils.RabbitMQ.Model;

namespace Gamidas.Utils.RabbitMQ;

public interface ISentEvent
{
    public void SentEmail(EmailModel email);
}
