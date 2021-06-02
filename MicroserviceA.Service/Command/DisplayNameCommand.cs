using MediatR;

namespace MicroserviceA.Service.Command
{
    public class DisplayNameCommand : IRequest<Unit>
    {
        public string Name { get; set; }
    }
}
