using System.Threading;

namespace Concepts.Examples.MultithreadingTests;

public class SynchronizationPromitives
{
    private int counterUnprotected = 0;
    private int counterLock = 0;
    private int counterInterlocked = 0;
    private int counterSpinLock = 0;
    private volatile int counterVolatileIncrement = 0;
    private volatile int counterVolatileClass = 0;

    private object lockObject = new object();
    private SpinLock spinLock = new SpinLock();

    public void Execute()
    {
        int tasksCount = 10000;
        Task[] tasks = new Task[tasksCount];

        for (int i = 0; i < tasksCount; i++)
        {
            int taskNumber = i + 1;
            tasks[i] = Task.Run(() =>
            {
                // Correct ways to increment. 
                lock (lockObject)
                {
                    counterLock++;
                }
                Interlocked.Increment(ref counterInterlocked);
                IncrementSpinLock();

                // Incorrect ways to increment.
                counterUnprotected++;
                counterVolatileIncrement++;
                Volatile.Write(ref counterVolatileClass, Volatile.Read(ref counterVolatileClass) + 1);
            });
        }
        Task.WaitAll(tasks);

        System.Console.WriteLine("All tasks are executed!!!");
        System.Console.WriteLine();

        // Correct values.
        System.Console.WriteLine("Correct values");
        System.Console.WriteLine($"counterLock: {counterLock}");
        System.Console.WriteLine($"counterInterlocked: {counterInterlocked}");
        System.Console.WriteLine($"counterSpinLock: {counterSpinLock}");
        System.Console.WriteLine();

        // Incorrect values.
        System.Console.WriteLine("Incorrect values");
        System.Console.WriteLine($"counterUnprotected: {counterUnprotected}");
        System.Console.WriteLine($"counterVolatileIncrement: {counterVolatileIncrement}");
        System.Console.WriteLine($"counterVolatileClass: {counterVolatileClass}");
        System.Console.WriteLine();
    }

    private void IncrementSpinLock()
    {
        bool lockTaken = false;
        spinLock.Enter(ref lockTaken);
        counterSpinLock++;
        spinLock.Exit();
    }
}