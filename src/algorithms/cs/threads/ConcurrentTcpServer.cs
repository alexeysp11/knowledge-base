using System.Net; 
using System.Net.Sockets; 
using System.Threading;
using System.Threading.Tasks;

namespace Studying.Leetcode.Threads
{
    /// <summary>
    /// This example is not working properly, because it works too slow compared to HTTP implementation!
    /// </summary>
    public class ConcurrentTcpServer : Studying.Leetcode.ILeetcodeProblem
    {
        private class TestTcpServer
        {
            public void CreateServer()
            {
                TcpListener listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 13000);
                try
                {
                    // Start TcpListener 
                    listener.Start();

                    // 
                    System.Console.WriteLine("Start to listen...");

                    // Start the thread 
                    while (true)
                    {
                        // var tcpClient = listener.AcceptTcpClient();
                        // ThreadPool.QueueUserWorkItem((_) => ProcessRequest(tcpClient));
                        var client = listener.AcceptTcpClient();
                        var t = new Thread(new ParameterizedThreadStart(ProcessRequest));
                        t.Start(client);
                    }
                }
                catch (System.Exception ex)
                {
                    throw;
                }
                finally
                {
                    listener.Stop();       // Stop listening for new clients.
                }
            }

            private void ProcessRequest(object clientObj)
            {
                var tcpClient = clientObj as TcpClient;
                // 
                System.Console.WriteLine($"#{Thread.CurrentThread.ManagedThreadId} Server: Starting a new TCP connection");
                byte[] receivedBytes = new byte[256]; 
                byte[] responseBytes = new byte[256]; 
                NetworkStream stream = tcpClient.GetStream();
                
                int msgLength = stream.Read(receivedBytes, 0, receivedBytes.Length); 
                
                responseBytes = receivedBytes;
                stream.Write(responseBytes, 0, responseBytes.Length); 

                tcpClient.Close();
                System.Console.WriteLine($"#{Thread.CurrentThread.ManagedThreadId} Server: Ending a new TCP connection");
            }
        }

        private class TestTcpClient
        {
            public void StartClient()
            {
                Thread.Sleep(2000);
                for (int i = 0; i < 15; i++)
                {
                    var t = Task.Run(async () => await SendRequestAsync());
                    t.Wait();
                    Thread.Sleep(100);
                }
            }

            private async Task SendRequestAsync()
            {
                System.Console.WriteLine($"#{Thread.CurrentThread.ManagedThreadId} Client: Starting a new TCP connection");
                string ip = "127.0.0.0"; 
                string serverName = "localhost"; 
                int port = 13000;
                byte[] responseBytes = new byte[1024]; 
                byte[] messageBytes = new byte[1024]; 
                TcpClient tcpClient = new TcpClient(serverName, port);
                NetworkStream networkStream = tcpClient.GetStream();
                try
                {
                    networkStream.Write(messageBytes, 0, messageBytes.Length);
                    
                    int msgLength = 0; 
                    msgLength = networkStream.Read(responseBytes, 0, responseBytes.Length);
                }
                catch (System.Exception e)
                {
                    throw e;
                }
                finally
                {
                    messageBytes = new byte[1];
                    responseBytes = new byte[1];
                    CloseConnection(tcpClient, networkStream);
                    System.Console.WriteLine($"#{Thread.CurrentThread.ManagedThreadId} Client: Ending a new TCP connection");
                }
            }

            public void CloseConnection(TcpClient tcpClient, NetworkStream networkStream)
            {
                if (tcpClient != null)
                {
                    try
                    {
                        if (networkStream != null)
                            networkStream.Close();
                        tcpClient.Close();
                    }
                    catch (System.Exception e)
                    {
                        throw; 
                    }
                    finally
                    {
                        tcpClient = null; 
                    }
                }
            }
        }

        public void Execute()
        {
            for (int i = 0; i < 5; i++)
            {
                var clientWorker = new TestTcpClient();
                Thread newThread = new Thread(new ThreadStart(clientWorker.StartClient));
                newThread.Start();
            }

            new TestTcpServer().CreateServer();
        }
    }
}