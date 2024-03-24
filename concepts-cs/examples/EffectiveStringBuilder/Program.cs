using System;
using System.Threading;
using System.Text;

namespace Concepts.Examples.EffectiveStringBuilder;

public class Program
{
    private static StringBuilderPool pool = new StringBuilderPool(5);
    private static Random random = new Random();

    public static void Main()
    {
        Thread[] threads = new Thread[3];

        for (int i = 0; i < threads.Length; i++)
        {
            threads[i] = new Thread(GenerateText);
            threads[i].Name = $"Thread{i + 1}";
            threads[i].Start();
        }

        foreach (Thread thread in threads)
        {
            thread.Join();
        }

        Console.WriteLine("All threads have finished executing.");
    }

    private static void GenerateText()
    {
        // Generate text of random length 5 times and add random characters.
        for (int i = 0; i < 5; i++)
        {
            var sb = pool.Get();

            int length = random.Next(5, 10);
            for (int j = 0; j < length; j++)
            {
                sb.Append((char)('a' + random.Next(0, 26))); 
            }

            Console.WriteLine(Thread.CurrentThread.Name + " generated: " + sb.ToString());

            // Release the StringBuilder back into a pool.
            sb.Clear();
            pool.Release(sb);
        }
    }
}