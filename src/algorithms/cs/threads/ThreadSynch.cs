using System.Threading; 

namespace Studying.Leetcode.Threads
{
    /// <summary>
    /// 
    /// </summary>
    public class ThreadSynch : Studying.Leetcode.ILeetcodeProblem
    {
        /// <summary>
        /// 
        /// </summary>
        private static class Server 
        {
            /// <summary>
            /// Protects critical section from readers 
            /// </summary>
            private static ManualResetEvent _mre = new ManualResetEvent(false); 
            /// <summary>
            /// Protects critical section from writers 
            /// </summary>
            private static Mutex _mutex = new Mutex(); 

            /// <summary>
            /// Internal variable which is considered as a critical section 
            /// </summary>
            private static int Count { get; set; }

            /// <summary>
            /// Retrieves a value of the variable 
            /// </summary>
            public static int GetCount()
            {
                // Wait until the variable is free to use 
                _mre.WaitOne(); 

                // Perform the task 
                Thread.Sleep(2000); 
                return Count; 
            }

            /// <summary>
            /// Modifies internal variable 
            /// </summary>
            public static void AddToCount(int value)
            {
                // Protect critical section from other writers
                _mutex.WaitOne(); 
                // Protect critical section from any readers
                _mre.Reset(); 

                // Perform the task 
                Thread.Sleep(5000); 
                Count = value; 

                // Allow readers to access critical section
                _mre.Set(); 
                // Allow writers to access critical sections
                _mutex.ReleaseMutex(); 
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Execute()
        {
            // Start writers
            for (int i = 0; i < 2; i++)
            {
                new Thread(() => {
                    int id = Thread.CurrentThread.ManagedThreadId; 
                    System.Console.WriteLine($"#{id} - Start writing..."); 
                    Server.AddToCount(id);
                    System.Console.WriteLine($"#{id} - Stop writing..."); 
                }).Start(); 
            }

            // Start readers
            for (int j = 0; j < 5; j++)
            {
                new Thread(() => {
                    int id = Thread.CurrentThread.ManagedThreadId; 
                    System.Console.WriteLine($"#{id} - Start reading..."); 
                    int count = Server.GetCount(); 
                    System.Console.WriteLine($"count: {count}"); 
                    System.Console.WriteLine($"#{id} - Stop reading..."); 
                }).Start(); 
            }
            Thread.Sleep(15000); 
            int count1 = Server.GetCount(); 
            System.Console.WriteLine($"count1: {count1}"); 

            System.Console.ReadLine(); 
        }
    }
}