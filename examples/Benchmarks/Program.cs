using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Concepts.Examples.Benchmarks.CodeStyleApproaches;
using Concepts.Examples.Benchmarks.CollectionOperations;
using Concepts.Examples.Benchmarks.GarbageCollectionCases;
using Concepts.Examples.Benchmarks.MultithreadingBenchmarks;
using Concepts.Examples.Benchmarks.StructureBenchmarks;
using Concepts.Examples.Benchmarks.TextOperations;

namespace Concepts.Examples.Benchmarks;

public class Program
{
    public static void Main(string[] args)
    {
        // Collections.
        // var summary = BenchmarkRunner.Run<ValueEntryPassAndSortBenchmark>();
        // var summary = BenchmarkRunner.Run<ArrayInitialization>();
        // var summary = BenchmarkRunner.Run<SpanInitialization>();

        // Multithreading.
        // var summary = BenchmarkRunner.Run<SynchronizationPrimitivesBenchmark>();

        // Structures.
        // var summary = BenchmarkRunner.Run<StructureInitialization>();

        // Text.
        // var summary = BenchmarkRunner.Run<StringBuilderConcatenation>();

        // Code style approaches.
        // var summary = BenchmarkRunner.Run<WhileVsRecursion>();

        // Garbage collection cases.
        // var summary = BenchmarkRunner.Run<DisposableBenchmark>();

        // Databases.
        var summary = BenchmarkRunner.Run<CollectionDbOperations>();
    }
}