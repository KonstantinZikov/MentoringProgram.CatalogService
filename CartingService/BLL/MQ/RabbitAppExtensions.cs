using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.MQ
{
    public static class RabbitAppExtensions
    {
        public static IApplicationBuilder StartRabbitListeners(this IApplicationBuilder app)
        {
            foreach (IRabbitListener service in app.ApplicationServices.GetServices<IRabbitListener>())
            {
                service.Start();
            }

            return app;
        }
    }
}
