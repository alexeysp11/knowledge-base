using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        string server = "127.0.0.1";
        int port = 5000;

        using (TcpClient client = new TcpClient(server, port))
        {
            NetworkStream stream = client.GetStream();

            // Request.
            string requestMessage = "Hello Server!!!!!";
            byte[] requestData = Encoding.UTF8.GetBytes(requestMessage);
            await stream.WriteAsync(requestData, 0, requestData.Length);
            Console.WriteLine($"Sent: {requestMessage}");

            // Response.
            byte[] responseData = new byte[256];
            int bytesRead = await stream.ReadAsync(responseData, 0, responseData.Length);
            string responseMessage = Encoding.UTF8.GetString(responseData, 0, bytesRead);
            Console.WriteLine($"Received: {responseMessage}");
        }
    }
}
