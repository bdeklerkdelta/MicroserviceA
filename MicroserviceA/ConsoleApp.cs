using MediatR;
using MicroserviceA.Service.Command;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace MicroserviceA
{
    public class ConsoleApp : IHostedService
    {
        private readonly IMediator _mediator;

        public ConsoleApp(IMediator mediator)
        {
            _mediator = mediator;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Please enter your name:");
            var name = Console.ReadLine();
            _mediator.Send(new DisplayNameCommand { Name = name});

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}