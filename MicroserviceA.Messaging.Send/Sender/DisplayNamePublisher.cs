using MicroserviceA.Domain;
using MicroserviceA.Messaging.Send.Options;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Text.Json;

namespace MicroserviceA.Messaging.Send.Sender
{
    public class DisplayNamePublisher : IDisplayNamePublisher
    {
        private readonly string _hostname;
        private readonly string _queueName;
        private readonly string _exchangeName;
        private readonly string _routingKey;
        private IConnection _connection;

        public DisplayNamePublisher(IOptions<RabbitMqOptions> rabbitMqOptions)
        {
            _queueName = rabbitMqOptions.Value.QueueName;
            _hostname = rabbitMqOptions.Value.Hostname;
            _exchangeName = rabbitMqOptions.Value.ExchangeName;
            _routingKey = rabbitMqOptions.Value.RoutingKey;

            CreateConnection();
        }
        public void SendDisplayName(DisplayName name)
        {
            if (ConnectionExists())
            {
                using (var channel = _connection.CreateModel())
                {
                    channel.ExchangeDeclare(_exchangeName, ExchangeType.Direct);
                    channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
                    channel.QueueBind(_queueName, _exchangeName, _routingKey, null);

                    var json = JsonSerializer.Serialize(string.Format("Hello my name is, {0}", name.Name));
                    var body = Encoding.UTF8.GetBytes(json);

                    channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: body);
                }
            }
        }

        private void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = _hostname
                };
                _connection = factory.CreateConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not create connection: {ex.Message}");
            }
        }

        private bool ConnectionExists()
        {
            if (_connection != null)
            {
                return true;
            }

            CreateConnection();

            return _connection != null;
        }
    }
}
