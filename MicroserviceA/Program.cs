using System;
using RabbitMQ.Client;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using MicroserviceA.Messaging.Send.Options;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using MicroserviceA.Service.Command;
using MediatR;
using MicroserviceA.Messaging.Send;
using MicroserviceA.Messaging.Send.Sender;

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
                     services.AddTransient<IRequestHandler<DisplayNameCommand, Unit>, DisplayNameCommandHandler>()
                             .AddTransient<IDisplayNameSender, DisplayNameSender>()
                             .Configure<RabbitMqOptions>(ConfigSection)
                             .AddMediatR(Assembly.GetExecutingAssembly())
                             .AddHostedService<ConsoleApp>()
                             .AddOptions());
    }
}