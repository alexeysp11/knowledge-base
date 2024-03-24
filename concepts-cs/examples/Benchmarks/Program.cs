using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Concepts.Examples.Benchmarks;

public class Program
{
    public static void Main(string[] args)
    {
        var summary = BenchmarkRunner.Run<CachedServerValuesBenchmark>();
    }
}