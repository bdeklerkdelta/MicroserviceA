using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MicroserviceA.Messaging.Send.Options;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using MicroserviceA.Service.Command;
using MediatR;
using MicroserviceA.Messaging.Send;
using MicroserviceA.Messaging.Send.Sender;
using MediatR.Extensions.FluentValidation.AspNetCore;

namespace MicroserviceA
{
    class Program
    {
        public static IConfigurationSection ConfigSection;
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, configuration) =>
            {
                configuration.Sources.Clear();

                IHostEnvironment env = hostingContext.HostingEnvironment;

                configuration
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

                IConfigurationRoot configurationRoot = configuration.Build();

                ConfigSection = configurationRoot.GetSection(nameof(RabbitMqOptions));
                var configValue = ConfigSection.Get<RabbitMqOptions>();

            })
            .ConfigureServices((_, services) =>
                     services.AddScoped<IRequestHandler<DisplayNameCommand, Unit>, DisplayNameCommandHandler>()
                             .AddScoped<IDisplayNamePublisher, DisplayNamePublisher>()
                             .Configure<RabbitMqOptions>(ConfigSection)
                             .AddMediatR(typeof(DisplayNameCommandHandler).GetTypeInfo().Assembly)
                             .AddHostedService<ConsoleApp>()
                             .AddFluentValidation(new[] { typeof(DisplayNameCommandHandler).GetTypeInfo().Assembly })
                             .AddOptions());
    }
}