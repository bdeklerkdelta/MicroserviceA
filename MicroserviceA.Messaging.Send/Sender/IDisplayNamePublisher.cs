using MicroserviceA.Domain;

namespace MicroserviceA.Messaging.Send
{
    public interface IDisplayNamePublisher
    {
        void SendDisplayName(DisplayName name);
    }
}
