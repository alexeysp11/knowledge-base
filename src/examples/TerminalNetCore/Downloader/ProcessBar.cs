using System;
using System.Linq;
using System.Threading;

namespace Downloader
{
    public class ProcessBar
    {
        public void Run(int col, int row)
        {
            uint percentage = 0;
            while (percentage < 100)
            {
                try
                {
                    percentage = this.ExecuteTask(percentage); 
                    this.PrintCurrentState(col, row, percentage);
                }
                catch (System.ArgumentOutOfRangeException outOfRange)
                {
                    System.Console.WriteLine(outOfRange.Message);
                }
            } 
        }

        private void PrintCurrentState(int col, int row, uint percentage)
        {
            if (percentage <= 100)
            {
                int steps = (int)percentage / 10; 
                
                // Hashes and dashes
                string hashes = string.Concat(Enumerable.Repeat("#", steps));
                string dashes = string.Concat(Enumerable.Repeat("=", 10 - steps));
                string line = string.Concat(hashes, dashes); 

                Console.SetCursorPosition(col, row);
                System.Console.WriteLine($"[{line}] {percentage}%");

                if (percentage == 100)
                {
                    System.Console.WriteLine("Done!");
                }
            }
            else
            {
                throw new ArgumentOutOfRangeException(
                    "percentage",
                    "Percentage cannot be larger than 100%"
                );
            }
        }

        private uint ExecuteTask(uint progressPercents)
        {
            uint delta = 5; 
            Thread.Sleep(500);

            if (progressPercents <= 100-delta)
            {
                return progressPercents + delta; 
            }
            else
            {
                return 100; 
            }
        }
    }
}