using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TcpServiceNetCore.ServiceEngine.Helpers;
using TcpServiceNetCore.ServiceEngine.Resolvers;

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

            try
            {
                NetworkStream stream = client.GetStream();
                System.Console.WriteLine("Client connected");
                
                var menuFormResolver = new MenuFormResolver();
                SessionInfo sessionInfo = menuFormResolver.InitSession();
                menuFormResolver.DisplayMenu();
                menuFormResolver.StartMenu("1");

                while (true)
                {
                    await ServiceEngineHelper.SendFormAsync(stream, sessionInfo, displayBorders: true);

                    string requestMessage = await ServiceEngineHelper.ReadMessageAsync(stream);
                    Console.WriteLine($"Received: {requestMessage}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _listener.Stop();
        _cts.Cancel();
        return Task.CompletedTask;
    }
}
