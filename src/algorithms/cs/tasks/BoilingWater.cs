using System;
using System.Threading; 
using System.Threading.Tasks; 

namespace Studying.Leetcode.Tasks
{
    /// <summary>
    /// 
    /// </summary>
    public class BoilingWater : Studying.Leetcode.ILeetcodeProblem
    {
        public async Task<string> MakeTeaAsync()
        {
            var boilingWater = BoilWaterAsync();
            System.Console.WriteLine("Take the cups out");
            for (int i = 0; i < 1_000_000_000; i++);
            System.Console.WriteLine("Put tea in cups");
            var water = await boilingWater;
            System.Console.WriteLine($"Pour {water} in cups");
            return "tea";
        }

        public async Task<string> BoilWaterAsync()
        {
            System.Console.WriteLine("Start the kettle");
            System.Console.WriteLine("Waiting for kettle");
            await Task.Delay(200);
            System.Console.WriteLine("Kettle finished boiling");
            return "water";
        }

        /// <summary>
        /// 
        /// </summary>
        public void Execute()
        {
            Func<Task> func = async () => await MakeTeaAsync();
            Task t = Task.Run(func);
            t.Wait();
        }
    }
}