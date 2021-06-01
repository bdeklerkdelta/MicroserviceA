using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceA.Messaging.Send.Options
{
    public class RabbitMqOptions
    {
        public string Hostname { get; set; }
        public string QueueName { get; set; }
    }
}
