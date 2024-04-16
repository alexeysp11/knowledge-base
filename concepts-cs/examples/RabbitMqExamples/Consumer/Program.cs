using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Concepts.Examples.Publisher;

public class Program
{
    public static void Main()
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