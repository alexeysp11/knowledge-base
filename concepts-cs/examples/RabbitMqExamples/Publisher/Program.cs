using RabbitMQ.Client;
using System.Text;

namespace Concepts.Examples.Publisher;

public class Program
{
    public static void Main()
    {
        // Configurations.
        var queueName = "dev-queue";
        var exchangeName = "dev-ex";
        var message = $"Message for the RMQ queue '{queueName} from Publisher'";

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
            channel.BasicPublish(exchange: "", 
                routingKey: queueName,
                basicProperties: null,
                body: Encoding.UTF8.GetBytes(message));
            Console.WriteLine($"Message is successfully sent to the RMQ server");
        }
    }
}