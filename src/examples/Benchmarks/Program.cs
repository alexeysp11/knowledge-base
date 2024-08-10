using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using KnowledgeBase.Examples.Benchmarks.CodeStyleApproaches;
using KnowledgeBase.Examples.Benchmarks.CollectionOperations;
using KnowledgeBase.Examples.Benchmarks.GarbageCollectionCases;
using KnowledgeBase.Examples.Benchmarks.MultithreadingBenchmarks;
using KnowledgeBase.Examples.Benchmarks.StructureBenchmarks;
using KnowledgeBase.Examples.Benchmarks.TextOperations;

namespace KnowledgeBase.Examples.Benchmarks;

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