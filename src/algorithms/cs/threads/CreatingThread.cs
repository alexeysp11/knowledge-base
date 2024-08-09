using System.Threading;

namespace Studying.Leetcode.Threads
{
    /// <summary>
    /// Class for creating threads.
    /// </summary>
    public class CreatingThread : Studying.Leetcode.ILeetcodeProblem
    {
        /// <summary>
        /// 
        /// </summary>
        public class NumberHelper
        {
            public int MaxNumber { get; private set; }

            public NumberHelper(int maxNumber)
            {
                MaxNumber = maxNumber;
            }

            public void ShowNumbers()
            {
                for (int i = 0; i < MaxNumber; i++)
                {
                    System.Console.WriteLine(i);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Execute()
        {
            // // Start a thread that calls a parameterized static method.
            // Thread newThread = new Thread(DoWork);
            // newThread.Start(42);

            // // Start a thread that calls a parameterized instance method.
            // newThread = new Thread(DoMoreWork);
            // newThread.Start("The answer.");

            // 
            int max = 10;
            var helper = new NumberHelper(max);
            var helperThread = new Thread(helper.ShowNumbers);
            helperThread.Start();
        }

        public void DoWork(object data)
        {
            System.Console.WriteLine("Static thread procedure. Data='{0}'", data);
        }

        public void DoMoreWork(object data)
        {
            System.Console.WriteLine("Instance thread procedure. Data='{0}'", data);
        }
    }
}