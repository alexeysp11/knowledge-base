using System.Threading; 

namespace Studying.Leetcode.Threads
{
    /// <summary>
    /// 
    /// </summary>
    public class UsingCallbackFunction : Studying.Leetcode.ILeetcodeProblem
    {
        /// <summary>
        /// 
        /// </summary>
        public delegate void SumOfNumberCallbackDelegate(int sum);

        /// <summary>
        /// 
        /// </summary>
        public class NumberHelper
        {
            public int MaxNumber { get; private set; }
            private SumOfNumberCallbackDelegate CallbackDelegate { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public NumberHelper(int maxNumber) : this(maxNumber, null)
            {
            }

            /// <summary>
            /// 
            /// </summary>
            public NumberHelper(int maxNumber, SumOfNumberCallbackDelegate callbackDelegate)
            {
                MaxNumber = maxNumber;
                CallbackDelegate = callbackDelegate;
            }

            /// <summary>
            /// 
            /// </summary>
            public void ShowNumbers()
            {
                int sum = 0;
                for (int i = 0; i < MaxNumber; i++)
                {
                    sum += i;
                    System.Console.WriteLine(i);
                }
                if (CallbackDelegate != null)
                    CallbackDelegate(sum);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void DisplaySumOfNumber(int sum)
        {
            System.Console.WriteLine("sum: " + sum);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Execute()
        {
            // 
            int max = 10;
            var callback = new SumOfNumberCallbackDelegate(DisplaySumOfNumber);
            var helper = new NumberHelper(max, callback);
            var helperThread = new Thread(helper.ShowNumbers);
            helperThread.Start();
        }
    }
}