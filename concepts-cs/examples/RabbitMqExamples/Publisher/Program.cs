using RabbitMQ.Client;
using System.Text;

namespace Concepts.Examples.Publisher;

public class Program
{
    public static void Main()
    {
        //RunDefaultExchange();
        RunDirectExchange();
    }

    private static void RunDirectExchange()
    {
        Task.Run(CreateTaskDirectExchange(12000, "error"));
        Task.Run(CreateTaskDirectExchange(10000, "info"));
        Task.Run(CreateTaskDirectExchange(8000, "warning"));

        Console.ReadKey();
    }

    static Func<Task> CreateTaskDirectExchange(int timeToSleepTo, string routingKey)
    {
        return () =>
        {
            var counter = 0;
            do
            {
                int timeToSleep = new Random().Next(1000, timeToSleepTo);
                Thread.Sleep(timeToSleep);

                var factory = new ConnectionFactory() { HostName = "localhost" };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchange: "direct_logs", type: ExchangeType.Direct);

                    string message = $"Message type [{routingKey}] from publisher N {counter}";

                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "direct_logs",
                                        routingKey: routingKey,
                                        basicProperties: null,
                                        body: body);

                    Console.WriteLine($"Message type [{routingKey}] is sent into Direct Exchange [N:{counter++}]");
                }
            } while (true);
        };
    }

    private static void RunDefaultExchange()
    {
        // Configurations.
        var queueName = "dev-queue";
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