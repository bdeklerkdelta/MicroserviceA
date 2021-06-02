using MediatR;
using MicroserviceA.Domain;
using MicroserviceA.Messaging.Send;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MicroserviceA.Service.Command
{
    public class DisplayNameCommandHandler : IRequestHandler<DisplayNameCommand, Unit>
    {
        private readonly IDisplayNamePublisher _displayNamePublisher;

        public DisplayNameCommandHandler(IDisplayNamePublisher displayNamePublisher)
        {
            _displayNamePublisher = displayNamePublisher;
        }

        public Task<Unit> Handle(DisplayNameCommand request, CancellationToken cancellationToken)
        {
            _displayNamePublisher.SendDisplayName(new DisplayName { Name = request.Name });

            return Unit.Task;
        }
    }
}
