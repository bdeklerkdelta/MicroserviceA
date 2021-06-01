using MediatR;
using MicroserviceA.Domain;
using MicroserviceA.Messaging.Send;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MicroserviceA.Service.Command
{
    public class DisplayNameCommandHandler : IRequestHandler<DisplayNameCommand, Unit>
    {
        private readonly IDisplayNameSender _displayNameSender;

        public DisplayNameCommandHandler(IDisplayNameSender displayNameSender)
        {
            _displayNameSender = displayNameSender;
        }

        public Task<Unit> Handle(DisplayNameCommand request, CancellationToken cancellationToken)
        {
            _displayNameSender.SendDisplayName(new DisplayName { Name = request.Name });

            return Unit.Task;
        }
    }
}
