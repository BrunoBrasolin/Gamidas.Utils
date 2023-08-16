using Microsoft.Extensions.DependencyInjection;
using Gamidas.Utils.RabbitMQ.Send;

namespace Gamidas.Utils;

public static class DependencyContainer
{
    public static void ConfigureGamidas(this IServiceCollection services)
    {
        services.AddScoped<ISendEvent, SendEvent>();
    }
}
