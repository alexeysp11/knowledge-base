using RabbitMQ.Client;
using System;
using System.Text;

namespace Concepts.Examples.Publisher;

public class Program
{
    private static readonly List<string> cars = new List<string> { "BMW", "Audi", "Tesla", "Mercedes" };
    private static readonly List<string> colors = new List<string> { "red", "white", "black" };
    private static readonly Random random = new Random();

    public static void Main()
    {
        //RunDefaultExchange();
        //RunDirectExchange();
        //RunTopicExchange();
        RunFanoutExchange();
    }

    private static void RunFanoutExchange()
    {
        var random = new Random();
        do
        {
            int timeToSleep = random.Next(1000, 3000);
            Thread.Sleep(timeToSleep);

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "notifier", type: ExchangeType.Fanout);

                var moneyCount = random.Next(1000, 10_000);
                string message = $"Payment received for the amount of {moneyCount}";

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "notifier",
                                    routingKey: "",
                                    basicProperties: null,
                                    body: body);

                Console.WriteLine($"Payment received for amount of {moneyCount}.\nNotifying by 'notifier' Exchange");
            }
        } while (true);
    }

    private static void RunTopicExchange()
    {
        var exchangeName = "topic_logs";
        var counter = 0;
        do
        {
            int timeToSleep = random.Next(1000, 2000);
            Thread.Sleep(timeToSleep);

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Topic);

                string routingKey = counter % 4 == 0
                    ? "Tesla.red.fast.ecological"
                    : counter % 5 == 0
                        ? "Mercedes.exclusive.expensive.ecological"
                        : GenerateRoutingKey();

                string message = $"Message type [{routingKey}] from publisher N {counter}";

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: exchangeName,
                                    routingKey: routingKey,
                                    basicProperties: null,
                                    body: body);

                Console.WriteLine($"- Message is sent into Topic Exchange: \n\t exchangeName = '{exchangeName}', \n\t message = '{message}' \n\t routingKey = '{routingKey}' \n\t counter = {counter}");
                
                counter += 1;
            }
        } while (true);
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

    private static string GenerateRoutingKey()
    {
        return $"{cars[random.Next(0, cars.Count)]}.{colors[random.Next(0, colors.Count)]}";
    }
}