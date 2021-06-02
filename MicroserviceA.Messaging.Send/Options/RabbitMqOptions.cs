namespace MicroserviceA.Messaging.Send.Options
{
    public class RabbitMqOptions
    {
        public string Hostname { get; set; }
        public string QueueName { get; set; }
        public string ExchangeName { get; set; }
        public string RoutingKey { get; set; }
    }
}
