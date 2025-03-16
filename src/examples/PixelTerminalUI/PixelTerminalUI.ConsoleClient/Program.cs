using System.Net.Http.Json;
using System.Net.Sockets;
using System.Text;
using PixelTerminalUI.ConsoleAdapter.Helpers;
using PixelTerminalUI.ConsoleClient.Models;
using PixelTerminalUI.ServiceEngine.Dto;

class Program
{
    static async Task Main(string[] args)
    {
        try
        {
            string serverIp = ConsoleHelper.EnterLine(hint: "Enter server IP (e.g. 127.0.0.1):", beforeInputString: ">>>");
            int port = GetPort("Enter port (e.g. 5000):");

            var communicationType = TerminalCommunicationType.Http;
            switch (communicationType)
            {
                case TerminalCommunicationType.Tcp:
                    await RunTcpAsync(serverIp, port);
                    break;
                
                case TerminalCommunicationType.Http:
                    await RunHttpAsync(serverIp, port);
                    break;
                
                case TerminalCommunicationType.Grpc:
                    //await RunGrpcAsync(serverIp, port);
                    break;
                
                default:
                    throw new Exception("Communication type is not implemented");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    static int GetPort(string hint, ConsoleColor? hintForegroundColor = null)
    {
        while (true)
        {
            string portString = ConsoleHelper.EnterLine(hint: hint, hintForegroundColor: hintForegroundColor, beforeInputString: ">>>");
            if (int.TryParse(portString, out int port))
            {
                return port;
            }
        }
    }

    static async Task RunTcpAsync(string serverIp, int port)
    {
        using (TcpClient client = new TcpClient(serverIp, port))
        {
            NetworkStream stream = client.GetStream();

            while (true)
            {
                // Response.
                byte[] responseData = new byte[1024];
                int bytesRead = await stream.ReadAsync(responseData, 0, responseData.Length);
                string responseMessage = Encoding.UTF8.GetString(responseData, 0, bytesRead);
                ConsoleHelper.WriteStringInColor(responseMessage);

                // Request.
                string requestMessage = ConsoleHelper.EnterLine(hint: "Enter data:", emptyStringReplacement: "-n", beforeInputString: ">>>");
                byte[] requestData = Encoding.UTF8.GetBytes(requestMessage);
                await stream.WriteAsync(requestData, 0, requestData.Length);
            }
        }
    }

    static async Task RunHttpAsync(string serverIp, int port)
    {
        var handler = new SocketsHttpHandler
        {
            PooledConnectionLifetime = TimeSpan.FromMinutes(15)
        };
        using (var httpClient = new HttpClient(handler))
        {
            httpClient.Timeout = TimeSpan.FromMinutes(25);
            SessionInfoDto? sessionInfoDto = null;
            while (true)
            {
                // Request.
                if (sessionInfoDto != null)
                {
                    string userInput = ConsoleHelper.EnterLine(hint: "Enter data:", emptyStringReplacement: "-n", beforeInputString: ">>>");
                    sessionInfoDto.UserInput = userInput;
                    sessionInfoDto.DisplayedInfo = null;
                    sessionInfoDto.SavedDisplayedInfo = null;
                }
                using HttpResponseMessage response = await httpClient.PostAsJsonAsync($"http://{serverIp}:{port}/pixelterminalui/go", sessionInfoDto);

                // // Response.
                response.EnsureSuccessStatusCode();
                sessionInfoDto = await response.Content.ReadFromJsonAsync<SessionInfoDto>();
                ConsoleHelper.WriteStringInColor(sessionInfoDto?.DisplayedInfo);
            }
        }
    }
}
