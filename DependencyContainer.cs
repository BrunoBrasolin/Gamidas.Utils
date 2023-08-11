using Gamidas.Utils.RabbitMQ;
using Microsoft.Extensions.DependencyInjection;

namespace Gamidas.Utils;

public static class DependencyContainer
{
    public static void ConfigureGamidas(this IServiceCollection services)
    {
        services.AddScoped<ISentEvent, SentEvent>();
    }
}
