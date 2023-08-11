using Microsoft.Extensions.DependencyInjection;
using Gamidas.Utils.RabbitMQ.Send;
using Gamidas.Utils.RabbitMQ.Receive;

namespace Gamidas.Utils;

public static class DependencyContainer
{
    public static void ConfigureGamidas(this IServiceCollection services)
    {
        services.AddScoped<ISendEvent, SendEvent>();
        services.AddTransient<IReceiveEvent, ReceiveEvent>();
    }
}
