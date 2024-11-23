using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TcpHostedService;

public class TcpServerService : IHostedService
{
    private TcpListener _listener;
    private readonly int _port = 5000;
    private CancellationTokenSource _cts;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        _listener = new TcpListener(IPAddress.Any, _port);
        _listener.Start();

        _ = Task.Run(() => AcceptClientsAsync(_cts.Token));

        return Task.CompletedTask;
    }

    private async Task AcceptClientsAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            var client = await _listener.AcceptTcpClientAsync();
            _ = HandleClientAsync(client, cancellationToken);
        }
    }

    private async Task HandleClientAsync(TcpClient client, CancellationToken cancellationToken)
    {
        using (client)
        {
            NetworkStream stream = client.GetStream();
            System.Console.WriteLine("Client connected");

            byte[] responseData = new byte[256];
            int bytesRead = await stream.ReadAsync(responseData, 0, responseData.Length);
            string responseMessage = Encoding.UTF8.GetString(responseData, 0, bytesRead);
            Console.WriteLine($"Received: {responseMessage}");

            string requestMessage = $"Received: {responseMessage}";
            byte[] requestData = Encoding.UTF8.GetBytes(requestMessage);
            await stream.WriteAsync(requestData, 0, requestData.Length);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _listener.Stop();
        _cts.Cancel();
        return Task.CompletedTask;
    }
}
