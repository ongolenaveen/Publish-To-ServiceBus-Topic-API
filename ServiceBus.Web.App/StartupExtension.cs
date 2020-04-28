using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceBus.Web.App.Contracts;
using ServiceBus.Web.App.Contracts.Models;
using ServiceBus.Web.App.Services;

namespace ServiceBus.Web.App
{
    public static class StartupExtension
    {
        public static void AddBindings(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<ServiceBusSettings>(config.GetSection("ServiceBusSettings"));
            services.AddScoped<IMessageService<Employee>, TopicsService<Employee>>();
        }
    }
}
