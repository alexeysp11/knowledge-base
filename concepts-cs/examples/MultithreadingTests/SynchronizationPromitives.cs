using System.Threading;

namespace Concepts.Examples.MultithreadingTests;

public class SynchronizationPromitives
{
    private int counterUnprotected = 0;
    private int counterLock = 0;
    private int counterInterlocked = 0;
    private volatile int counterVolatileIncrement = 0;
    private volatile int counterVolatileClass = 0;
    private object lockObject = new object();

    public void Execute()
    {
        int tasksCount = 10000;
        Task[] tasks = new Task[tasksCount];

        for (int i = 0; i < tasksCount; i++)
        {
            int taskNumber = i + 1;
            tasks[i] = Task.Run(() =>
            {
                counterUnprotected++;
                lock (lockObject)
                {
                    counterLock++;
                }
                Interlocked.Increment(ref counterInterlocked);
                counterVolatileIncrement++;
                Volatile.Write(ref counterVolatileClass, counterVolatileClass++);
            });
        }
        Task.WaitAll(tasks);

        System.Console.WriteLine("All tasks are executed!!!");
        System.Console.WriteLine($"counterUnprotected: {counterUnprotected}");
        System.Console.WriteLine($"counterLock: {counterLock}");
        System.Console.WriteLine($"counterInterlocked: {counterInterlocked}");
        System.Console.WriteLine($"counterVolatileIncrement: {counterVolatileIncrement}");
        System.Console.WriteLine($"counterVolatileClass: {counterVolatileClass}");
    }
}