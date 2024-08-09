using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Studying.Leetcode.Threads
{
    /// <summary>
    /// Class that handles client connections via HTTP.
    /// </summary>
    public class ConcurrentHttpServer : Studying.Leetcode.ILeetcodeProblem
    {
        /// <summary>
        /// Test implementation of the HTTP server.
        /// </summary>
        private class TestHttpServer
        {
            /// <summary>
            /// Allowed web site's paths 
            /// </summary>
            private List<string> WebPaths = new List<string>();

            /// <summary>
            /// 
            /// </summary>
            public void CreateWebServer()
            {
                // Start HttpListener 
                HttpListener listener = new HttpListener();
                AddWebPaths(); 
                AddPrefixes(listener); 
                listener.Start();

                // 
                System.Console.WriteLine("Start to listen...");

                // Start the thread 
                new Thread(() => {
                    while (true)
                    {
                        HttpListenerContext ctx = listener.GetContext();
                        ThreadPool.QueueUserWorkItem((_) => ProcessRequest(ctx));
                    }
                }).Start();
            }

            /// <summary>
        /// Processes request and sends response back 
        /// </summary>
        /// <param name="ctx"></param>
        private void ProcessRequest(HttpListenerContext ctx)
        {
            try
            {
                // Decode request 
                string url = ctx.Request.Url.ToString(); 
                string body = (new System.IO.StreamReader(ctx.Request.InputStream, ctx.Request.ContentEncoding)).ReadToEnd(); 

                // Create response body 
                byte[] buf = Encoding.UTF8.GetBytes(body);

                // Envelope response 
                ctx.Response.ContentEncoding = Encoding.UTF8;
                ctx.Response.ContentType = "text/html";
                ctx.Response.ContentLength64 = buf.Length;
                ctx.Response.OutputStream.Write(buf, 0, buf.Length);

                // 
                System.Console.WriteLine("body: " + body); 
                System.Console.WriteLine(ctx.Response.StatusCode + " " + ctx.Response.StatusDescription + ": " + ctx.Request.Url);

                ctx.Response.Close();
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex); 
            }
        }

            /// <summary>
            /// Adds all the possible paths from WebPaths dictionary into HttpListener.Prefixes 
            /// </summary>
            /// <param name="listener"></param>
            private void AddPrefixes(HttpListener listener)
            {
                foreach (string path in WebPaths)
                {
                    if (!string.IsNullOrEmpty(path)) 
                        listener.Prefixes.Add("http://127.0.0.1:8080/" + path + "/");
                }
            }

            /// <summary>
            /// Adds all the possible paths into WebPaths dictionary 
            /// </summary>
            private void AddWebPaths()
            {
                WebPaths.Add("test");
            }
        }

        /// <summary>
        /// Test implementation of the HTTP client.
        /// </summary>
        private class TestHttpClient
        {
            /// <summary>
            /// 
            /// </summary>
            private readonly HttpClient client = new HttpClient();

            /// <summary>
            /// 
            /// </summary>
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
                try
                {
                    using HttpResponseMessage response = await client.GetAsync("http://127.0.0.1:8080/test/");
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    // Above three lines can be replaced with new helper method below
                    // string responseBody = await client.GetStringAsync(uri);

                    System.Console.WriteLine(responseBody);
                }
                catch (HttpRequestException ex)
                {
                    System.Console.WriteLine("HTTP exception: " + ex.Message);
                }
                catch (System.Exception ex)
                {
                    System.Console.WriteLine("Generic exception: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Execute()
        {
            // Create HTTP clients.
            // Use threads for each HTTP client, each thread sends the request to server every 100-200 ms (15-30 iterations, 5-10 clients).
            for (int i = 0; i < 5; i++)
            {
                var clientWorker = new TestHttpClient();
                Thread newThread = new Thread(new ThreadStart(clientWorker.StartClient));
                newThread.Start();
            }
            
            // Create HTTP server.
            new TestHttpServer().CreateWebServer();
        }
    }
}