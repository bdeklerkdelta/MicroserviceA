using MicroserviceA.Domain;
using System;

namespace MicroserviceA.Messaging.Send
{
    public interface IDisplayNameSender
    {
        void SendDisplayName(DisplayName name);
    }
}
