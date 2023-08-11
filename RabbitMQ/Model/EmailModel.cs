namespace Gamidas.Utils.RabbitMQ.Model;

public record EmailModel(string Recipient, string Body, string Subject);
