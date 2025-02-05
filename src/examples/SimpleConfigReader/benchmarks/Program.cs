using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace SimpleConfigReader.Benchmarks;

public class Program
{
    public static void Main(string[] args)
    {
        // var summary = BenchmarkRunner.Run<SeqVsAsyncPoolingBenchmark>();
        // var summary = BenchmarkRunner.Run<SeqVsAsyncXmlReadingBenchmark>();
        var summary = BenchmarkRunner.Run<SeqVsAsyncCsvReadingBenchmark>();
    }
}