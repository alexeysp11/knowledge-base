using System.Threading; 
using System.Threading.Tasks; 

namespace Studying.Leetcode.Tasks
{
    /// <summary>
    /// 
    /// </summary>
    public class SimpleTaskDelay : Studying.Leetcode.ILeetcodeProblem
    {
        /// <summary>
        /// 
        /// </summary>
        public void Execute()
        {
            System.Console.WriteLine("Start (thread: " + Thread.CurrentThread.ManagedThreadId + ")");

            var task = Task.Run(() => {
                System.Console.WriteLine("\t Start task (thread: " + Thread.CurrentThread.ManagedThreadId + ")");

                // Thread.Sleep(2000);
                Task.Delay(2000);

                System.Console.WriteLine("\t Finsh task (thread: " + Thread.CurrentThread.ManagedThreadId + ")");
            });

            System.Console.WriteLine("Before waiting (thread: " + Thread.CurrentThread.ManagedThreadId + ")");
            task.Wait();
            System.Console.WriteLine("After waiting (thread: " + Thread.CurrentThread.ManagedThreadId + ")");

            System.Console.WriteLine("Finish (thread: " + Thread.CurrentThread.ManagedThreadId + ")");
        }
    }
}