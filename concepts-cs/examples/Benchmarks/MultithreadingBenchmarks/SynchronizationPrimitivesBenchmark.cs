using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Threading;

namespace Concepts.Examples.Benchmarks.MultithreadingBenchmarks;

/// <summary>
/// A class for testing performance when using synchronization primitives.
/// </summary>
public class SynchronizationPrimitivesBenchmark
{
    private int counter = 0;
    private volatile int counterVolatile = 0;
    private object lockObject = new object();

    [Benchmark]
    public void TestLock()
    {
        lock (lockObject)
        {
            counter++;
        }
    }

    [Benchmark]
    public void TestInterlocked()
    {
        Interlocked.Increment(ref counter);
    }

    [Benchmark]
    public void TestVolatile()
    {
        counterVolatile++;
    }
}