using System.Threading;

namespace Studying.Leetcode.Threads
{
    /// <summary>
    /// Concurrently writes info in the console.
    /// </summary>
    public class ConcurrentConsoleWrite : Studying.Leetcode.ILeetcodeProblem
    {
        private int Qty { get; set; } = 15;

        /// <summary>
        /// 
        /// </summary>
        private void ConsoleWriteData(int threadId)
        {
            for (int i = 0; i < Qty; i++)
            {
                System.Console.WriteLine($"#{threadId} - iteration {i}");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Execute()
        {
            for (int i = 0; i < 3; i++)
            {
                new Thread(() => {
                    int id = Thread.CurrentThread.ManagedThreadId; 
                    System.Console.WriteLine($"#{id} - Start writing..."); 
                    ConsoleWriteData(id);
                    System.Console.WriteLine($"#{id} - Stop writing..."); 
                }).Start(); 
            }
        }
    }
}