using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Concepts.Examples.Benchmarks.CollectionOperations;
using Concepts.Examples.Benchmarks.MultithreadingBenchmarks;

namespace Concepts.Examples.Benchmarks;

public class Program
{
    public static void Main(string[] args)
    {
        // var summary = BenchmarkRunner.Run<ValueEntryPassAndSortBenchmark>();
        var summary = BenchmarkRunner.Run<SynchronizationPrimitivesBenchmark>();
    }
}