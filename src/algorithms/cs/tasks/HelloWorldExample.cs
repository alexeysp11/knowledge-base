using System;
using System.Threading; 
using System.Threading.Tasks; 

namespace Studying.Leetcode.Tasks
{
    /// <summary>
    /// 
    /// </summary>
    public class HelloWorldExample : Studying.Leetcode.ILeetcodeProblem
    {
        private static string result;

        /// <summary>
        /// 
        /// </summary>
        public void Execute()
        {
            StartStatic();
        }

        public static void StartStatic() 
        {
            SaySomething();
            Console.WriteLine(result);
        }

        public static async Task<string> SaySomething() 
        {
            // await Task.Delay(5);
            Thread.Sleep(5);
            result = "Hello world!";
            return "Something";
        }
    }
}