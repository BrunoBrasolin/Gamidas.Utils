using Microsoft.Extensions.DependencyInjection;

namespace Gamidas.Utils;

public static class DependencyContainer
{
    public static void ConfigureGamidas(this IServiceCollection services)
    {
        services.AddScoped<RabbitMQ.Send.ISendEvent, RabbitMQ.Send.SendEvent>();
        services.AddScoped<Encryption.IEncryption, Encryption.Encryption>();
    }
}