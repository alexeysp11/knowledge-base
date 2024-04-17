using System.Threading;
using System.Threading.Tasks;

namespace Concepts.Examples.MultithreadingTests;

public class TestConfigureAwait
{
    public void Execute()
    {
        System.Console.WriteLine($"Execute. Thread: {Thread.CurrentThread.ManagedThreadId}");
        var task = Task.Run(async () => 
        {
            System.Console.WriteLine($"Task. Thread: {Thread.CurrentThread.ManagedThreadId}");
            var localAsyncTask = SomeAsyncOperation().ConfigureAwait(false);
            SomeSynchronousMethod();
            await localAsyncTask;
            System.Console.WriteLine($"Task. Thread: {Thread.CurrentThread.ManagedThreadId}");
        });
        task.ConfigureAwait(true);
        task.Wait();
        System.Console.WriteLine($"Execute. Thread: {Thread.CurrentThread.ManagedThreadId}");
    }

    private void SomeSynchronousMethod()
    {
        System.Console.WriteLine($"SomeSynchronousMethod. Thread: {Thread.CurrentThread.ManagedThreadId}");
    }

    private async Task SomeAsyncOperation()
    {
        System.Console.WriteLine($"SomeAsyncOperation. Thread: {Thread.CurrentThread.ManagedThreadId}");
        await Task.Delay(1000);
    }
}