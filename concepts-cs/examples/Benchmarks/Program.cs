using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Concepts.Examples.Benchmarks.CollectionOperations;
using Concepts.Examples.Benchmarks.MultithreadingBenchmarks;
using Concepts.Examples.Benchmarks.StructureBenchmarks;

namespace Concepts.Examples.Benchmarks;

public class Program
{
    public static void Main(string[] args)
    {
        // Collections.
        // var summary = BenchmarkRunner.Run<ValueEntryPassAndSortBenchmark>();
        // var summary = BenchmarkRunner.Run<ArrayInitialization>();
        var summary = BenchmarkRunner.Run<SpanInitialization>();

        // Multithreading.
        // var summary = BenchmarkRunner.Run<SynchronizationPrimitivesBenchmark>();

        // Structures.
        // var summary = BenchmarkRunner.Run<StructureInitialization>();
    }
}