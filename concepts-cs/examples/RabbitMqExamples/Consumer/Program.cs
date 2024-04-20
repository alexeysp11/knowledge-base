using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Concepts.Examples.Consumer;

public class Program
{
    public static void Main()
    {
        //RunDefaultExchangeConsumer();
        //RunDirectExchangeConsumer();
        RunTopicExchangeConsumer();
    }

    private static void RunTopicExchangeConsumer()
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.ExchangeDeclare(exchange: "topic_logs", type: ExchangeType.Topic);

            var queueName = channel.QueueDeclare().QueueName;

            channel.QueueBind(queue: queueName,
                                     exchange: "topic_logs",
                                     routingKey: "Tesla.#");

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (sender, e) =>
            {
                var body = e.Body;
                var message = Encoding.UTF8.GetString(body.ToArray());
                Console.WriteLine(" Received message: {0}", message);
            };

            channel.BasicConsume(queue: queueName,
                                 autoAck: true,
                                 consumer: consumer);

            Console.WriteLine($"Subscribed to the queue '{queueName}'");
            Console.WriteLine("Listerning to [Tesla.#]");

            Console.ReadLine();
        }
    }

    private static void RunDirectExchangeConsumer()
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.ExchangeDeclare(exchange: "direct_logs", type: ExchangeType.Direct);

            var queueName = channel.QueueDeclare().QueueName;

            channel.QueueBind(queue: queueName,
                                        exchange: "direct_logs",
                                        routingKey: "error");

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (sender, e) =>
            {
                var body = e.Body;
                var message = Encoding.UTF8.GetString(body.ToArray());
                Console.WriteLine(" Received message: '{0}' from the queue '{1}'", message, queueName);
            };

            channel.BasicConsume(queue: queueName,
                                    autoAck: true,
                                    consumer: consumer);

            Console.WriteLine($"Subscribed to the queue '{queueName}'");

            Console.ReadLine();
        }
    }

    private static void RunDefaultExchangeConsumer()
    {
        // Configurations.
        var queueName = "dev-queue";

        // Establish connection.
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += Consumer_Received;

            channel.BasicConsume(queue: queueName,
                autoAck: true,
                consumer: consumer);
            Console.WriteLine($"Subscribed to the '{queueName}'");
            Console.ReadLine();
        }
    }

    private static void Consumer_Received(object? sender, BasicDeliverEventArgs e)
    {
        var body = e.Body;
        var message = Encoding.UTF8.GetString(body.ToArray());
        Console.WriteLine($"- Received message from the RMQ: '{message}'");
    }
}